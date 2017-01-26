﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Globalization;

using JetBrains.Annotations;

namespace CodeJam.Strings
{
	partial class StringExtensions
	{
		#region byte
		/// <summary>
		/// Converts the string representation of a number in a specified style and culture-specific format to its
		/// <see cref="byte"/> equivalent. A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="str">
		/// A string containing a number to convert. The string is interpreted using the style specified by
		/// <paramref name="numberStyle"/>.
		/// </param>
		/// <param name="numberStyle">
		/// A bitwise combination of enumeration values that indicates the style elements that can be present in
		/// <paramref name="str"/>. Default value is Integer.
		/// </param>
		/// <param name="provider">
		/// An object that supplies culture-specific formatting information about <paramref name="str"/>.
		/// </param>
		/// <returns>
		/// When this method returns, contains the <see cref="byte"/> value equivalent of the number contained in
		/// <paramref name="str"/>, if the conversion succeeded, or null if the conversion failed. The conversion fails if
		/// the <paramref name="str"/> parameter is null or String.Empty, is not in a format compliant withstyle, or
		/// represents a number less than <see cref="byte.MinValue"/> or greater than <see cref="byte.MaxValue"/>.
		/// </returns>
		[Pure]
		public static byte? ToByte(
			[CanBeNull] this string str,
			NumberStyles numberStyle = NumberStyles.Integer,
			[CanBeNull] IFormatProvider provider = null)
		{
			byte result;
			return byte.TryParse(str, numberStyle, provider, out result) ? (byte?)result : null;
		}

		/// <summary>
		/// Converts the string representation of a number in a specified style and culture-invariant format to its
		/// <see cref="byte"/> equivalent. A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="str">
		/// A string containing a number to convert. The string is interpreted using the style specified by
		/// <paramref name="numberStyle"/>.
		/// </param>
		/// <param name="numberStyle">
		/// A bitwise combination of enumeration values that indicates the style elements that can be present in
		/// <paramref name="str"/>. Default value is Integer.
		/// </param>
		/// <returns>
		/// When this method returns, contains the <see cref="byte"/> value equivalent of the number contained in
		/// <paramref name="str"/>, if the conversion succeeded, or null if the conversion failed. The conversion fails if
		/// the <paramref name="str"/> parameter is null or String.Empty, is not in a format compliant withstyle, or
		/// represents a number less than <see cref="byte.MinValue"/> or greater than <see cref="byte.MaxValue"/>.
		/// </returns>
		[Pure]
		public static byte? ToByteInvariant(
			[CanBeNull] this string str,
			NumberStyles numberStyle = NumberStyles.Integer)
		{
			byte result;
			return byte.TryParse(str, numberStyle,  CultureInfo.InvariantCulture, out result) ? (byte?)result : null;
		}
		#endregion

		#region sbyte
		/// <summary>
		/// Converts the string representation of a number in a specified style and culture-specific format to its
		/// <see cref="sbyte"/> equivalent. A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="str">
		/// A string containing a number to convert. The string is interpreted using the style specified by
		/// <paramref name="numberStyle"/>.
		/// </param>
		/// <param name="numberStyle">
		/// A bitwise combination of enumeration values that indicates the style elements that can be present in
		/// <paramref name="str"/>. Default value is Integer.
		/// </param>
		/// <param name="provider">
		/// An object that supplies culture-specific formatting information about <paramref name="str"/>.
		/// </param>
		/// <returns>
		/// When this method returns, contains the <see cref="sbyte"/> value equivalent of the number contained in
		/// <paramref name="str"/>, if the conversion succeeded, or null if the conversion failed. The conversion fails if
		/// the <paramref name="str"/> parameter is null or String.Empty, is not in a format compliant withstyle, or
		/// represents a number less than <see cref="sbyte.MinValue"/> or greater than <see cref="sbyte.MaxValue"/>.
		/// </returns>
		[Pure]
		public static sbyte? ToSByte(
			[CanBeNull] this string str,
			NumberStyles numberStyle = NumberStyles.Integer,
			[CanBeNull] IFormatProvider provider = null)
		{
			sbyte result;
			return sbyte.TryParse(str, numberStyle, provider, out result) ? (sbyte?)result : null;
		}

