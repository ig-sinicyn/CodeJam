﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using JetBrains.Annotations;

using static CodeJam.PlatformDependent;

namespace CodeJam.Ranges
{
	/// <summary>Extension methods for <seealso cref="Range{T}"/>.</summary>
	[SuppressMessage("ReSharper", "ArrangeRedundantParentheses")]
	public static partial class RangeExtensions
	{
		#region Updating a range
		/// <summary>
		/// Replaces exclusive boundaries with inclusive ones with the values from the selector callbacks
		/// </summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="fromValueSelector">Callback to obtain a new value for the From boundary. Used if the boundary is exclusive.</param>
		/// <param name="toValueSelector">Callback to obtain a new value for the To boundary. Used if the boundary is exclusive.</param>
		/// <returns>A range with inclusive boundaries.</returns>
		[Pure]
		public static Range<T> MakeInclusive<T>(
			this Range<T> range,
			[NotNull, InstantHandle] Func<T, T> fromValueSelector,
			[NotNull, InstantHandle] Func<T, T> toValueSelector)
		{
			if (range.IsEmpty || (!range.From.IsExclusiveBoundary && !range.To.IsExclusiveBoundary))
			{
				return range;
			}

			var from = range.From;
			if (from.IsExclusiveBoundary)
			{
				from = Range.BoundaryFrom(fromValueSelector(from.GetValueOrDefault()));
			}
			var to = range.To;
			if (to.IsExclusiveBoundary)
			{
				to = Range.BoundaryTo(toValueSelector(to.GetValueOrDefault()));
			}

			return range.TryCreateRange(from, to);
		}

		/// <summary>
		/// Replaces inclusive boundaries with exclusive ones with the values from the selector callbacks
		/// </summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="fromValueSelector">Callback to obtain a new value for the From boundary. Used if the boundary is inclusive.</param>
		/// <param name="toValueSelector">Callback to obtain a new value for the To boundary. Used if the boundary is inclusive.</param>
		/// <returns>A range with exclusive boundaries.</returns>
		[Pure]
		public static Range<T> MakeExclusive<T>(
			this Range<T> range,
			[NotNull, InstantHandle] Func<T, T> fromValueSelector,
			[NotNull, InstantHandle] Func<T, T> toValueSelector)
		{
			if (range.IsEmpty || (!range.From.IsInclusiveBoundary && !range.To.IsInclusiveBoundary))
			{
				return range;
			}

			var from = range.From;
			if (from.IsInclusiveBoundary)
			{
				from = Range.BoundaryFromExclusive(fromValueSelector(from.GetValueOrDefault()));
			}
			var to = range.To;
			if (to.IsInclusiveBoundary)
			{
				to = Range.BoundaryToExclusive(toValueSelector(to.GetValueOrDefault()));
			}

			return range.TryCreateRange(from, to);
		}

		/// <summary>Creates a new range with the key specified.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <typeparam name="TKey2">The type of the new key.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="key">The value of the new key.</param>
		/// <returns>A new range with the key specified.</returns>
		[Pure, MethodImpl(AggressiveInlining)]
		public static Range<T, TKey2> WithKey<T, TKey2>(this Range<T> range, TKey2 key) =>
			Range.Create(range.From, range.To, key);
		#endregion

		#region Contains & HasIntersection
		/// <summary>Determines whether the range contains the specified value.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="value">The value to check.</param>
		/// <returns><c>true</c>, if the range contains the value.</returns>
		[Pure, MethodImpl(AggressiveInlining)]
		public static bool Contains<T>(this Range<T> range, T value) =>
			RangeBoundaryFrom<T>.IsValid(value)
				? Contains(range, Range.BoundaryFrom(value))
				: Contains(range, Range.BoundaryTo(value));

		/// <summary>Determines whether the range contains the specified range boundary.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="other">The boundary to check.</param>
		/// <returns><c>true</c>, if the range contains the boundary.</returns>
		[Pure, MethodImpl(AggressiveInlining)]
		public static bool Contains<T>(this Range<T> range, RangeBoundaryFrom<T> other)
		{
			if (range.IsEmpty)
			{
				return other.IsEmpty;
			}
			return other.IsNotEmpty && range.From <= other && range.To >= other;
		}

