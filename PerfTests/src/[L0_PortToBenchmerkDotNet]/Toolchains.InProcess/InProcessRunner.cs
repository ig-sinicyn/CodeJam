﻿using System;
using System.Reflection;

using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace BenchmarkDotNet.Toolchains.InProcess
{
	internal class InProcessRunner
	{
		public static int Run(IHostApi hostApi, Benchmark benchmark, BenchmarkActionCodegen codegenMode)
		{
			bool isDiagnoserAttached = hostApi.IsDiagnoserAttached;

			// the first thing to do is to let diagnosers hook in before anything happens
			// so all jit-related diagnosers can catch first jit compilation!
			if (isDiagnoserAttached)
				hostApi.BeforeAnythingElse();

			try
			{
				// we are not using Runnable here in any direct way in order to avoid strong dependency Main<=>Runnable
				// which could cause the jitting/assembly loading to happen before we do anything
				// we have some jitting diagnosers and we want them to catch all the informations!!

				var inProcessRunnableTypeName = $"{typeof(InProcessRunner).FullName}+{nameof(Runnable)}";
				var type = typeof(InProcessRunner).GetTypeInfo().Assembly.GetType(inProcessRunnableTypeName);
				if (type == null)
					throw new InvalidOperationException($"Bug: type {inProcessRunnableTypeName} not found.");

				type.GetMethod(nameof(Runnable.RunCore), BindingFlags.Public | BindingFlags.Static)
					.Invoke(null, new object[] { hostApi, benchmark, codegenMode });

				return 0;
			}
			catch (Exception ex)
			{
				hostApi.WriteLine(ex.ToString());
				return -1;
			}
		}

		[UsedImplicitly]
		private static class Runnable
		{
			public static void RunCore(IHostApi hostApi, Benchmark benchmark, BenchmarkActionCodegen codegenMode)
			{
				var target = benchmark.Target;
				var job = benchmark.Job; // TODO: filter job (same as SourceCodePresenter does)?
				var unrollFactor = benchmark.Job.ResolveValue(RunMode.UnrollFactorCharacteristic, EnvResolver.Instance);
				// var dummyUnrollFactor = 1 << 6; // TODO: as arg to CreateDummy()?

				// DONTTOUCH: these should be allocated together
				var instance = Activator.CreateInstance(benchmark.Target.Type);
				var mainAction = BenchmarkActionFactory.CreateRun(target, instance, codegenMode, unrollFactor);
				var idleAction = BenchmarkActionFactory.CreateIdle(target, instance, codegenMode, unrollFactor);
				var setupAction = BenchmarkActionFactory.CreateSetup(target, instance);
				var cleanupAction = BenchmarkActionFactory.CreateCleanup(target, instance);
				var dummy1 = BenchmarkActionFactory.CreateDummy();
				var dummy2 = BenchmarkActionFactory.CreateDummy();
				var dummy3 = BenchmarkActionFactory.CreateDummy();

				FillProperties(instance, benchmark);

				hostApi.WriteLine();
				foreach (var infoLine in BenchmarkEnvironmentInfo.GetCurrent().ToFormattedString())
					hostApi.WriteLine("// {0}", infoLine);
				hostApi.WriteLine("// Job: {0}", job.DisplayInfo);
				hostApi.WriteLine();

				var engineParameters = new EngineParameters
				{
					//HostApi = hostApi,
					IsDiagnoserAttached = hostApi.IsDiagnoserAttached,
					MainAction = mainAction.InvokeMultiple,
					Dummy1Action = dummy1.InvokeSingle,
					Dummy2Action = dummy2.InvokeSingle,
					Dummy3Action = dummy3.InvokeSingle,
					IdleAction = idleAction.InvokeMultiple,
					SetupAction = setupAction.InvokeSingle,
					CleanupAction = cleanupAction.InvokeSingle,
					TargetJob = job,
					OperationsPerInvoke = target.OperationsPerInvoke
				};

				var engine = job
					.ResolveValue(InfrastructureMode.EngineFactoryCharacteristic, InfrastructureResolver.Instance)
					.Create(engineParameters);

				engine.PreAllocate();

				setupAction.InvokeSingle();

				if (job.ResolveValue(RunMode.RunStrategyCharacteristic, EngineResolver.Instance)
					!= RunStrategy.ColdStart)
					engine.Jitting(); // does first call to main action, must be executed after setup()!

				if (hostApi.IsDiagnoserAttached)
					hostApi.AfterSetup();

				var results = engine.Run();

				if (hostApi.IsDiagnoserAttached)
					hostApi.BeforeCleanup();
				cleanupAction.InvokeSingle();

				hostApi.Print(results); // printing costs memory, do this after runs
			}

			/// <summary>Fills the properties of the instance of the object used to run the benchmark.</summary>
			/// <param name="instance">The instance.</param>
			/// <param name="benchmark">The benchmark.</param>
			private static void FillProperties(object instance, Benchmark benchmark)
			{
				foreach (var parameter in benchmark.Parameters.Items)
				{
					var flags = BindingFlags.Public;
					flags |= parameter.IsStatic ? BindingFlags.Static : BindingFlags.Instance;

					var targetType = benchmark.Target.Type;
					var paramProperty = targetType.GetProperty(parameter.Name, flags);

					var setter = paramProperty?.GetSetMethod();
					if (setter == null)
						throw new InvalidOperationException(
							$"Type {targetType.FullName}: no settable property {parameter.Name} found.");

					// ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
					if (setter.IsStatic)
						setter.Invoke(null, new[] { parameter.Value });
					else
						setter.Invoke(instance, new[] { parameter.Value });
				}
			}
		}
	}
}