﻿using System;
using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

using static CodeJam.Ranges.CompositeRangeInternal;

namespace CodeJam.Ranges
{
	/// <summary>Extension methods for <see cref="CompositeRange{T}"/>.</summary>
	[PublicAPI]
	public static partial class CompositeRangeExtensions
	{
		#region ToCompositeRange
		/// <summary>Converts sequence of elements to the composite range.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <typeparam name="TKey">The type of the range key</typeparam>
		/// <param name="source">Original collection.</param>
		/// <param name="fromValueSelector">Callback to obtain a value for the From boundary.</param>
		/// <param name="toValueSelector">Callback to obtain a value for the To boundary.</param>
		/// <returns>A new composite range with keys filled from the original collection.</returns>
		[Pure]
		public static CompositeRange<T, TKey> ToCompositeRange<T, TKey>(
			[NotNull] this IEnumerable<TKey> source,
			[NotNull, InstantHandle] Func<TKey, T> fromValueSelector,
			[NotNull, InstantHandle] Func<TKey, T> toValueSelector) =>
				source
					.Select(s => Range.Create(fromValueSelector(s), toValueSelector(s), s))
					.ToCompositeRange();

		/// <summary>Converts sequence of elements to the composite range.</summary>
		/// <typeparam name="TSource">The type of the values in original collection.</typeparam>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <typeparam name="TKey">The type of the range key</typeparam>
		/// <param name="source">Original collection.</param>
		/// <param name="fromValueSelector">Callback to obtain a value for the From boundary.</param>
		/// <param name="toValueSelector">Callback to obtain a value for the To boundary.</param>
		/// <param name="keySelector">Callback to obtain a value for the range key.</param>
		/// <returns>A new composite range with keys filled from the original collection.</returns>
		[Pure]
		public static CompositeRange<T, TKey> ToCompositeRange<TSource, T, TKey>(
			[NotNull] this IEnumerable<TSource> source,
			[NotNull, InstantHandle] Func<TSource, T> fromValueSelector,
			[NotNull, InstantHandle] Func<TSource, T> toValueSelector,
			[NotNull, InstantHandle] Func<TSource, TKey> keySelector) =>
				source
					.Select(s => Range.Create(fromValueSelector(s), toValueSelector(s), keySelector(s)))
					.ToCompositeRange();
		#endregion

		#region ToCompositeRangePrev
		/// <summary>
		/// Converts sequence of elements to the composite range using only From boundary.
		/// The To boundary value is taken from the next item in sequence (+∞ for the last item in sequence)
		/// </summary>
		/// <typeparam name="TSource">The type of the values in original collection.</typeparam>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <param name="source">Original collection.</param>
		/// <param name="fromValueSelector">Callback to obtain a value for the From boundary.</param>
		/// <returns>A new composite range with keys filled from the original collection.</returns>
		[Pure]
		public static CompositeRange<T, TSource> ToCompositeRangeFrom<TSource, T>(
			[NotNull] this IEnumerable<TSource> source,
			[NotNull, InstantHandle] Func<TSource, T> fromValueSelector) =>
				ToCompositeRangeFrom(source, fromValueSelector, t => t);

		/// <summary>
		/// Converts sequence of elements to the composite range using only From boundary.
		/// The To boundary value is taken from the next item in sequence (+∞ for the last item in sequence)
		/// </summary>
		/// <typeparam name="TSource">The type of the values in original collection.</typeparam>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <typeparam name="TKey">The type of the range key</typeparam>
		/// <param name="source">Original collection.</param>
		/// <param name="fromValueSelector">Callback to obtain a value for the From boundary.</param>
		/// <param name="keySelector">Callback to obtain a value for the range key.</param>
		/// <returns>A new composite range with keys filled from the original collection.</returns>
		[Pure]
		public static CompositeRange<T, TKey> ToCompositeRangeFrom<TSource, T, TKey>(
			[NotNull] this IEnumerable<TSource> source,
			[NotNull, InstantHandle] Func<TSource, T> fromValueSelector,
			[NotNull, InstantHandle] Func<TSource, TKey> keySelector)
		{
			var keyAndFromBoundary = source
				.Select(s => (From: Range.BoundaryFrom(fromValueSelector(s)), Key: keySelector(s)))
				.OrderBy(s => s.From)
				.ToArray();

			if (keyAndFromBoundary.Length == 0)
				return CompositeRange<T, TKey>.Empty;

			// logic is following:
			// foreach item in sequence
			//   if same boundary as before - add to pending
			//   else add all (pending to current) ranges. Store current as pending.

			var prevBoundary = RangeBoundaryFrom<T>.Empty;
			var prevKeys = new List<TKey>();
			var ranges = new List<Range<T, TKey>>();
			foreach (var fromWithKey in keyAndFromBoundary)
			{
				if (prevBoundary != fromWithKey.From)
				{
					foreach (var prevKey in prevKeys)
					{
						ranges.Add(Range.Create(prevBoundary, fromWithKey.From.GetComplementation(), prevKey));
					}
					prevKeys.Clear();
					prevBoundary = fromWithKey.From;
				}

				prevKeys.Add(fromWithKey.Key);
			}

			foreach (var prevKey in prevKeys)
			{
				ranges.Add(Range.Create(prevBoundary, RangeBoundaryTo<T>.PositiveInfinity, prevKey));
			}

			return ranges.ToCompositeRange();
		}