		/// <summary>
		/// Converts the string representation of a number in a specified style and culture-invariant format to its
		/// <see cref="sbyte"/> equivalent. A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="str">
		/// A string containing a number to convert. The string is interpreted using the style specified by
		/// <paramref name="numberStyle"/>.
		/// </param>
		/// <param name="numberStyle">
		/// A bitwise combination of enumeration values that indicates the style elements that can be present in
		/// <paramref name="str"/>. Default value is Integer.
		/// </param>
		/// <returns>
		/// When this method returns, contains the <see cref="sbyte"/> value equivalent of the number contained in
		/// <paramref name="str"/>, if the conversion succeeded, or null if the conversion failed. The conversion fails if
		/// the <paramref name="str"/> parameter is null or String.Empty, is not in a format compliant withstyle, or
		/// represents a number less than <see cref="sbyte.MinValue"/> or greater than <see cref="sbyte.MaxValue"/>.
		/// </returns>
		[Pure]
		public static sbyte? ToSByteInvariant(
			[CanBeNull] this string str,
			NumberStyles numberStyle = NumberStyles.Integer)
		{
			sbyte result;
			return sbyte.TryParse(str, numberStyle,  CultureInfo.InvariantCulture, out result) ? (sbyte?)result : null;
		}
		#endregion

		#region short
		/// <summary>
		/// Converts the string representation of a number in a specified style and culture-specific format to its
		/// <see cref="short"/> equivalent. A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="str">
		/// A string containing a number to convert. The string is interpreted using the style specified by
		/// <paramref name="numberStyle"/>.
		/// </param>
		/// <param name="numberStyle">
		/// A bitwise combination of enumeration values that indicates the style elements that can be present in
		/// <paramref name="str"/>. Default value is Number.
		/// </param>
		/// <param name="provider">
		/// An object that supplies culture-specific formatting information about <paramref name="str"/>.
		/// </param>
		/// <returns>
		/// When this method returns, contains the <see cref="short"/> value equivalent of the number contained in
		/// <paramref name="str"/>, if the conversion succeeded, or null if the conversion failed. The conversion fails if
		/// the <paramref name="str"/> parameter is null or String.Empty, is not in a format compliant withstyle, or
		/// represents a number less than <see cref="short.MinValue"/> or greater than <see cref="short.MaxValue"/>.
		/// </returns>
		[Pure]
		public static short? ToInt16(
			[CanBeNull] this string str,
			NumberStyles numberStyle = NumberStyles.Number,
			[CanBeNull] IFormatProvider provider = null)
		{
			short result;
			return short.TryParse(str, numberStyle, provider, out result) ? (short?)result : null;
		}

		/// <summary>
		/// Converts the string representation of a number in a specified style and culture-invariant format to its
		/// <see cref="short"/> equivalent. A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="str">
		/// A string containing a number to convert. The string is interpreted using the style specified by
		/// <paramref name="numberStyle"/>.
		/// </param>
		/// <param name="numberStyle">
		/// A bitwise combination of enumeration values that indicates the style elements that can be present in
		/// <paramref name="str"/>. Default value is Number.
		/// </param>
		/// <returns>
		/// When this method returns, contains the <see cref="short"/> value equivalent of the number contained in
		/// <paramref name="str"/>, if the conversion succeeded, or null if the conversion failed. The conversion fails if
		/// the <paramref name="str"/> parameter is null or String.Empty, is not in a format compliant withstyle, or
		/// represents a number less than <see cref="short.MinValue"/> or greater than <see cref="short.MaxValue"/>.
		/// </returns>
		[Pure]
		public static short? ToInt16Invariant(
			[CanBeNull] this string str,
			NumberStyles numberStyle = NumberStyles.Number)
		{
			short result;
			return short.TryParse(str, numberStyle,  CultureInfo.InvariantCulture, out result) ? (short?)result : null;
		}
		#endregion

