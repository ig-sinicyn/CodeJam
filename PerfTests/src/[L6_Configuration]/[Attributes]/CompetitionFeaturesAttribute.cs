﻿using System;

using BenchmarkDotNet.Environments;

using CodeJam.PerfTests.Configs;
using CodeJam.PerfTests.Exporters;

using JetBrains.Annotations;

namespace CodeJam.PerfTests
{
	/// <summary>Modifier attribute for competition features.</summary>
	/// <summary>Competition config attribute.</summary>
	/// <seealso cref="CompetitionFeatures"/>
	// ReSharper disable RedundantAttributeUsageProperty
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Assembly, Inherited = true, AllowMultiple = true)]
	// ReSharper restore RedundantAttributeUsageProperty
	[PublicAPI, MeansImplicitUse(ImplicitUseTargetFlags.WithMembers)]
	public class CompetitionFeaturesAttribute : Attribute, ICompetitionFeatures
	{
		private readonly CompetitionFeatures _features;

		/// <summary>Initializes a new instance of the <see cref="CompetitionFeaturesAttribute"/> class.</summary>
		public CompetitionFeaturesAttribute()
		{
			_features = new CompetitionFeatures();
		}

		/// <summary>Initializes a new instance of the <see cref="CompetitionFeaturesAttribute"/> class.</summary>
		/// <param name="features">The features.</param>
		protected CompetitionFeaturesAttribute([NotNull] CompetitionFeatures features)
		{
			Code.NotNull(features, nameof(features));

			_features = new CompetitionFeatures(features);
		}

		#region Measurements
		/// <summary>
		/// Performs single run per measurement.
		/// Recommended for use if single call time >> than timer resolution (recommended minimum is 1000 ns).
		/// </summary>
		/// <value>Target platform for the competition.</value>
		public bool BurstMode
		{
			get
			{
				return _features.BurstMode;
			}
			set
			{
				_features.BurstMode = value;
			}
		}
		#endregion

		#region Environment
		/// <summary>
		/// The code is being run on a CI server.
		/// <seealso cref="CompetitionLimitsMode.LogAnnotations"/>,
		/// <seealso cref="SourceAnnotationsMode.DontSaveAdjustedLimits"/>
		/// and <see cref="CompetitionRunMode.ContinuousIntegrationMode"/> are enabled,
		/// <see cref="ICompetitionFeatures.PreviousRunLogUri"/> is ignored.
		/// </summary>
		/// <value>
		/// <c>true</c> if the code is being run on a CI server.
		/// </value>
		public bool ContinuousIntegrationMode
		{
			get
			{
				return _features.ContinuousIntegrationMode;
			}
			set
			{
				_features.ContinuousIntegrationMode = value;
			}
		}

		/// <summary>Specifies target platform for the competition.</summary>
		/// <value>Target platform for the competition.</value>
		public Platform TargetPlatform
		{
			get
			{
				return _features.TargetPlatform;
			}
			set
			{
				_features.TargetPlatform = value;
			}
		}
		#endregion

		#region Annotations
		/// <summary>Enables source annotations feature.</summary>
		/// <value><c>true</c> if source annotations feature should be enabled.</value>
		public bool AnnotateSources
		{
			get
			{
				return _features.AnnotateSources;
			}
			set
			{
				_features.AnnotateSources = value;
			}
		}

		/// <summary>
		/// Ignores existing annotations if <see cref="AnnotateSources"/> is enabled.
		/// Value of <see cref="PreviousRunLogUri"/> is ignored.
		/// </summary>
		/// <value><c>true</c> if reannotation feature should be enabled.</value>
		public bool IgnoreExistingAnnotations
		{
			get
			{
				return _features.IgnoreExistingAnnotations;
			}
			set
			{
				_features.IgnoreExistingAnnotations = value;
			}
		}

		/// <summary>Sets the <see cref="SourceAnnotationsMode.PreviousRunLogUri"/> to the specified value.</summary>
		/// <value>The value for <see cref="SourceAnnotationsMode.PreviousRunLogUri"/>.</value>
		public string PreviousRunLogUri
		{
			get
			{
				return _features.PreviousRunLogUri;
			}
			set
			{
				_features.PreviousRunLogUri = value;
			}
		}
		#endregion

		#region Troubleshooting
		/// <summary>Fails tests if there are any warnings.</summary>
		/// <value>
		/// <c>true</c> if <see cref="CompetitionRunMode.ReportWarningsAsErrors"/> should be set to true.
		/// </value>
		public bool ReportWarningsAsErrors
		{
			get
			{
				return _features.ReportWarningsAsErrors;
			}
			set
			{
				_features.ReportWarningsAsErrors = value;
			}
		}

		/// <summary>
		/// Enables <see cref="CompetitionRunMode.DetailedLogging"/> and <see cref="CompetitionRunMode.AllowDebugBuilds"/> options.
		/// Adds the <see cref="CsvTimingsExporter"/> exporter.
		/// Adds important info and detailed info loggers.
		/// </summary>
		/// <value><c>true</c> to enable troubleshooting mode.</value>
		public bool TroubleshootingMode
		{
			get
			{
				return _features.TroubleshootingMode;
			}
			set
			{
				_features.TroubleshootingMode = value;
			}
		}

		/// <summary>Enables important info logger.</summary>
		/// <value><c>true</c> if important info logger should be used.</value>
		public bool ImportantInfoLogger
		{
			get
			{
				return _features.ImportantInfoLogger;
			}
			set
			{
				_features.ImportantInfoLogger = value;
			}
		}

		/// <summary>Enables detailed logger.</summary>
		/// <value><c>true</c> if detailed logger should be used.</value>
		public bool DetailedLogger
		{
			get
			{
				return _features.DetailedLogger;
			}
			set
			{
				_features.DetailedLogger = value;
			}
		}
		#endregion

		/// <summary>Gets the features from the attribute.</summary>
		/// <returns>Features from the attribute</returns>
		public CompetitionFeatures GetFeatures() => _features.UnfreezeCopy().Freeze();
	}
}