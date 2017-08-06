﻿using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

using JetBrains.Annotations;

using static CodeJam.CodeExceptions;
using static CodeJam.PlatformDependent;

namespace CodeJam.IO
{
	/// <summary>IO exception factory class</summary>
	[PublicAPI]
	public static class IoCodeExceptions
	{
		#region Path
		/// <summary>Creates <seealso cref="ArgumentException"/> for invalid path.</summary>
		/// <param name="argumentName">Name of the argument.</param>
		/// <param name="path">The path being checked.</param>
		/// <returns>Initialized instance of <see cref="ArgumentException"/>.</returns>
		[DebuggerHidden, NotNull, MethodImpl(AggressiveInlining)]
		[StringFormatMethod("messageFormat")]
		[MustUseReturnValue]
		public static ArgumentException ArgumentNotWellFormedPath(
			[NotNull, InvokerParameterName] string argumentName,
			[NotNull] string path)
		{
			BreakIfAttached();
			return new ArgumentException(
				argumentName,
				$"The path '{path}' is not a valid absolute or not-rooted relative path.")
				.LogToCodeTraceSourceBeforeThrow();
		}

		/// <summary>Creates <seealso cref="ArgumentException"/> for invalid full path.</summary>
		/// <param name="argumentName">Name of the argument.</param>
		/// <param name="path">The path being checked.</param>
		/// <returns>Initialized instance of <see cref="ArgumentException"/>.</returns>
		[DebuggerHidden, NotNull, MethodImpl(AggressiveInlining)]
		[StringFormatMethod("messageFormat")]
		[MustUseReturnValue]
		public static ArgumentException ArgumentNotWellFormedAbsolutePath(
			[NotNull, InvokerParameterName] string argumentName,
			[NotNull] string path)
		{
			BreakIfAttached();
			return new ArgumentException(
				argumentName,
				$"The path '{path}' is not a valid full path.")
				.LogToCodeTraceSourceBeforeThrow();
		}

		/// <summary>Creates <seealso cref="ArgumentException"/> for invalid relative path.</summary>
		/// <param name="argumentName">Name of the argument.</param>
		/// <param name="path">The path being checked.</param>
		/// <returns>Initialized instance of <see cref="ArgumentException"/>.</returns>
		[DebuggerHidden, NotNull, MethodImpl(AggressiveInlining)]
		[StringFormatMethod("messageFormat")]
		[MustUseReturnValue]
		public static ArgumentException ArgumentRootedOrNotRelativePath(
			[NotNull, InvokerParameterName] string argumentName,
			[NotNull] string path)
		{
			BreakIfAttached();
			return new ArgumentException(
				argumentName,
				$"The path '{path}' is not a valid not-rooted relative path.")
				.LogToCodeTraceSourceBeforeThrow();
		}

		/// <summary>Creates <seealso cref="ArgumentException"/> for invalid simple name.</summary>
		/// <param name="argumentName">Name of the argument.</param>
		/// <param name="path">The path being checked.</param>
		/// <returns>Initialized instance of <see cref="ArgumentException"/>.</returns>
		[DebuggerHidden, NotNull, MethodImpl(AggressiveInlining)]
		[StringFormatMethod("messageFormat")]
		[MustUseReturnValue]
		public static ArgumentException ArgumentNotSimpleName(
			[NotNull, InvokerParameterName] string argumentName,
			[NotNull] string path)
		{
			BreakIfAttached();
			return new ArgumentException(
				argumentName,
				$"The path '{path}' is not a valid name of the file or a directory.")
				.LogToCodeTraceSourceBeforeThrow();
		}

		/// <summary>Creates <seealso cref="ArgumentException"/> if path does not ends with one of path separator chars.</summary>
		/// <param name="argumentName">Name of the argument.</param>
		/// <param name="path">The path being checked.</param>
		/// <returns>Initialized instance of <see cref="ArgumentException"/>.</returns>
		[DebuggerHidden, NotNull, MethodImpl(AggressiveInlining)]
		[StringFormatMethod("messageFormat")]
		[MustUseReturnValue]
		public static ArgumentException ArgumentNotVolumeOrDirectoryPath(
			[NotNull, InvokerParameterName] string argumentName,
			[NotNull] string path)
		{
			BreakIfAttached();
			return new ArgumentException(
				argumentName,
				$"The path '{path}' should end with volume or directory separator chars.")
				.LogToCodeTraceSourceBeforeThrow();
		}
		#endregion