		#region ushort
		/// <summary>
		/// Converts the string representation of a number in a specified style and culture-specific format to its
		/// <see cref="ushort"/> equivalent. A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="str">
		/// A string containing a number to convert. The string is interpreted using the style specified by
		/// <paramref name="numberStyle"/>.
		/// </param>
		/// <param name="numberStyle">
		/// A bitwise combination of enumeration values that indicates the style elements that can be present in
		/// <paramref name="str"/>. Default value is Integer.
		/// </param>
		/// <param name="provider">
		/// An object that supplies culture-specific formatting information about <paramref name="str"/>.
		/// </param>
		/// <returns>
		/// When this method returns, contains the <see cref="ushort"/> value equivalent of the number contained in
		/// <paramref name="str"/>, if the conversion succeeded, or null if the conversion failed. The conversion fails if
		/// the <paramref name="str"/> parameter is null or String.Empty, is not in a format compliant withstyle, or
		/// represents a number less than <see cref="ushort.MinValue"/> or greater than <see cref="ushort.MaxValue"/>.
		/// </returns>
		[Pure]
		public static ushort? ToUInt16(
			[CanBeNull] this string str,
			NumberStyles numberStyle = NumberStyles.Integer,
			[CanBeNull] IFormatProvider provider = null)
		{
			ushort result;
			return ushort.TryParse(str, numberStyle, provider, out result) ? (ushort?)result : null;
		}

		/// <summary>
		/// Converts the string representation of a number in a specified style and culture-invariant format to its
		/// <see cref="ushort"/> equivalent. A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="str">
		/// A string containing a number to convert. The string is interpreted using the style specified by
		/// <paramref name="numberStyle"/>.
		/// </param>
		/// <param name="numberStyle">
		/// A bitwise combination of enumeration values that indicates the style elements that can be present in
		/// <paramref name="str"/>. Default value is Integer.
		/// </param>
		/// <returns>
		/// When this method returns, contains the <see cref="ushort"/> value equivalent of the number contained in
		/// <paramref name="str"/>, if the conversion succeeded, or null if the conversion failed. The conversion fails if
		/// the <paramref name="str"/> parameter is null or String.Empty, is not in a format compliant withstyle, or
		/// represents a number less than <see cref="ushort.MinValue"/> or greater than <see cref="ushort.MaxValue"/>.
		/// </returns>
		[Pure]
		public static ushort? ToUInt16Invariant(
			[CanBeNull] this string str,
			NumberStyles numberStyle = NumberStyles.Integer)
		{
			ushort result;
			return ushort.TryParse(str, numberStyle,  CultureInfo.InvariantCulture, out result) ? (ushort?)result : null;
		}
		#endregion

		#region int
		/// <summary>
		/// Converts the string representation of a number in a specified style and culture-specific format to its
		/// <see cref="int"/> equivalent. A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="str">
		/// A string containing a number to convert. The string is interpreted using the style specified by
		/// <paramref name="numberStyle"/>.
		/// </param>
		/// <param name="numberStyle">
		/// A bitwise combination of enumeration values that indicates the style elements that can be present in
		/// <paramref name="str"/>. Default value is Integer.
		/// </param>
		/// <param name="provider">
		/// An object that supplies culture-specific formatting information about <paramref name="str"/>.
		/// </param>
		/// <returns>
		/// When this method returns, contains the <see cref="int"/> value equivalent of the number contained in
		/// <paramref name="str"/>, if the conversion succeeded, or null if the conversion failed. The conversion fails if
		/// the <paramref name="str"/> parameter is null or String.Empty, is not in a format compliant withstyle, or
		/// represents a number less than <see cref="int.MinValue"/> or greater than <see cref="int.MaxValue"/>.
		/// </returns>
		[Pure]
		public static int? ToInt32(
			[CanBeNull] this string str,
			NumberStyles numberStyle = NumberStyles.Integer,
			[CanBeNull] IFormatProvider provider = null)
		{
			int result;
			return int.TryParse(str, numberStyle, provider, out result) ? (int?)result : null;
		}