		/// <summary>
		/// Converts sequence of elements to the composite range using only To boundary.
		/// The From boundary value is taken from the previous item in sequence (-∞ for the last item in sequence).
		/// </summary>
		/// <typeparam name="TSource">The type of the values in original collection.</typeparam>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <param name="source">Original collection.</param>
		/// <param name="toValueSelector">Callback to obtain a value for the To boundary.</param>
		/// <returns>A new composite range with keys filled from the original collection.</returns>
		[Pure]
		public static CompositeRange<T, TSource> ToCompositeRangeTo<TSource, T>(
			[NotNull] this IEnumerable<TSource> source,
			[NotNull, InstantHandle] Func<TSource, T> toValueSelector) =>
				ToCompositeRangeTo(source, toValueSelector, t => t);

		/// <summary>
		/// Converts sequence of elements to the composite range using only To boundary.
		/// The From boundary value is taken from the previous item in sequence (-∞ for the last item in sequence).
		/// </summary>
		/// <typeparam name="TSource">The type of the values in original collection.</typeparam>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <typeparam name="TKey">The type of the range key</typeparam>
		/// <param name="source">Original collection.</param>
		/// <param name="toValueSelector">Callback to obtain a value for the To boundary.</param>
		/// <param name="keySelector">Callback to obtain a value for the range key.</param>
		/// <returns>A new composite range with keys filled from the original collection.</returns>
		[Pure]
		public static CompositeRange<T, TKey> ToCompositeRangeTo<TSource, T, TKey>(
			[NotNull] this IEnumerable<TSource> source,
			[NotNull, InstantHandle] Func<TSource, T> toValueSelector,
			[NotNull, InstantHandle] Func<TSource, TKey> keySelector)
		{
			var keyAndToBoundary = source
				.Select(s => (Key: keySelector(s), To: Range.BoundaryTo(toValueSelector(s))))
				.OrderBy(s => s.To)
				.ToArray();

			if (keyAndToBoundary.Length == 0)
				return CompositeRange<T, TKey>.Empty;

			// logic is following:
			// foreach item in sequence
			//   if To is same as before - add same range with new key
			//   else add range (old.To..To, key) and.

			var prevRange = Range<T, TKey>.Empty;
			var ranges = new List<Range<T, TKey>>();
			foreach (var toWithKey in keyAndToBoundary)
			{
				if (prevRange.IsEmpty)
				{
					prevRange = Range.Create(RangeBoundaryFrom<T>.NegativeInfinity, toWithKey.To, toWithKey.Key);
				}
				else if (prevRange.To == toWithKey.To)
				{
					prevRange = prevRange.WithKey(toWithKey.Key);
				}
				else
				{
					prevRange = Range.Create(prevRange.To.GetComplementation(), toWithKey.To, toWithKey.Key);
				}
				ranges.Add(prevRange);
			}

			return ranges.ToCompositeRange();
		}
		#endregion

		#region Update range
		/// <summary>Creates a new composite range with the key specified.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <typeparam name="TKey2">The type of the new key.</typeparam>
		/// <param name="compositeRange">The source range.</param>
		/// <param name="key">The value of the new key.</param>
		/// <returns>A new composite range with the key specified.</returns>
		[Pure]
		public static CompositeRange<T, TKey2> WithKeys<T, TKey2>(this CompositeRange<T> compositeRange, TKey2 key) =>
			compositeRange.IsEmpty
				? CompositeRange<T, TKey2>.Empty
				: compositeRange.SubRanges.Select(s => s.WithKey(key)).ToCompositeRange();

		/// <summary>Creates a new composite range with the key specified.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <typeparam name="TKey">The type of the range key</typeparam>
		/// <typeparam name="TKey2">The type of the new key.</typeparam>
		/// <param name="compositeRange">The source range.</param>
		/// <param name="key">The value of the new key.</param>
		/// <returns>A new composite range with the key specified.</returns>
		[Pure]
		public static CompositeRange<T, TKey2> WithKeys<T, TKey, TKey2>(
			this CompositeRange<T, TKey> compositeRange, TKey2 key) =>
				compositeRange.IsEmpty
					? CompositeRange<T, TKey2>.Empty
					: compositeRange.SubRanges.Select(s => s.WithKey(key)).ToCompositeRange();