		/// <summary>Determines whether the range contains the specified range boundary.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="other">The boundary to check.</param>
		/// <returns><c>true</c>, if the range contains the boundary.</returns>
		[Pure, MethodImpl(AggressiveInlining)]
		public static bool Contains<T>(this Range<T> range, RangeBoundaryTo<T> other)
		{
			if (range.IsEmpty)
			{
				return other.IsEmpty;
			}
			return other.IsNotEmpty && range.From <= other && range.To >= other;
		}

		/// <summary>Determines whether the range contains another range.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="from">The boundary From value of the range to check.</param>
		/// <param name="to">The boundary To value of the range to check.</param>
		/// <returns><c>true</c>, if the range contains another range.</returns>
		[Pure, MethodImpl(AggressiveInlining)]
		public static bool Contains<T>(this Range<T> range, T from, T to) =>
			Contains(range, Range.Create(from, to));

		/// <summary>Determines whether the range contains another range.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="other">The range to check.</param>
		/// <returns><c>true</c>, if the range contains another range.</returns>
		// DONTTOUCH: The last parameter should be nongeneric to avoid overload resolution conflicts
		// WAITINGFOR: https://github.com/dotnet/roslyn/issues/250 (case 2)
		[Pure, MethodImpl(AggressiveInlining)]
		public static bool Contains<T, TRange>(this TRange range, Range<T> other)
			where TRange : IRange<T>
		{
			if (range.IsEmpty)
			{
				return other.IsEmpty;
			}
			return other.IsNotEmpty && range.From <= other.From && range.To >= other.To;
		}

		/// <summary>Determines whether the range has intersection with another range.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="from">The boundary From value of the range to check.</param>
		/// <param name="to">The boundary To value of the range to check.</param>
		/// <returns><c>true</c>, if the range has intersection with another range.</returns>
		[Pure, MethodImpl(AggressiveInlining)]
		public static bool HasIntersection<T>(this Range<T> range, T from, T to) =>
			HasIntersection(range, Range.Create(from, to));

		/// <summary>Determines whether the range has intersection with another range.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <typeparam name="TRange">The type of another range.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="other">The range to check.</param>
		/// <returns><c>true</c>, if the range has intersection with another range.</returns>
		[Pure, MethodImpl(AggressiveInlining)]
		public static bool HasIntersection<T, TRange>(this Range<T> range, TRange other)
			where TRange : IRange<T>
		{
			if (range.IsEmpty)
			{
				return other.IsEmpty;
			}
			return other.IsNotEmpty && range.From <= other.To && range.To >= other.From;
		}
		#endregion

		#region Adjust
		/// <summary>Ensures that the value fits into a range.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <param name="range">The range the value will be fitted to.</param>
		/// <param name="value">The value to be adjusted.</param>
		/// <exception cref="ArgumentException">The range is empty or any of its boundaries is exclusive.</exception>
		/// <returns>A new value that fits into a range specified</returns>
		[Pure]
		public static T Clamp<T>(this Range<T> range, T value)
		{
			Code.AssertArgument(
				range.IsNotEmpty, nameof(range), "Cannot fit the value into empty range.");
			Code.AssertArgument(
				!range.From.IsExclusiveBoundary, nameof(range), "The boundary From is exclusive and has no value.");
			Code.AssertArgument(
				!range.To.IsExclusiveBoundary, nameof(range), "The boundary To is exclusive and has no value.");

			// case for the positive infinity
			if (!RangeBoundaryFrom<T>.IsValid(value))
			{
				if (range.To < RangeBoundaryTo<T>.PositiveInfinity)
					return range.To.Value;
				return value;
			}

			if (range.From > value)
				return range.From.Value;

			if (range.To < value)
				return range.To.Value;

			return value;
		}
		#endregion

		#region StartsAfter & EndsBefore
		/// <summary>Determines whether the range starts after the value specified.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="value">The value to check.</param>
		/// <returns><c>true</c>, if the range starts after the value.</returns>
		[Pure, MethodImpl(AggressiveInlining)]
		public static bool StartsAfter<T>(this Range<T> range, T value) =>
			RangeBoundaryFrom<T>.IsValid(value) && range.From > Range.BoundaryFrom(value);

		/// <summary>Determines whether the range starts after the boundary specified.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="other">The boundary to check.</param>
		/// <returns><c>true</c>, if the range starts after the boundary.</returns>
		[Pure, MethodImpl(AggressiveInlining)]
		public static bool StartsAfter<T>(this Range<T> range, RangeBoundaryFrom<T> other) =>
			other.IsNotEmpty && range.From > other;