		/// <summary>
		/// Converts the string representation of a number in a specified style and culture-invariant format to its
		/// <see cref="int"/> equivalent. A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="str">
		/// A string containing a number to convert. The string is interpreted using the style specified by
		/// <paramref name="numberStyle"/>.
		/// </param>
		/// <param name="numberStyle">
		/// A bitwise combination of enumeration values that indicates the style elements that can be present in
		/// <paramref name="str"/>. Default value is Integer.
		/// </param>
		/// <returns>
		/// When this method returns, contains the <see cref="int"/> value equivalent of the number contained in
		/// <paramref name="str"/>, if the conversion succeeded, or null if the conversion failed. The conversion fails if
		/// the <paramref name="str"/> parameter is null or String.Empty, is not in a format compliant withstyle, or
		/// represents a number less than <see cref="int.MinValue"/> or greater than <see cref="int.MaxValue"/>.
		/// </returns>
		[Pure]
		public static int? ToInt32Invariant(
			[CanBeNull] this string str,
			NumberStyles numberStyle = NumberStyles.Integer)
		{
			int result;
			return int.TryParse(str, numberStyle,  CultureInfo.InvariantCulture, out result) ? (int?)result : null;
		}
		#endregion

		#region uint
		/// <summary>
		/// Converts the string representation of a number in a specified style and culture-specific format to its
		/// <see cref="uint"/> equivalent. A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="str">
		/// A string containing a number to convert. The string is interpreted using the style specified by
		/// <paramref name="numberStyle"/>.
		/// </param>
		/// <param name="numberStyle">
		/// A bitwise combination of enumeration values that indicates the style elements that can be present in
		/// <paramref name="str"/>. Default value is Integer.
		/// </param>
		/// <param name="provider">
		/// An object that supplies culture-specific formatting information about <paramref name="str"/>.
		/// </param>
		/// <returns>
		/// When this method returns, contains the <see cref="uint"/> value equivalent of the number contained in
		/// <paramref name="str"/>, if the conversion succeeded, or null if the conversion failed. The conversion fails if
		/// the <paramref name="str"/> parameter is null or String.Empty, is not in a format compliant withstyle, or
		/// represents a number less than <see cref="uint.MinValue"/> or greater than <see cref="uint.MaxValue"/>.
		/// </returns>
		[Pure]
		public static uint? ToUInt32(
			[CanBeNull] this string str,
			NumberStyles numberStyle = NumberStyles.Integer,
			[CanBeNull] IFormatProvider provider = null)
		{
			uint result;
			return uint.TryParse(str, numberStyle, provider, out result) ? (uint?)result : null;
		}

		/// <summary>
		/// Converts the string representation of a number in a specified style and culture-invariant format to its
		/// <see cref="uint"/> equivalent. A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="str">
		/// A string containing a number to convert. The string is interpreted using the style specified by
		/// <paramref name="numberStyle"/>.
		/// </param>
		/// <param name="numberStyle">
		/// A bitwise combination of enumeration values that indicates the style elements that can be present in
		/// <paramref name="str"/>. Default value is Integer.
		/// </param>
		/// <returns>
		/// When this method returns, contains the <see cref="uint"/> value equivalent of the number contained in
		/// <paramref name="str"/>, if the conversion succeeded, or null if the conversion failed. The conversion fails if
		/// the <paramref name="str"/> parameter is null or String.Empty, is not in a format compliant withstyle, or
		/// represents a number less than <see cref="uint.MinValue"/> or greater than <see cref="uint.MaxValue"/>.
		/// </returns>
		[Pure]
		public static uint? ToUInt32Invariant(
			[CanBeNull] this string str,
			NumberStyles numberStyle = NumberStyles.Integer)
		{
			uint result;
			return uint.TryParse(str, numberStyle,  CultureInfo.InvariantCulture, out result) ? (uint?)result : null;
		}
		#endregion