		#region File / Directory
		/// <summary>Creates <seealso cref="FileNotFoundException" /> for missing file when there is a dictionary.</summary>
		/// <param name="argumentName">Name of the argument.</param>
		/// <param name="filePath">The file being checked.</param>
		/// <returns>Initialized instance of <seealso cref="FileNotFoundException" />.</returns>
		[DebuggerHidden, NotNull, MethodImpl(AggressiveInlining)]
		[MustUseReturnValue]
		public static FileNotFoundException ArgumentDirectoryExistsFileExpected(
			[NotNull, InvokerParameterName] string argumentName,
			[NotNull] string filePath)
		{
			BreakIfAttached();
			return new FileNotFoundException(
				$"Argument {argumentName}. The path {filePath} is a directory, not a file.", filePath)
				.LogToCodeTraceSourceBeforeThrow();
		}

		/// <summary>Creates <seealso cref="DirectoryNotFoundException" /> for missing directory when there is a file.</summary>
		/// <param name="argumentName">Name of the argument.</param>
		/// <param name="directoryPath">The directory being checked.</param>
		/// <returns>Initialized instance of <seealso cref="DirectoryNotFoundException" />.</returns>
		[DebuggerHidden, NotNull, MethodImpl(AggressiveInlining)]
		[MustUseReturnValue]
		public static DirectoryNotFoundException ArgumentFileExistsDirectoryExpected(
			[NotNull, InvokerParameterName] string argumentName,
			[NotNull] string directoryPath)
		{
			BreakIfAttached();
			return new DirectoryNotFoundException(
				$"Argument {argumentName}. The path {directoryPath} is a file, not a directory.")
				.LogToCodeTraceSourceBeforeThrow();
		}

		/// <summary>Creates <seealso cref="FileNotFoundException" /> for missing file.</summary>
		/// <param name="argumentName">Name of the argument.</param>
		/// <param name="filePath">The file being checked.</param>
		/// <returns>Initialized instance of <seealso cref="FileNotFoundException" />.</returns>
		[DebuggerHidden, NotNull, MethodImpl(AggressiveInlining)]
		[MustUseReturnValue]
		public static FileNotFoundException ArgumentFileNotFound(
			[NotNull, InvokerParameterName] string argumentName,
			[NotNull] string filePath)
		{
			BreakIfAttached();
			return new FileNotFoundException($"Argument {argumentName}. File {filePath} not found.", filePath)
				.LogToCodeTraceSourceBeforeThrow();
		}

		/// <summary>Creates <seealso cref="DirectoryNotFoundException" /> for missing directory.</summary>
		/// <param name="argumentName">Name of the argument.</param>
		/// <param name="directoryPath">The directory being checked.</param>
		/// <returns>Initialized instance of <seealso cref="DirectoryNotFoundException" />.</returns>
		[DebuggerHidden, NotNull, MethodImpl(AggressiveInlining)]
		[MustUseReturnValue]
		public static DirectoryNotFoundException ArgumentDirectoryNotFound(
			[NotNull, InvokerParameterName] string argumentName,
			[NotNull] string directoryPath)
		{
			BreakIfAttached();
			return new DirectoryNotFoundException($"Argument {argumentName}. Directory {directoryPath} not found.")
				.LogToCodeTraceSourceBeforeThrow();
		}

		/// <summary>Creates <seealso cref="IOException"/></summary>
		/// <param name="messageFormat">The message format.</param>
		/// <param name="args">The arguments.</param>
		/// <returns>Initialized instance of <see cref="IOException"/>.</returns>
		[DebuggerHidden, NotNull, MethodImpl(AggressiveInlining)]
		[StringFormatMethod("messageFormat")]
		[MustUseReturnValue]
		// ReSharper disable once InconsistentNaming
		public static IOException IOException(
			[NotNull] string messageFormat,
			[CanBeNull] params object[] args)
		{
			BreakIfAttached();
			return new IOException(
				FormatMessage(messageFormat, args))
				.LogToCodeTraceSourceBeforeThrow();
		}

		/// <summary>Creates <seealso cref="IOException" /> for file that should not exist.</summary>
		/// <param name="filePath">The file being checked.</param>
		/// <returns>Initialized instance of <seealso cref="IOException" />.</returns>
		[DebuggerHidden, NotNull, MethodImpl(AggressiveInlining)]
		[MustUseReturnValue]
		public static IOException FileExists([NotNull] string filePath)
		{
			BreakIfAttached();
			return new IOException($"File {filePath} exists while should not.")
				.LogToCodeTraceSourceBeforeThrow();
		}

		/// <summary>Creates <seealso cref="IOException" /> for directory that should not exist.</summary>
		/// <param name="directoryPath">The directory being checked.</param>
		/// <returns>Initialized instance of <seealso cref="IOException" />.</returns>
		[DebuggerHidden, NotNull, MethodImpl(AggressiveInlining)]
		[MustUseReturnValue]
		public static IOException DirectoryExists([NotNull] string directoryPath)
		{
			BreakIfAttached();
			return new IOException($"Directory {directoryPath} exists while should not.")
				.LogToCodeTraceSourceBeforeThrow();
		}
		#endregion
	}
}