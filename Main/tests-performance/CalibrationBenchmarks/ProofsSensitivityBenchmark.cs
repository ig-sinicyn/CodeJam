﻿using System;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.NUnit;

using JetBrains.Annotations;

using NUnit.Framework;

using static CodeJam.AssemblyWideConfig;

namespace CodeJam
{
	/// <summary>
	/// Prooftest: benchmark is sensitive enough to spot a minimal method change
	/// </summary>
	[TestFixture(Category = BenchmarkConstants.BenchmarkCategory + ": Self-testing")]
	[PublicAPI]
	public class ProofsSensitivityBenchmark
	{
		[Test]
		[Explicit(BenchmarkConstants.ExplicitExcludeReason)]
		public void BenchmarkSensitivity() => CompetitionBenchmarkRunner.Run(this, RunConfig);

		[Params(1000, 10 * 1000, 100 * 1000, 1000 * 1000)]
		public int Count { get; set; }

		[Benchmark(Baseline = true)]
		public int Test00Baseline()
		{
			var sum = 0;
			var count = Count;
			for (var i = 0; i < count; i++)
			{
				sum += i;
			}

			return sum;
		}

		[CompetitionBenchmark]
		public int Test01PlusOne()
		{
			var sum = 0;
			var count = Count;
			for (var i = 0; i < count; i++)
			{
				sum += i + 1;
			}

			return sum;
		}
	}
}