		#region long
		/// <summary>
		/// Converts the string representation of a number in a specified style and culture-specific format to its
		/// <see cref="long"/> equivalent. A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="str">
		/// A string containing a number to convert. The string is interpreted using the style specified by
		/// <paramref name="numberStyle"/>.
		/// </param>
		/// <param name="numberStyle">
		/// A bitwise combination of enumeration values that indicates the style elements that can be present in
		/// <paramref name="str"/>. Default value is Integer.
		/// </param>
		/// <param name="provider">
		/// An object that supplies culture-specific formatting information about <paramref name="str"/>.
		/// </param>
		/// <returns>
		/// When this method returns, contains the <see cref="long"/> value equivalent of the number contained in
		/// <paramref name="str"/>, if the conversion succeeded, or null if the conversion failed. The conversion fails if
		/// the <paramref name="str"/> parameter is null or String.Empty, is not in a format compliant withstyle, or
		/// represents a number less than <see cref="long.MinValue"/> or greater than <see cref="long.MaxValue"/>.
		/// </returns>
		[Pure]
		public static long? ToInt64(
			[CanBeNull] this string str,
			NumberStyles numberStyle = NumberStyles.Integer,
			[CanBeNull] IFormatProvider provider = null)
		{
			long result;
			return long.TryParse(str, numberStyle, provider, out result) ? (long?)result : null;
		}

		/// <summary>
		/// Converts the string representation of a number in a specified style and culture-invariant format to its
		/// <see cref="long"/> equivalent. A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="str">
		/// A string containing a number to convert. The string is interpreted using the style specified by
		/// <paramref name="numberStyle"/>.
		/// </param>
		/// <param name="numberStyle">
		/// A bitwise combination of enumeration values that indicates the style elements that can be present in
		/// <paramref name="str"/>. Default value is Integer.
		/// </param>
		/// <returns>
		/// When this method returns, contains the <see cref="long"/> value equivalent of the number contained in
		/// <paramref name="str"/>, if the conversion succeeded, or null if the conversion failed. The conversion fails if
		/// the <paramref name="str"/> parameter is null or String.Empty, is not in a format compliant withstyle, or
		/// represents a number less than <see cref="long.MinValue"/> or greater than <see cref="long.MaxValue"/>.
		/// </returns>
		[Pure]
		public static long? ToInt64Invariant(
			[CanBeNull] this string str,
			NumberStyles numberStyle = NumberStyles.Integer)
		{
			long result;
			return long.TryParse(str, numberStyle,  CultureInfo.InvariantCulture, out result) ? (long?)result : null;
		}
		#endregion

		#region ulong
		/// <summary>
		/// Converts the string representation of a number in a specified style and culture-specific format to its
		/// <see cref="ulong"/> equivalent. A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="str">
		/// A string containing a number to convert. The string is interpreted using the style specified by
		/// <paramref name="numberStyle"/>.
		/// </param>
		/// <param name="numberStyle">
		/// A bitwise combination of enumeration values that indicates the style elements that can be present in
		/// <paramref name="str"/>. Default value is Integer.
		/// </param>
		/// <param name="provider">
		/// An object that supplies culture-specific formatting information about <paramref name="str"/>.
		/// </param>
		/// <returns>
		/// When this method returns, contains the <see cref="ulong"/> value equivalent of the number contained in
		/// <paramref name="str"/>, if the conversion succeeded, or null if the conversion failed. The conversion fails if
		/// the <paramref name="str"/> parameter is null or String.Empty, is not in a format compliant withstyle, or
		/// represents a number less than <see cref="ulong.MinValue"/> or greater than <see cref="ulong.MaxValue"/>.
		/// </returns>
		[Pure]
		public static ulong? ToUInt64(
			[CanBeNull] this string str,
			NumberStyles numberStyle = NumberStyles.Integer,
			[CanBeNull] IFormatProvider provider = null)
		{
			ulong result;
			return ulong.TryParse(str, numberStyle, provider, out result) ? (ulong?)result : null;
		}

		/// <summary>
		/// Converts the string representation of a number in a specified style and culture-invariant format to its
		/// <see cref="ulong"/> equivalent. A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="str">
		/// A string containing a number to convert. The string is interpreted using the style specified by
		/// <paramref name="numberStyle"/>.
		/// </param>
		/// <param name="numberStyle">
		/// A bitwise combination of enumeration values that indicates the style elements that can be present in
		/// <paramref name="str"/>. Default value is Integer.
		/// </param>
		/// <returns>
		/// When this method returns, contains the <see cref="ulong"/> value equivalent of the number contained in
		/// <paramref name="str"/>, if the conversion succeeded, or null if the conversion failed. The conversion fails if
		/// the <paramref name="str"/> parameter is null or String.Empty, is not in a format compliant withstyle, or
		/// represents a number less than <see cref="ulong.MinValue"/> or greater than <see cref="ulong.MaxValue"/>.
		/// </returns>
		[Pure]
		public static ulong? ToUInt64Invariant(
			[CanBeNull] this string str,
			NumberStyles numberStyle = NumberStyles.Integer)
		{
			ulong result;
			return ulong.TryParse(str, numberStyle,  CultureInfo.InvariantCulture, out result) ? (ulong?)result : null;
		}
		#endregion