		/// <summary>Determines whether the range starts after the boundary specified.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="other">The boundary to check.</param>
		/// <returns><c>true</c>, if the range starts after the boundary.</returns>
		[Pure, MethodImpl(AggressiveInlining)]
		public static bool StartsAfter<T>(this Range<T> range, RangeBoundaryTo<T> other) =>
			other.IsNotEmpty && range.From > other;

		/// <summary>Determines whether the range starts after the range specified.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="other">The range to check.</param>
		/// <returns><c>true</c>, if the range starts after another range.</returns>
		// DONTTOUCH: The last parameter should be nongeneric to avoid overload resolution conflicts
		// WAITINGFOR: https://github.com/dotnet/roslyn/issues/250 (case 2)
		[Pure, MethodImpl(AggressiveInlining)]
		public static bool StartsAfter<T, TRange>(this TRange range, Range<T> other)
			where TRange : IRange<T> =>
				other.IsNotEmpty && range.From > other.To;

		/// <summary>Determines whether the range ends before the value specified.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="value">The value to check.</param>
		/// <returns><c>true</c>, if the range ends before the value.</returns>
		[Pure, MethodImpl(AggressiveInlining)]
		public static bool EndsBefore<T>(this Range<T> range, T value) =>
			range.IsNotEmpty && RangeBoundaryTo<T>.IsValid(value) && range.To < Range.BoundaryTo(value);

		/// <summary>Determines whether the range ends before the boundary specified.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="other">The boundary to check.</param>
		/// <returns><c>true</c>, if the range ends before the boundary.</returns>
		[Pure, MethodImpl(AggressiveInlining)]
		public static bool EndsBefore<T>(this Range<T> range, RangeBoundaryFrom<T> other) =>
			range.IsNotEmpty && other.IsNotEmpty && range.To < other;

		/// <summary>Determines whether the range ends before the boundary specified.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="other">The boundary to check.</param>
		/// <returns><c>true</c>, if the range ends before the boundary.</returns>
		[Pure, MethodImpl(AggressiveInlining)]
		public static bool EndsBefore<T>(this Range<T> range, RangeBoundaryTo<T> other) =>
			range.IsNotEmpty && other.IsNotEmpty && range.To < other;

		/// <summary>Determines whether the range ends before the range specified.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="other">The range to check.</param>
		/// <returns><c>true</c>, if the range ends before another range.</returns>
		// DONTTOUCH: The last parameter should be nongeneric to avoid overload resolution conflicts
		// WAITINGFOR: https://github.com/dotnet/roslyn/issues/250 (case 2)
		[Pure, MethodImpl(AggressiveInlining)]
		public static bool EndsBefore<T, TRange>(this TRange range, Range<T> other)
			where TRange : IRange<T> =>
				range.IsNotEmpty && other.IsNotEmpty && range.To < other.From;
		#endregion

		#region Union/Extend
		/// <summary>Returns a union range containing both of the ranges.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="from">The boundary From value.</param>
		/// <param name="to">The boundary To value.</param>
		/// <returns>A union range containing both of the ranges.</returns>
		[Pure, MethodImpl(AggressiveInlining)]
		public static Range<T> Union<T>(this Range<T> range, T from, T to) =>
			Union(range, Range.Create(from, to));

		/// <summary>Returns a union range containing both of the ranges.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <typeparam name="TRange">The type of another range.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="other">The range to union with.</param>
		/// <returns>A union range containing both of the ranges.</returns>
		[Pure, MethodImpl(AggressiveInlining)]
		public static Range<T> Union<T, TRange>(this Range<T> range, TRange other)
			where TRange : IRange<T>
		{
			if (range.IsEmpty)
				return range.TryCreateRange(other.From, other.To);

			if (other.IsEmpty)
				return range;

			return range.TryCreateRange(
				other.From >= range.From ? range.From : other.From,
				range.To >= other.To ? range.To : other.To);
		}

		/// <summary>Extends the range from the left.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="from">A new value From.</param>
		/// <returns>
		/// A range with a new From boundary or the source fange if the new boundary is greater than original.
		/// </returns>
		[Pure, MethodImpl(AggressiveInlining)]
		public static Range<T> ExtendFrom<T>(this Range<T> range, T from) =>
			ExtendFrom(range, Range.BoundaryFrom(from));

