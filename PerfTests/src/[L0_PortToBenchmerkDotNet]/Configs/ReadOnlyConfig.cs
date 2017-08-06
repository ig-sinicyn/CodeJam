﻿using System;
using System.Collections.Generic;

using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Filters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Validators;

using JetBrains.Annotations;

// ReSharper disable once CheckNamespace

namespace BenchmarkDotNet.Configs
{
	/// <summary>Wrapper class for readonly config.</summary>
	/// <seealso cref="IConfig"/>
	[PublicAPI]
	public class ReadOnlyConfig : IConfig
	{
		#region Fields & .ctor
		private readonly IConfig _config;

		/// <summary>Initializes a new instance of the <see cref="ReadOnlyConfig"/> class.</summary>
		/// <param name="config">The config to wrap.</param>
		public ReadOnlyConfig([NotNull] IConfig config)
		{
			if (config == null)
				throw new ArgumentNullException(nameof(config));

			_config = config;
		}
		#endregion

		#region IConfig implementation
		/// <summary>Get benchmark column providers.</summary>
		/// <returns>Benchmark column columns.</returns>
		public IEnumerable<IColumnProvider> GetColumnProviders() => _config.GetColumnProviders();

		/// <summary>Get benchmark exporters.</summary>
		/// <returns>Benchmark exporters.</returns>
		public IEnumerable<IExporter> GetExporters() => _config.GetExporters();

		/// <summary>Get benchmark loggers.</summary>
		/// <returns>Benchmark loggers.</returns>
		public IEnumerable<ILogger> GetLoggers() => _config.GetLoggers();

		/// <summary>Get benchmark diagnosers.</summary>
		/// <returns>Benchmark diagnosers.</returns>
		public IEnumerable<IDiagnoser> GetDiagnosers() => _config.GetDiagnosers();

		/// <summary>Get benchmark analysers.</summary>
		/// <returns>Benchmark analysers.</returns>
		public IEnumerable<IAnalyser> GetAnalysers() => _config.GetAnalysers();

		/// <summary>Get benchmark jobs.</summary>
		/// <returns>Benchmark jobs.</returns>
		public IEnumerable<Job> GetJobs() => _config.GetJobs();

		/// <summary>Get benchmark validators.</summary>
		/// <returns>Benchmark validators.</returns>
		public IEnumerable<IValidator> GetValidators() => _config.GetValidators();

		/// <summary>Gets hardware counters.</summary>
		/// <returns>Hardware counters</returns>
		public IEnumerable<HardwareCounter> GetHardwareCounters() => _config.GetHardwareCounters();

		/// <summary>Gets the filters.</summary>
		/// <returns>Filters</returns>
		public IEnumerable<IFilter> GetFilters() => _config.GetFilters();

		/// <summary>Get order provider.</summary>
		/// <returns>Order provider.</returns>
		public IOrderProvider GetOrderProvider() => _config.GetOrderProvider();

		/// <summary>Gets summary style.</summary>
		/// <returns>Summary style</returns>
		public ISummaryStyle GetSummaryStyle() => _config.GetSummaryStyle();

		/// <summary>Get union rule.</summary>
		/// <returns>Union rule.</returns>
		public ConfigUnionRule UnionRule { get; }

		/// <summary>Determines if all auto-generated files should be kept or removed after running benchmarks.</summary>
		public bool KeepBenchmarkFiles { get; }
		#endregion
	}
}