		/// <summary>Creates a new composite range with the key specified.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <typeparam name="TKey">The type of the range key</typeparam>
		/// <typeparam name="TKey2">The type of the new key.</typeparam>
		/// <param name="compositeRange">The source range.</param>
		/// <param name="keySelector">Callback to obtain a value for the range key.</param>
		/// <returns>A new composite range with the key specified.</returns>
		[Pure]
		public static CompositeRange<T, TKey2> WithKeys<T, TKey, TKey2>(
			this CompositeRange<T, TKey> compositeRange,
			[NotNull, InstantHandle] Func<TKey, TKey2> keySelector) =>
				compositeRange.IsEmpty
					? CompositeRange<T, TKey2>.Empty
					: compositeRange.SubRanges.Select(s => s.WithKey(keySelector(s.Key))).ToCompositeRange();

		/// <summary>Removes keys from the composite range.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <typeparam name="TKey">The type of the range key</typeparam>
		/// <param name="compositeRange">The source range.</param>
		/// <returns>A new composite range without associated keys.</returns>
		[Pure]
		public static CompositeRange<T> WithoutKeys<T, TKey>(this CompositeRange<T, TKey> compositeRange) =>
			compositeRange.IsEmpty
				? CompositeRange<T>.Empty
				: compositeRange.SubRanges.Select(s => s.WithoutKey()).ToCompositeRange();
		#endregion

		#region GetComplementation
		/// <summary>
		/// Returns complementation composite range.
		/// Result range contains result of (infinityRange.Exclude(<paramref name="range"/>).
		/// </summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <param name="range">The source range.</param>
		/// <returns>Complementation composite range.</returns>
		[Pure]
		public static CompositeRange<T> GetComplementation<T>(this Range<T> range) =>
			GetComplementationCore<T, CompositeRange<T>>(range.ToCompositeRange());

		/// <summary>
		/// Returns complementation composite range.
		/// Result range contains result of (infinityRange.Exclude(<paramref name="range"/>).
		/// </summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <typeparam name="TKey">The type of the range key</typeparam>
		/// <param name="range">The source range.</param>
		/// <returns>Complementation composite range.</returns>
		[Pure]
		public static CompositeRange<T> GetComplementation<T, TKey>(this Range<T, TKey> range) =>
			GetComplementationCore<T, CompositeRange<T, TKey>>(range.ToCompositeRange());

		/// <summary>
		/// Returns complementation composite range.
		/// Result range contains result of (infinityRange.Exclude(<paramref name="compositeRange"/>).
		/// </summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <param name="compositeRange">The source range.</param>
		/// <returns>Complementation composite range.</returns>
		[Pure]
		public static CompositeRange<T> GetComplementation<T>(this CompositeRange<T> compositeRange) =>
			GetComplementationCore<T, CompositeRange<T>>(compositeRange);

		/// <summary>
		/// Returns complementation composite range.
		/// Result range contains result of (infinityRange.Exclude(<paramref name="compositeRange"/>).
		/// </summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <typeparam name="TKey">The type of the range key</typeparam>
		/// <param name="compositeRange">The source range.</param>
		/// <returns>Complementation composite range.</returns>
		[Pure]
		public static CompositeRange<T> GetComplementation<T, TKey>(this CompositeRange<T, TKey> compositeRange) =>
			GetComplementationCore<T, CompositeRange<T, TKey>>(compositeRange);

		private static CompositeRange<T> GetComplementationCore<T, TCompositeRange>(
			TCompositeRange compositeRange)
			where TCompositeRange : ICompositeRange<T>
		{
			if (compositeRange.IsEmpty)
			{
				return CompositeRange<T>.Infinite;
			}

			var result = new List<Range<T>>();

			if (compositeRange.ContainingRange.From.HasValue)
			{
				result.Add(
					Range.Create(
						RangeBoundaryFrom<T>.NegativeInfinity,
						compositeRange.ContainingRange.From.GetComplementation()));
			}

			var previousRange = Range<T>.Empty;
			foreach (var range in compositeRange.GetMergedRanges())
			{
				if (previousRange.IsNotEmpty)
				{
					result.Add(
						Range.Create(
							previousRange.To.GetComplementation(),
							range.From.GetComplementation()));
				}
				previousRange = range;
			}

			if (compositeRange.ContainingRange.To.HasValue)
			{
				result.Add(
					Range.Create(
						compositeRange.ContainingRange.To.GetComplementation(),
						RangeBoundaryTo<T>.PositiveInfinity));
			}

			return new CompositeRange<T>(result, UnsafeOverload.NoEmptyRangesAlreadySortedAndMerged);
		}
		#endregion
	}
}