		#region float
		/// <summary>
		/// Converts the string representation of a number in a specified style and culture-specific format to its
		/// <see cref="float"/> equivalent. A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="str">
		/// A string containing a number to convert. The string is interpreted using the style specified by
		/// <paramref name="numberStyle"/>.
		/// </param>
		/// <param name="numberStyle">
		/// A bitwise combination of enumeration values that indicates the style elements that can be present in
		/// <paramref name="str"/>. Default value is Float.
		/// </param>
		/// <param name="provider">
		/// An object that supplies culture-specific formatting information about <paramref name="str"/>.
		/// </param>
		/// <returns>
		/// When this method returns, contains the <see cref="float"/> value equivalent of the number contained in
		/// <paramref name="str"/>, if the conversion succeeded, or null if the conversion failed. The conversion fails if
		/// the <paramref name="str"/> parameter is null or String.Empty, is not in a format compliant withstyle, or
		/// represents a number less than <see cref="float.MinValue"/> or greater than <see cref="float.MaxValue"/>.
		/// </returns>
		[Pure]
		public static float? ToSingle(
			[CanBeNull] this string str,
			NumberStyles numberStyle = NumberStyles.Float,
			[CanBeNull] IFormatProvider provider = null)
		{
			float result;
			return float.TryParse(str, numberStyle, provider, out result) ? (float?)result : null;
		}

		/// <summary>
		/// Converts the string representation of a number in a specified style and culture-invariant format to its
		/// <see cref="float"/> equivalent. A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="str">
		/// A string containing a number to convert. The string is interpreted using the style specified by
		/// <paramref name="numberStyle"/>.
		/// </param>
		/// <param name="numberStyle">
		/// A bitwise combination of enumeration values that indicates the style elements that can be present in
		/// <paramref name="str"/>. Default value is Float.
		/// </param>
		/// <returns>
		/// When this method returns, contains the <see cref="float"/> value equivalent of the number contained in
		/// <paramref name="str"/>, if the conversion succeeded, or null if the conversion failed. The conversion fails if
		/// the <paramref name="str"/> parameter is null or String.Empty, is not in a format compliant withstyle, or
		/// represents a number less than <see cref="float.MinValue"/> or greater than <see cref="float.MaxValue"/>.
		/// </returns>
		[Pure]
		public static float? ToSingleInvariant(
			[CanBeNull] this string str,
			NumberStyles numberStyle = NumberStyles.Float)
		{
			float result;
			return float.TryParse(str, numberStyle,  CultureInfo.InvariantCulture, out result) ? (float?)result : null;
		}
		#endregion

		#region double
		/// <summary>
		/// Converts the string representation of a number in a specified style and culture-specific format to its
		/// <see cref="double"/> equivalent. A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="str">
		/// A string containing a number to convert. The string is interpreted using the style specified by
		/// <paramref name="numberStyle"/>.
		/// </param>
		/// <param name="numberStyle">
		/// A bitwise combination of enumeration values that indicates the style elements that can be present in
		/// <paramref name="str"/>. Default value is Float.
		/// </param>
		/// <param name="provider">
		/// An object that supplies culture-specific formatting information about <paramref name="str"/>.
		/// </param>
		/// <returns>
		/// When this method returns, contains the <see cref="double"/> value equivalent of the number contained in
		/// <paramref name="str"/>, if the conversion succeeded, or null if the conversion failed. The conversion fails if
		/// the <paramref name="str"/> parameter is null or String.Empty, is not in a format compliant withstyle, or
		/// represents a number less than <see cref="double.MinValue"/> or greater than <see cref="double.MaxValue"/>.
		/// </returns>
		[Pure]
		public static double? ToDouble(
			[CanBeNull] this string str,
			NumberStyles numberStyle = NumberStyles.Float,
			[CanBeNull] IFormatProvider provider = null)
		{
			double result;
			return double.TryParse(str, numberStyle, provider, out result) ? (double?)result : null;
		}

