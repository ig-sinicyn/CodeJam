﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;

using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Helpers;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;

using CodeJam.PerfTests.Running.Core;
using CodeJam.PerfTests.Running.Limits;
using CodeJam.PerfTests.Running.Messages;

using JetBrains.Annotations;

using static CodeJam.PerfTests.Loggers.HostLogger;

namespace CodeJam.PerfTests.Running.SourceAnnotations
{
	/// <summary>
	/// Helper class for xml annotations
	/// </summary>
	[SuppressMessage("ReSharper", "ArrangeBraces_using")]
	internal static class XmlAnnotations
	{
		#region XML metadata constants
		private const string LogAnnotationStart = LogImportantAreaStart +
			" ------ xml_annotation_begin ------";

		private const string LogAnnotationEnd = LogImportantAreaEnd +
			" ------- xml_annotation_end -------";

		private const string CompetitionBenchmarksRootNode = "CompetitionBenchmarks";
		private const string CompetitionNode = "Competition";
		private const string CandidateNode = "Candidate";
		private const string TargetAttribute = "Target";
		private const string BaselineAttribute = "Baseline";
		private const string MinRatioAttribute = "MinRatio";
		private const string MaxRatioAttribute = "MaxRatio";
		#endregion

		#region XML doc loading

		#region Core logic for XML annotations
		[NotNull]
		// ReSharper disable once SuggestBaseTypeForParameter
		private static string GetCompetitionName(this Target target, XDocument xmlAnnotationDoc,
			bool useFullTypeName) =>
			useFullTypeName
				? target.Type.GetShortAssemblyQualifiedName()
				: target.Type.Name;

		[NotNull]
		private static string GetCandidateName(this Target target) =>
			target.MethodDisplayInfo;

		[NotNull]
		// ReSharper disable once SuggestBaseTypeForParameter
		private static XElement GetOrAddElement(this XElement parent, XName elementName, string targetName)
		{
			if (targetName == null)
				throw new ArgumentNullException(nameof(targetName));

			var result = parent
				.Elements(elementName)
				.SingleOrDefault(e => e.Attribute(TargetAttribute)?.Value == targetName);

			if (result == null)
			{
				result = new XElement(elementName);
				result.SetAttribute(TargetAttribute, targetName);
				parent.Add(result);
			}

			return result;
		}

		[CanBeNull]
		// ReSharper disable once UnusedMethodReturnValue.Local
		private static XAttribute SetAttribute(this XElement parent, XName attributeName, object attributeValue)
		{
			if (attributeValue == null)
			{
				parent.Attribute(attributeName)?.Remove();
				return null;
			}

			var result = parent.Attribute(attributeName);

			if (result == null)
			{
				result = new XAttribute(attributeName, attributeValue);
				parent.Add(result);
			}
			else
			{
				result.SetValue(attributeValue);
			}

			return result;
		}

		[NotNull]
		private static XmlReaderSettings GetXmlReaderSettings() =>
			new XmlReaderSettings
			{
				DtdProcessing = DtdProcessing.Prohibit
			};

		[NotNull]
		private static XmlWriterSettings GetXmlWriterSettings(bool omitXmlDeclaration = false) =>
			new XmlWriterSettings
			{
				OmitXmlDeclaration = omitXmlDeclaration,
				Indent = true,
				IndentChars = "\t",
				NewLineChars = "\r\n"
			};
		#endregion

		#region XML annotations
		/// <summary>Parses xml annotation document.</summary>
		/// <param name="source">Stream that contains xml document.</param>
		/// <param name="competitionState">State of the run.</param>
		/// <param name="sourceDescription">Source description to be used in messages.</param>
		/// <returns>XML annotation document or <c>null</c> if parsing failed.</returns>
		[CanBeNull]
		public static XDocument TryParseXmlAnnotationDoc(
			[NotNull] Stream source,
			[NotNull] CompetitionState competitionState,
			[NotNull] string sourceDescription)
		{
			Code.NotNull(source, nameof(source));
			Code.NotNull(competitionState, nameof(competitionState));
			Code.NotNullNorEmpty(sourceDescription, nameof(sourceDescription));

			XDocument xmlAnnotationDoc;
			try
			{
				using (var reader = XmlReader.Create(source, GetXmlReaderSettings()))
				{
					xmlAnnotationDoc = XDocument.Load(reader);
				}
			}
			catch (ArgumentException ex)
			{
				competitionState.WriteExceptionMessage(
					MessageSource.Analyser, MessageSeverity.SetupError,
					$"{sourceDescription}: Could not parse annotation.", ex);
				return null;
			}
			catch (XmlException ex)
			{
				competitionState.WriteExceptionMessage(
					MessageSource.Analyser, MessageSeverity.SetupError,
					$"{sourceDescription}: Could not parse annotation.", ex);
				return null;
			}

			var rootNode = xmlAnnotationDoc.Element(CompetitionBenchmarksRootNode);
			if (rootNode == null)
			{
				competitionState.WriteMessage(
					MessageSource.Analyser, MessageSeverity.SetupError,
					$"{sourceDescription}: root node {CompetitionBenchmarksRootNode} not found.");

				return null;
			}

			return xmlAnnotationDoc;
		}