		/// <summary>Extends the range from the left.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="from">A new boundary From.</param>
		/// <returns>
		/// A range with a new From boundary or the source fange if the new boundary is greater than original.
		/// </returns>
		[Pure, MethodImpl(AggressiveInlining)]
		public static Range<T> ExtendFrom<T>(this Range<T> range, RangeBoundaryFrom<T> from)
		{
			if (range.IsEmpty || from.IsEmpty)
				return range;

			return range.From <= from
				? range
				: range.TryCreateRange(from, range.To);
		}

		/// <summary>Extends the range from the right.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="to">A new value To.</param>
		/// <returns>
		/// A range with a new To boundary or the source fange if the new boundary is less than original.
		/// </returns>
		[Pure, MethodImpl(AggressiveInlining)]
		public static Range<T> ExtendTo<T>(this Range<T> range, T to) =>
			ExtendTo(range, Range.BoundaryTo(to));

		/// <summary>Extends the range from the right.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="to">A new boundary To.</param>
		/// <returns>
		/// A range with a new To boundary or the source fange if the new boundary is less than original.
		/// </returns>
		[Pure, MethodImpl(AggressiveInlining)]
		public static Range<T> ExtendTo<T>(this Range<T> range, RangeBoundaryTo<T> to)
		{
			if (range.IsEmpty || to.IsEmpty)
				return range;

			return range.To >= to
				? range
				: range.TryCreateRange(range.From, to);
		}
		#endregion

		#region Intersect/Trim
		/// <summary>Returns an intersection of the the ranges.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="from">The boundary From value.</param>
		/// <param name="to">The boundary To value.</param>
		/// <returns>An intersection range or empty range if the ranges do not intersect.</returns>
		[Pure, MethodImpl(AggressiveInlining)]
		public static Range<T> Intersect<T>(this Range<T> range, T from, T to) =>
			Intersect(range, Range.Create(from, to));

		/// <summary>Returns an intersection of the the ranges.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <typeparam name="TRange">The type of another range.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="other">The range to intersect with.</param>
		/// <returns>An intersection range or empty range if the ranges do not intersect.</returns>
		[Pure, MethodImpl(AggressiveInlining)]
		public static Range<T> Intersect<T, TRange>(this Range<T> range, TRange other)
			where TRange : IRange<T> =>
				range.TryCreateRange(
					(range.IsEmpty || range.From >= other.From) ? range.From : other.From,
					range.To <= other.To ? range.To : other.To);

		/// <summary>Trims the range from the left.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="from">A new value From.</param>
		/// <returns>
		/// A range with a new From boundary
		/// or the source fange if the new boundary is less than original
		/// or an empty range if the new From boundary is greater than To boundary of the range.
		/// </returns>
		[Pure, MethodImpl(AggressiveInlining)]
		public static Range<T> TrimFrom<T>(this Range<T> range, T from) =>
			TrimFrom(range, Range.BoundaryFrom(from));

		/// <summary>Trims the range from the left.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="from">A new boundary From.</param>
		/// <returns>
		/// A range with a new From boundary
		/// or the source fange if the new boundary is less than original
		/// or an empty range if the new From boundary is greater than To boundary of the range.
		/// </returns>
		[Pure, MethodImpl(AggressiveInlining)]
		public static Range<T> TrimFrom<T>(this Range<T> range, RangeBoundaryFrom<T> from) =>
			from.IsNotEmpty && range.From >= from
				? range
				: range.TryCreateRange(from, range.To);

		/// <summary>Trims the range from the right.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="to">A new value To.</param>
		/// <returns>
		/// A range with a new To boundary
		/// or the source fange if the new boundary is greater than original
		/// or an empty range if the new To boundary is less than From boundary of the range.
		/// </returns>
		[Pure, MethodImpl(AggressiveInlining)]
		public static Range<T> TrimTo<T>(this Range<T> range, T to) =>
			TrimTo(range, Range.BoundaryTo(to));

		/// <summary>Trims the range from the right.</summary>
		/// <typeparam name="T">The type of the range values.</typeparam>
		/// <param name="range">The source range.</param>
		/// <param name="to">A new boundary To.</param>
		/// <returns>
		/// A range with a new To boundary
		/// or the source fange if the new boundary is greater than original
		/// or an empty range if the new To boundary is less than From boundary of the range.
		/// </returns>
		[Pure, MethodImpl(AggressiveInlining)]
		public static Range<T> TrimTo<T>(this Range<T> range, RangeBoundaryTo<T> to) =>
			to.IsNotEmpty && range.To <= to
				? range
				: range.TryCreateRange(range.From, to);
		#endregion
	}
}