		/// <summary>
		/// Converts the string representation of a number in a specified style and culture-invariant format to its
		/// <see cref="double"/> equivalent. A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="str">
		/// A string containing a number to convert. The string is interpreted using the style specified by
		/// <paramref name="numberStyle"/>.
		/// </param>
		/// <param name="numberStyle">
		/// A bitwise combination of enumeration values that indicates the style elements that can be present in
		/// <paramref name="str"/>. Default value is Float.
		/// </param>
		/// <returns>
		/// When this method returns, contains the <see cref="double"/> value equivalent of the number contained in
		/// <paramref name="str"/>, if the conversion succeeded, or null if the conversion failed. The conversion fails if
		/// the <paramref name="str"/> parameter is null or String.Empty, is not in a format compliant withstyle, or
		/// represents a number less than <see cref="double.MinValue"/> or greater than <see cref="double.MaxValue"/>.
		/// </returns>
		[Pure]
		public static double? ToDoubleInvariant(
			[CanBeNull] this string str,
			NumberStyles numberStyle = NumberStyles.Float)
		{
			double result;
			return double.TryParse(str, numberStyle,  CultureInfo.InvariantCulture, out result) ? (double?)result : null;
		}
		#endregion

		#region decimal
		/// <summary>
		/// Converts the string representation of a number in a specified style and culture-specific format to its
		/// <see cref="decimal"/> equivalent. A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="str">
		/// A string containing a number to convert. The string is interpreted using the style specified by
		/// <paramref name="numberStyle"/>.
		/// </param>
		/// <param name="numberStyle">
		/// A bitwise combination of enumeration values that indicates the style elements that can be present in
		/// <paramref name="str"/>. Default value is Number.
		/// </param>
		/// <param name="provider">
		/// An object that supplies culture-specific formatting information about <paramref name="str"/>.
		/// </param>
		/// <returns>
		/// When this method returns, contains the <see cref="decimal"/> value equivalent of the number contained in
		/// <paramref name="str"/>, if the conversion succeeded, or null if the conversion failed. The conversion fails if
		/// the <paramref name="str"/> parameter is null or String.Empty, is not in a format compliant withstyle, or
		/// represents a number less than <see cref="decimal.MinValue"/> or greater than <see cref="decimal.MaxValue"/>.
		/// </returns>
		[Pure]
		public static decimal? ToDecimal(
			[CanBeNull] this string str,
			NumberStyles numberStyle = NumberStyles.Number,
			[CanBeNull] IFormatProvider provider = null)
		{
			decimal result;
			return decimal.TryParse(str, numberStyle, provider, out result) ? (decimal?)result : null;
		}

		/// <summary>
		/// Converts the string representation of a number in a specified style and culture-invariant format to its
		/// <see cref="decimal"/> equivalent. A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="str">
		/// A string containing a number to convert. The string is interpreted using the style specified by
		/// <paramref name="numberStyle"/>.
		/// </param>
		/// <param name="numberStyle">
		/// A bitwise combination of enumeration values that indicates the style elements that can be present in
		/// <paramref name="str"/>. Default value is Number.
		/// </param>
		/// <returns>
		/// When this method returns, contains the <see cref="decimal"/> value equivalent of the number contained in
		/// <paramref name="str"/>, if the conversion succeeded, or null if the conversion failed. The conversion fails if
		/// the <paramref name="str"/> parameter is null or String.Empty, is not in a format compliant withstyle, or
		/// represents a number less than <see cref="decimal.MinValue"/> or greater than <see cref="decimal.MaxValue"/>.
		/// </returns>
		[Pure]
		public static decimal? ToDecimalInvariant(
			[CanBeNull] this string str,
			NumberStyles numberStyle = NumberStyles.Number)
		{
			decimal result;
			return decimal.TryParse(str, numberStyle,  CultureInfo.InvariantCulture, out result) ? (decimal?)result : null;
		}
		#endregion
	}
}