		/// <summary>Parses xml annotation document from the resource.</summary>
		/// <param name="annotationResourceKey">Name of the xml resource with competition limits.</param>
		/// <param name="competitionState">State of the run.</param>
		/// <returns>XML annotation document or <c>null</c> if parsing failed.</returns>
		[CanBeNull]
		public static XDocument TryParseXmlAnnotationDoc(
			ResourceKey annotationResourceKey,
			[NotNull] CompetitionState competitionState)
		{
			Code.NotNull(competitionState, nameof(competitionState));
			var assembly = annotationResourceKey.Assembly;
			var resourceName = annotationResourceKey.ResourceName;

			using (var resourceStream = assembly.GetManifestResourceStream(resourceName))
			{
				if (resourceStream == null)
				{
					competitionState.WriteMessage(
						MessageSource.Analyser, MessageSeverity.SetupError,
						$"XML annotation resource stream '{resourceName}' not found. Assembly {assembly.FullName}.");

					return null;
				}

				return TryParseXmlAnnotationDoc(resourceStream, competitionState, $"Resource '{resourceName}'");
			}
		}

		/// <summary>Parses xml annotation documents from the log.</summary>
		/// <param name="logUri">The URI the log will be obtained from.</param>
		/// <param name="competitionState">State of the run.</param>
		/// <returns>Parsed xml annotation documents, if any or <c>null</c> if parsing failed.</returns>
		[CanBeNull]
		public static XDocument[] TryParseXmlAnnotationDocsFromLog(
			[NotNull] string logUri,
			[NotNull] CompetitionState competitionState)
		{
			Code.NotNullNorEmpty(logUri, nameof(logUri));
			Code.NotNull(competitionState, nameof(competitionState));

			competitionState.WriteVerbose($"Downloading '{logUri}'.");

			using (var reader = BenchmarkHelpers.TryGetTextFromUri(logUri, TimeSpan.FromSeconds(15)))
			{
				if (reader == null)
				{
					competitionState.WriteMessage(
						MessageSource.Analyser, MessageSeverity.Warning,
						$"Could not load log content from '{logUri}'.");
					return null;
				}

				competitionState.WriteVerbose($"Parsing '{logUri}' content.");

				return ParseLogContent(logUri, reader, competitionState);
			}
		}

		private static XDocument[] ParseLogContent(
			string logUri, TextReader reader,
			CompetitionState competitionState)
		{
			var result = new List<XDocument>();

			var buffer = new StringBuilder();
			int lineNumber = 0, xmlStartLineNumber = -1;
			string logLine;
			while ((logLine = reader.ReadLine()) != null)
			{
				lineNumber++;

				if (logLine.StartsWith(LogAnnotationStart, StringComparison.OrdinalIgnoreCase))
				{
					if (xmlStartLineNumber >= 0)
					{
						competitionState.WriteMessage(
							MessageSource.Analyser, MessageSeverity.Warning,
							$"The log is damaged, annotation area start tag was repeated twice. Uri '{logUri}', lines {xmlStartLineNumber} and {lineNumber}.");
					}
					xmlStartLineNumber = lineNumber;
				}
				else if (logLine.StartsWith(LogAnnotationEnd, StringComparison.OrdinalIgnoreCase))
				{
					if (xmlStartLineNumber < 0)
					{
						competitionState.WriteMessage(
							MessageSource.Analyser, MessageSeverity.Warning,
							$"The log is damaged, annotation area start tag is missing. Uri '{logUri}', start tag at line {lineNumber}.");
					}
					else if (buffer.Length > 0)
					{
						var xmlAnnotationDoc = TryParseLogContentDoc(
							buffer.ToString(), competitionState, logUri, xmlStartLineNumber);
						if (xmlAnnotationDoc != null)
						{
							result.Add(xmlAnnotationDoc);
						}
					}

					buffer.Length = 0;
					xmlStartLineNumber = -1;
				}
				else if (xmlStartLineNumber >= 0)
				{
					buffer.Append(logLine);
				}
			}

			if (xmlStartLineNumber >= 0)
			{
				competitionState.WriteMessage(
					MessageSource.Analyser, MessageSeverity.Warning,
					$"The log is damaged, annotation area end tag is missing. Uri '{logUri}', start tag at line {xmlStartLineNumber}.");
			}

			return result.ToArray();
		}

