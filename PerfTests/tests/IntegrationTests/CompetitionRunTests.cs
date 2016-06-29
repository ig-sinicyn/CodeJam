﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

using BenchmarkDotNet.Attributes;

using CodeJam.PerfTests.Running.Core;
using CodeJam.PerfTests.Running.Messages;

using JetBrains.Annotations;

using NUnit.Framework;

using static CodeJam.PerfTests.PerfTestHelpers;

namespace CodeJam.PerfTests.IntegrationTests
{
	[PublicAPI]
	[TestFixture(Category = "BenchmarkDotNet")]
	[SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	[SuppressMessage("ReSharper", "ArgumentsStyleLiteral")]
	public static class CompetitionRunTests
	{
		private static void AssertCompetitionCompleted(CompetitionState runState, int runNumber = 1)
		{
			var summary = runState.LastRunSummary;

			Assert.AreEqual(summary.ValidationErrors.Length, 0);
			Assert.IsTrue(runState.Completed);
			Assert.AreEqual(runState.RunNumber, runNumber);
			Assert.AreEqual(runState.RunsLeft, 0);
			Assert.AreEqual(runState.RunLimitExceeded, false);
			Assert.AreEqual(runState.LooksLikeLastRun, true);
		}

		[Test]
		public static void CompetitionEmptyBenchmark()
		{
			Interlocked.Exchange(ref _callCounter, 0);

			var runState = new PerfTestRunner().Run<EmptyBenchmark>(SelfTestConfig);
			var messages = runState.GetMessages();

			Assert.AreEqual(_callCounter, 0);
			AssertCompetitionCompleted(runState);
			Assert.AreEqual(messages.Length, 0);
		}

		[Test]
		public static void CompetitionNoBaselineOkBenchmark()
		{
			Interlocked.Exchange(ref _callCounter, 0);

			var runState = new PerfTestRunner().Run<NoBaselineOkBenchmark>(SelfTestConfig);
			var messages = runState.GetMessages();

			Assert.AreEqual(_callCounter, ExpectedRunCount);
			AssertCompetitionCompleted(runState);

			Assert.AreEqual(messages.Length, 0);
		}

		[Test]
		public static void CompetitionOkBenchmark()
		{
			Interlocked.Exchange(ref _callCounter, 0);

			var runState = new PerfTestRunner().Run<OkBenchmark>(SelfTestConfig);
			var messages = runState.GetMessages();

			Assert.AreEqual(_callCounter, ExpectedRunCount);
			AssertCompetitionCompleted(runState);

			Assert.AreEqual(messages.Length, 1);

			Assert.AreEqual(messages[0].MessageText, "CompetitionAnalyser: All competition limits are ok.");
		}

		[Test]
		public static void CompetitionXmlOkBenchmark()
		{
			Interlocked.Exchange(ref _callCounter, 0);

			var runState = new PerfTestRunner().Run<XmlOkBenchmark>(SelfTestConfig);
			var messages = runState.GetMessages();

			Assert.AreEqual(_callCounter, ExpectedRunCount);
			AssertCompetitionCompleted(runState);

			Assert.AreEqual(messages.Length, 1);

			Assert.AreEqual(messages[0].MessageText, "CompetitionAnalyser: All competition limits are ok.");
		}

		[Test]
		public static void CompetitionXmlFullAnnotationBenchmark()
		{
			Interlocked.Exchange(ref _callCounter, 0);

			var runState = new PerfTestRunner().Run<XmlFullAnnotationBenchmark>(SelfTestConfig);
			var messages = runState.GetMessages();

			Assert.AreEqual(_callCounter, ExpectedRunCount);
			AssertCompetitionCompleted(runState);

			Assert.AreEqual(messages.Length, 1);

			Assert.AreEqual(messages[0].MessageText, "CompetitionAnalyser: All competition limits are ok.");
		}

		[Test]
		public static void CompetitionNoBaselineFailBenchmark()
		{
			Interlocked.Exchange(ref _callCounter, 0);

			var runState = new PerfTestRunner().Run<NoBaselineFailBenchmark>(SelfTestConfig);
			var messages = runState.GetMessages();

			Assert.AreEqual(_callCounter, ExpectedRunCount);
			AssertCompetitionCompleted(runState);

			Assert.AreEqual(messages.Length, 1);

			Assert.AreEqual(messages[0].RunNumber, 1);
			Assert.AreEqual(messages[0].RunMessageNumber, 1);
			Assert.AreEqual(messages[0].MessageSeverity, MessageSeverity.SetupError);
			Assert.AreEqual(messages[0].MessageSource, MessageSource.Analyser);
			Assert.AreEqual(messages[0].MessageText, "The benchmark NoBaselineFailBenchmark has no baseline.");
		}

		[Test]
		public static void CompetitionLimitsFailBenchmark()
		{
			Interlocked.Exchange(ref _callCounter, 0);

			var runState = new PerfTestRunner().Run<LimitsFailBenchmark>(SelfTestConfig);
			var messages = runState.GetMessages();

			Assert.AreEqual(_callCounter, 3 * ExpectedRunCount); // 3x rerun
			AssertCompetitionCompleted(runState, runNumber: 3);

			Assert.AreEqual(messages.Length, 6);

			Assert.AreEqual(messages[0].RunNumber, 1);
			Assert.AreEqual(messages[0].RunMessageNumber, 1);
			Assert.AreEqual(messages[0].MessageSeverity, MessageSeverity.TestError);
			Assert.AreEqual(messages[0].MessageSource, MessageSource.Analyser);
			Assert.That(messages[0].MessageText, Does.Contain("Method SlowerX10"));
			Assert.That(messages[0].MessageText, Does.Contain(" does not fit into limits "));

			Assert.AreEqual(messages[1].RunNumber, 1);
			Assert.AreEqual(messages[1].RunMessageNumber, 2);
			Assert.AreEqual(messages[1].MessageSeverity, MessageSeverity.Informational);
			Assert.AreEqual(messages[1].MessageSource, MessageSource.Runner);
			Assert.AreEqual(messages[1].MessageText, "Requesting 1 run(s): Limit checking failed.");

			Assert.AreEqual(messages[2].RunNumber, 2);
			Assert.AreEqual(messages[2].RunMessageNumber, 1);
			Assert.AreEqual(messages[2].MessageSeverity, MessageSeverity.TestError);
			Assert.AreEqual(messages[2].MessageSource, MessageSource.Analyser);
			Assert.That(messages[2].MessageText, Does.Contain("Method SlowerX10"));
			Assert.That(messages[2].MessageText, Does.Contain(" does not fit into limits "));

			Assert.AreEqual(messages[3].RunNumber, 2);
			Assert.AreEqual(messages[3].RunMessageNumber, 2);
			Assert.AreEqual(messages[3].MessageSeverity, MessageSeverity.Informational);
			Assert.AreEqual(messages[3].MessageSource, MessageSource.Runner);
			Assert.AreEqual(messages[3].MessageText, "Requesting 1 run(s): Limit checking failed.");

			Assert.AreEqual(messages[4].RunNumber, 3);
			Assert.AreEqual(messages[4].RunMessageNumber, 1);
			Assert.AreEqual(messages[4].MessageSeverity, MessageSeverity.TestError);
			Assert.AreEqual(messages[4].MessageSource, MessageSource.Analyser);
			Assert.That(messages[4].MessageText, Does.Contain("Method SlowerX10"));
			Assert.That(messages[4].MessageText, Does.Contain(" does not fit into limits "));

			Assert.AreEqual(messages[5].RunNumber, 3);
			Assert.AreEqual(messages[5].RunMessageNumber, 2);
			Assert.AreEqual(messages[5].MessageSeverity, MessageSeverity.Warning);
			Assert.AreEqual(messages[5].MessageSource, MessageSource.Runner);
			Assert.AreEqual(
				messages[5].MessageText,
				"The benchmark was run 3 time(s) (read log for details). Consider to adjust competition setup.");
		}

		#region Perf test helpers
		// two methods in each benchmark
		private const int ExpectedRunCount = 2 * ExpectedSelfTestRunCount;

		private static int _callCounter;

		public class EmptyBenchmark { }

		public class NoBaselineOkBenchmark
		{
			[Benchmark]
			public void Benchmark1()
			{
				Interlocked.Increment(ref _callCounter);
				Delay(PerfTestCount);
			}

			[CompetitionBenchmark(DoesNotCompete = true)]
			public void Benchmark2()
			{
				Interlocked.Increment(ref _callCounter);
				Delay(PerfTestCount);
			}
		}

		public class OkBenchmark
		{
			[CompetitionBaseline]
			public void Baseline()
			{
				Interlocked.Increment(ref _callCounter);
				Delay(PerfTestCount);
			}

			[CompetitionBenchmark(5, 20)]
			public void SlowerX10()
			{
				Interlocked.Increment(ref _callCounter);
				Delay(10 * PerfTestCount);
			}
		}

		[CompetitionMetadata(
			"CodeJam.PerfTests.Assets.CompetitionTests.xml",
			MetadataResourcePath = @"..\Assets\CompetitionTests.xml")]
		public class XmlOkBenchmark
		{
			[CompetitionBaseline]
			public void Baseline()
			{
				Interlocked.Increment(ref _callCounter);
				Delay(PerfTestCount);
			}

			[CompetitionBenchmark]
			public void SlowerX20()
			{
				Interlocked.Increment(ref _callCounter);
				Delay(20 * PerfTestCount);
			}
		}

		[CompetitionMetadata(
			"CodeJam.PerfTests.Assets.CompetitionTests.xml",
			MetadataResourcePath = @"..\Assets\CompetitionTests.xml",
			UseFullTypeName = true)]
		public class XmlFullAnnotationBenchmark
		{
			[CompetitionBaseline]
			public void Baseline()
			{
				Interlocked.Increment(ref _callCounter);
				Delay(PerfTestCount);
			}

			[CompetitionBenchmark]
			public void SlowerX20()
			{
				Interlocked.Increment(ref _callCounter);
				Delay(20 * PerfTestCount);
			}
		}

		public class NoBaselineFailBenchmark
		{
			[Benchmark]
			public void Benchmark1() => Interlocked.Increment(ref _callCounter);

			[CompetitionBenchmark]
			public void Benchmark2() => Interlocked.Increment(ref _callCounter);
		}

		public class LimitsFailBenchmark
		{
			[CompetitionBaseline]
			public void Baseline()
			{
				Interlocked.Increment(ref _callCounter);
				Delay(PerfTestCount);
			}

			[CompetitionBenchmark(1, 1)]
			public void SlowerX10()
			{
				Interlocked.Increment(ref _callCounter);
				Delay(10 * PerfTestCount);
			}
		}
		#endregion
	}
}