		[CanBeNull]
		private static XDocument TryParseLogContentDoc(
			string logXmlText,
			CompetitionState competitionState,
			string logUri, int logLine)
		{
			using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(logXmlText)))
			{
				var xmlAnnotationDoc = TryParseXmlAnnotationDoc(
					stream,
					competitionState,
					$"Log '{logUri}', line {logLine}");

				return xmlAnnotationDoc;
			}
		}

		/// <summary>Writes xml annotation document for the competition targets to the log.</summary>
		/// <param name="competitionTargets">The competition targets to log.</param>
		/// <param name="competitionState">State of the run.</param>
		public static void LogXmlAnnotationDoc(
			[NotNull] IReadOnlyCollection<CompetitionTarget> competitionTargets,
			[NotNull] CompetitionState competitionState)
		{
			Code.NotNull(competitionTargets, nameof(competitionTargets));
			Code.NotNull(competitionState, nameof(competitionState));

			if (competitionTargets.Count == 0)
				return;

			// Create xml annotation doc
			var xmlAnnotationDoc = new XDocument(new XElement(CompetitionBenchmarksRootNode));
			foreach (var competitionTarget in competitionTargets)
			{
				AddOrUpdateXmlAnnotation(xmlAnnotationDoc, competitionTarget, true);
			}

			// Dump it
			var tmp = new StringBuilder();
			using (var writer = XmlWriter.Create(tmp, GetXmlWriterSettings(true)))
			{
				xmlAnnotationDoc.Save(writer);
			}

			var logger = competitionState.Logger;
			using (BeginLogImportant(competitionState.Config))
			{
				logger.WriteLineInfo(LogAnnotationStart);
				logger.WriteLineInfo(tmp.ToString());
				logger.WriteLineInfo(LogAnnotationEnd);
			}
		}
		#endregion

		/// <summary>Saves the specified xml annotation document.</summary>
		/// <param name="xmlAnnotationDoc">The xml annotation document.</param>
		/// <param name="output">The writer the document will be saved to.</param>
		public static void Save(
			[NotNull] XDocument xmlAnnotationDoc,
			[NotNull] TextWriter output)
		{
			Code.NotNull(xmlAnnotationDoc, nameof(xmlAnnotationDoc));
			Code.NotNull(output, nameof(output));

			using (var writer = XmlWriter.Create(output, GetXmlWriterSettings()))
			{
				xmlAnnotationDoc.Save(writer);
			}
		}
		#endregion

		#region CompetitionLimit-related
		/// <summary>Parses competition limit from the xml annotation document.</summary>
		/// <param name="target">The target.</param>
		/// <param name="xmlAnnotationDoc">The xml annotation document.</param>
		/// <param name="useFullTypeName">Use full type name in XML annotations.</param>
		/// <param name="competitionState">State of the run.</param>
		/// <returns>Parsed competition limit or <c>null</c> if there is no xml annotation for the target.</returns>
		[CanBeNull]
		public static LimitRange? TryParseCompetitionLimit(
			[NotNull] Target target,
			[NotNull] XDocument xmlAnnotationDoc,
			bool useFullTypeName,
			[NotNull] CompetitionState competitionState)
		{
			Code.NotNull(target, nameof(target));
			Code.NotNull(xmlAnnotationDoc, nameof(xmlAnnotationDoc));
			Code.NotNull(competitionState, nameof(competitionState));

			var competitionName = target.GetCompetitionName(xmlAnnotationDoc, useFullTypeName);
			var candidateName = target.GetCandidateName();

			var matchingNodes =
				// ReSharper disable once PossibleNullReferenceException
				from competition in xmlAnnotationDoc.Element(CompetitionBenchmarksRootNode).Elements(CompetitionNode)
				where competition.Attribute(TargetAttribute)?.Value == competitionName
				from candidate in competition.Elements(CandidateNode)
				where candidate.Attribute(TargetAttribute)?.Value == candidateName
				select candidate;

			var competitionNode = matchingNodes.SingleOrDefault();
			if (competitionNode == null)
			{
				return null;
			}

			var baseline = TryParseFlagValue(target, competitionNode, BaselineAttribute, competitionState);
			var minRatio = TryParseLimitValue(target, competitionNode, MinRatioAttribute, competitionState);
			var maxRatio = TryParseLimitValue(target, competitionNode, MaxRatioAttribute, competitionState);

			if (baseline.GetValueOrDefault() != target.Baseline)
			{
				competitionState.WriteMessage(
					MessageSource.Analyser, MessageSeverity.SetupError,
					$"XML anotation for {target.MethodDisplayInfo}: baseline flag on the method and in the annotation do not match.");
				return null;
			}
			return LimitRange.CreateRatioLimit(minRatio, maxRatio);
		}

		private static T? TryParseCore<T>(
			Target target, XElement competitionNode,
			string limitProperty,
			CompetitionState competitionState,
			Func<string, T?> tryParseCallback)
			where T : struct
		{
			T? result = null;

			var limitText = competitionNode.Attribute(limitProperty)?.Value;
			if (limitText != null)
			{
				result = tryParseCallback(limitText);
				if (result == null)
				{
					competitionState.WriteMessage(
						MessageSource.Analyser, MessageSeverity.SetupError,
						$"XML anotation for {target.MethodDisplayInfo}: could not parse {limitProperty}.");
				}
			}

			return result;
		}

		private static double? TryParseLimitValue(
			Target target, XElement competitionNode,
			string limitProperty,
			CompetitionState competitionState) =>
				TryParseCore(
					target, competitionNode,
					limitProperty, competitionState,
					s => double.TryParse(s, NumberStyles.Any, HostEnvironmentInfo.MainCultureInfo, out var result)
						? result
						: default(double?));

		private static bool? TryParseFlagValue(
			Target target, XElement competitionNode,
			string limitProperty,
			CompetitionState competitionState) =>
				TryParseCore(
					target, competitionNode,
					limitProperty, competitionState,
					s => bool.TryParse(s, out var result) ? result : default(bool?));

		/// <summary>Adds or updates xml annotation for the competition target.</summary>
		/// <param name="xmlAnnotationDoc">The xml annotation document that will be updated.</param>
		/// <param name="competitionTarget">The competition target.</param>
		/// <param name="useFullTypeName">Use full type name in XML annotations.</param>
		public static void AddOrUpdateXmlAnnotation(
			[NotNull] XDocument xmlAnnotationDoc,
			[NotNull] CompetitionTarget competitionTarget,
			bool useFullTypeName)
		{
			Code.NotNull(xmlAnnotationDoc, nameof(xmlAnnotationDoc));
			Code.NotNull(competitionTarget, nameof(competitionTarget));
			Code.NotNull(competitionTarget.CompetitionMetadata, nameof(competitionTarget.CompetitionMetadata));

			var competitionName = competitionTarget.Target.GetCompetitionName(xmlAnnotationDoc, useFullTypeName);
			var candidateName = competitionTarget.Target.GetCandidateName();
			var isBaseline = competitionTarget.Baseline;

			var competition = xmlAnnotationDoc
				.Element(CompetitionBenchmarksRootNode)
				.GetOrAddElement(CompetitionNode, competitionName);
			var candidate = competition.GetOrAddElement(CandidateNode, candidateName);

			var baselineText = isBaseline ? XmlConvert.ToString(true) : null;
			var minText = competitionTarget.Limits.MinRatioText;
			var maxText = competitionTarget.Limits.MaxRatioText;

			// Informational only, ignored on parse
			candidate.SetAttribute(BaselineAttribute, baselineText);

			candidate.SetAttribute(MaxRatioAttribute, minText);
			candidate.SetAttribute(MinRatioAttribute, maxText);
		}
		#endregion
	}
}