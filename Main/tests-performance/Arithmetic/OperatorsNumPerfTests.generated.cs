﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;

using CodeJam.PerfTests;

using NUnit.Framework;

namespace CodeJam.Arithmetic
{
	[TestFixture(Category = CompetitionHelpers.PerfTestCategory + ": Operators<T>.Numeric")]
	[CompetitionXmlAnnotation("CodeJam.Arithmetic.OperatorsNumPerfTests.generated.xml")]
	[Explicit(CompetitionHelpers.TemporarilyExcludedReason)]
	public class OperatorsNumPerfTests
	{
		#region Unary
		#region UnaryMinus
		[Test]
		public void RunIntUnaryMinusCase() =>
			Competition.Run<IntUnaryMinusCase>();

		[CompetitionBurstMode]		
		public class IntUnaryMinusCase : IntOperatorsBaseCase
		{
			private static readonly Func<int, int> _opUnaryMinus = Operators<int>.UnaryMinus;
			private static readonly Func<int, int> _emittedUnaryMinus = b => -b;

			[CompetitionBaseline]
			public int Test00UnaryMinusBaseline()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
				{
					var b = ValuesB[i];
					result = -b;
				}
				return result;
			}

			[CompetitionBenchmark]
			public int Test01UnaryMinusOperator()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _opUnaryMinus(ValuesB[i]);
				return result;
			}

			[CompetitionBenchmark]
			public int Test02UnaryMinusEmitted()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _emittedUnaryMinus(ValuesB[i]);
				return result;
			}
		}
		#endregion

		#region OnesComplement
		[Test]
		public void RunIntOnesComplementCase() =>
			Competition.Run<IntOnesComplementCase>();

		[CompetitionBurstMode]		
		public class IntOnesComplementCase : IntOperatorsBaseCase
		{
			private static readonly Func<int, int> _opOnesComplement = Operators<int>.OnesComplement;
			private static readonly Func<int, int> _emittedOnesComplement = b => ~b;

			[CompetitionBaseline]
			public int Test00OnesComplementBaseline()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
				{
					var b = ValuesB[i];
					result = ~b;
				}
				return result;
			}

			[CompetitionBenchmark]
			public int Test01OnesComplementOperator()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _opOnesComplement(ValuesB[i]);
				return result;
			}

			[CompetitionBenchmark]
			public int Test02OnesComplementEmitted()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _emittedOnesComplement(ValuesB[i]);
				return result;
			}
		}
		#endregion
		#endregion

		#region Binary
		#region Plus
		[Test]
		public void RunIntPlusCase() =>
			Competition.Run<IntPlusCase>();

		[CompetitionBurstMode]
		public class IntPlusCase : IntOperatorsBaseCase
		{
			private static readonly Func<int, int, int> _opPlus = Operators<int>.Plus;
			private static readonly Func<int, int, int> _emittedPlus = (a, b) => a + b;

			[CompetitionBaseline]
			public int Test00PlusBaseline()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
				{
					var a = ValuesA[i];
					var b = ValuesB[i];
					result = a + b;
				}
				return result;
			}

			[CompetitionBenchmark]
			public int Test01PlusOperator()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _opPlus(ValuesA[i], ValuesB[i]);
				return result;
			}

			[CompetitionBenchmark]
			public int Test02PlusEmitted()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _emittedPlus(ValuesA[i], ValuesB[i]);
				return result;
			}
		}

		[Test]
		public void RunNullableDoublePlusCase() =>
			Competition.Run<NullableDoublePlusCase>();

		[CompetitionBurstMode]
		public class NullableDoublePlusCase : NullableDoubleOperatorsBaseCase
		{
			private static readonly Func<double?, double?, double?> _opPlus = Operators<double?>.Plus;
			private static readonly Func<double?, double?, double?> _emittedPlus = (a, b) => a + b;

			[CompetitionBaseline]
			public double? Test00PlusBaseline()
			{
				var result = default(double?);
				for (var i = 0; i < ValuesB.Length; i++)
				{
					var a = ValuesA[i];
					var b = ValuesB[i];
					result = a + b;
				}
				return result;
			}

			[CompetitionBenchmark]
			public double? Test01PlusOperator()
			{
				var result = default(double?);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _opPlus(ValuesA[i], ValuesB[i]);
				return result;
			}

			[CompetitionBenchmark]
			public double? Test02PlusEmitted()
			{
				var result = default(double?);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _emittedPlus(ValuesA[i], ValuesB[i]);
				return result;
			}
		}
		#endregion

		#region Minus
		[Test]
		public void RunIntMinusCase() =>
			Competition.Run<IntMinusCase>();

		[CompetitionBurstMode]
		public class IntMinusCase : IntOperatorsBaseCase
		{
			private static readonly Func<int, int, int> _opMinus = Operators<int>.Minus;
			private static readonly Func<int, int, int> _emittedMinus = (a, b) => a - b;

			[CompetitionBaseline]
			public int Test00MinusBaseline()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
				{
					var a = ValuesA[i];
					var b = ValuesB[i];
					result = a - b;
				}
				return result;
			}

			[CompetitionBenchmark]
			public int Test01MinusOperator()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _opMinus(ValuesA[i], ValuesB[i]);
				return result;
			}

			[CompetitionBenchmark]
			public int Test02MinusEmitted()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _emittedMinus(ValuesA[i], ValuesB[i]);
				return result;
			}
		}

		[Test]
		public void RunNullableDoubleMinusCase() =>
			Competition.Run<NullableDoubleMinusCase>();

		[CompetitionBurstMode]
		public class NullableDoubleMinusCase : NullableDoubleOperatorsBaseCase
		{
			private static readonly Func<double?, double?, double?> _opMinus = Operators<double?>.Minus;
			private static readonly Func<double?, double?, double?> _emittedMinus = (a, b) => a - b;

			[CompetitionBaseline]
			public double? Test00MinusBaseline()
			{
				var result = default(double?);
				for (var i = 0; i < ValuesB.Length; i++)
				{
					var a = ValuesA[i];
					var b = ValuesB[i];
					result = a - b;
				}
				return result;
			}

			[CompetitionBenchmark]
			public double? Test01MinusOperator()
			{
				var result = default(double?);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _opMinus(ValuesA[i], ValuesB[i]);
				return result;
			}

			[CompetitionBenchmark]
			public double? Test02MinusEmitted()
			{
				var result = default(double?);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _emittedMinus(ValuesA[i], ValuesB[i]);
				return result;
			}
		}
		#endregion

		#region Mul
		[Test]
		public void RunIntMulCase() =>
			Competition.Run<IntMulCase>();

		[CompetitionBurstMode]
		public class IntMulCase : IntOperatorsBaseCase
		{
			private static readonly Func<int, int, int> _opMul = Operators<int>.Mul;
			private static readonly Func<int, int, int> _emittedMul = (a, b) => a * b;

			[CompetitionBaseline]
			public int Test00MulBaseline()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
				{
					var a = ValuesA[i];
					var b = ValuesB[i];
					result = a * b;
				}
				return result;
			}

			[CompetitionBenchmark]
			public int Test01MulOperator()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _opMul(ValuesA[i], ValuesB[i]);
				return result;
			}

			[CompetitionBenchmark]
			public int Test02MulEmitted()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _emittedMul(ValuesA[i], ValuesB[i]);
				return result;
			}
		}

		[Test]
		public void RunNullableDoubleMulCase() =>
			Competition.Run<NullableDoubleMulCase>();

		[CompetitionBurstMode]
		public class NullableDoubleMulCase : NullableDoubleOperatorsBaseCase
		{
			private static readonly Func<double?, double?, double?> _opMul = Operators<double?>.Mul;
			private static readonly Func<double?, double?, double?> _emittedMul = (a, b) => a * b;

			[CompetitionBaseline]
			public double? Test00MulBaseline()
			{
				var result = default(double?);
				for (var i = 0; i < ValuesB.Length; i++)
				{
					var a = ValuesA[i];
					var b = ValuesB[i];
					result = a * b;
				}
				return result;
			}

			[CompetitionBenchmark]
			public double? Test01MulOperator()
			{
				var result = default(double?);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _opMul(ValuesA[i], ValuesB[i]);
				return result;
			}

			[CompetitionBenchmark]
			public double? Test02MulEmitted()
			{
				var result = default(double?);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _emittedMul(ValuesA[i], ValuesB[i]);
				return result;
			}
		}
		#endregion

		#region Div
		[Test]
		public void RunIntDivCase() =>
			Competition.Run<IntDivCase>();

		[CompetitionBurstMode]
		public class IntDivCase : IntOperatorsBaseCase
		{
			private static readonly Func<int, int, int> _opDiv = Operators<int>.Div;
			private static readonly Func<int, int, int> _emittedDiv = (a, b) => a / b;

			[CompetitionBaseline]
			public int Test00DivBaseline()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
				{
					var a = ValuesA[i];
					var b = ValuesB[i];
					result = a / b;
				}
				return result;
			}

			[CompetitionBenchmark]
			public int Test01DivOperator()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _opDiv(ValuesA[i], ValuesB[i]);
				return result;
			}

			[CompetitionBenchmark]
			public int Test02DivEmitted()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _emittedDiv(ValuesA[i], ValuesB[i]);
				return result;
			}
		}

		[Test]
		public void RunNullableDoubleDivCase() =>
			Competition.Run<NullableDoubleDivCase>();

		[CompetitionBurstMode]
		public class NullableDoubleDivCase : NullableDoubleOperatorsBaseCase
		{
			private static readonly Func<double?, double?, double?> _opDiv = Operators<double?>.Div;
			private static readonly Func<double?, double?, double?> _emittedDiv = (a, b) => a / b;

			[CompetitionBaseline]
			public double? Test00DivBaseline()
			{
				var result = default(double?);
				for (var i = 0; i < ValuesB.Length; i++)
				{
					var a = ValuesA[i];
					var b = ValuesB[i];
					result = a / b;
				}
				return result;
			}

			[CompetitionBenchmark]
			public double? Test01DivOperator()
			{
				var result = default(double?);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _opDiv(ValuesA[i], ValuesB[i]);
				return result;
			}

			[CompetitionBenchmark]
			public double? Test02DivEmitted()
			{
				var result = default(double?);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _emittedDiv(ValuesA[i], ValuesB[i]);
				return result;
			}
		}
		#endregion

		#region Modulo
		[Test]
		public void RunIntModuloCase() =>
			Competition.Run<IntModuloCase>();

		[CompetitionBurstMode]
		public class IntModuloCase : IntOperatorsBaseCase
		{
			private static readonly Func<int, int, int> _opModulo = Operators<int>.Modulo;
			private static readonly Func<int, int, int> _emittedModulo = (a, b) => a % b;

			[CompetitionBaseline]
			public int Test00ModuloBaseline()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
				{
					var a = ValuesA[i];
					var b = ValuesB[i];
					result = a % b;
				}
				return result;
			}

			[CompetitionBenchmark]
			public int Test01ModuloOperator()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _opModulo(ValuesA[i], ValuesB[i]);
				return result;
			}

			[CompetitionBenchmark]
			public int Test02ModuloEmitted()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _emittedModulo(ValuesA[i], ValuesB[i]);
				return result;
			}
		}
		#endregion

		#region Xor
		[Test]
		public void RunIntXorCase() =>
			Competition.Run<IntXorCase>();

		[CompetitionBurstMode]
		public class IntXorCase : IntOperatorsBaseCase
		{
			private static readonly Func<int, int, int> _opXor = Operators<int>.Xor;
			private static readonly Func<int, int, int> _emittedXor = (a, b) => a ^ b;

			[CompetitionBaseline]
			public int Test00XorBaseline()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
				{
					var a = ValuesA[i];
					var b = ValuesB[i];
					result = a ^ b;
				}
				return result;
			}

			[CompetitionBenchmark]
			public int Test01XorOperator()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _opXor(ValuesA[i], ValuesB[i]);
				return result;
			}

			[CompetitionBenchmark]
			public int Test02XorEmitted()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _emittedXor(ValuesA[i], ValuesB[i]);
				return result;
			}
		}
		#endregion

		#region BitwiseAnd
		[Test]
		public void RunIntBitwiseAndCase() =>
			Competition.Run<IntBitwiseAndCase>();

		[CompetitionBurstMode]
		public class IntBitwiseAndCase : IntOperatorsBaseCase
		{
			private static readonly Func<int, int, int> _opBitwiseAnd = Operators<int>.BitwiseAnd;
			private static readonly Func<int, int, int> _emittedBitwiseAnd = (a, b) => a & b;

			[CompetitionBaseline]
			public int Test00BitwiseAndBaseline()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
				{
					var a = ValuesA[i];
					var b = ValuesB[i];
					result = a & b;
				}
				return result;
			}

			[CompetitionBenchmark]
			public int Test01BitwiseAndOperator()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _opBitwiseAnd(ValuesA[i], ValuesB[i]);
				return result;
			}

			[CompetitionBenchmark]
			public int Test02BitwiseAndEmitted()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _emittedBitwiseAnd(ValuesA[i], ValuesB[i]);
				return result;
			}
		}
		#endregion

		#region BitwiseOr
		[Test]
		public void RunIntBitwiseOrCase() =>
			Competition.Run<IntBitwiseOrCase>();

		[CompetitionBurstMode]
		public class IntBitwiseOrCase : IntOperatorsBaseCase
		{
			private static readonly Func<int, int, int> _opBitwiseOr = Operators<int>.BitwiseOr;
			private static readonly Func<int, int, int> _emittedBitwiseOr = (a, b) => a | b;

			[CompetitionBaseline]
			public int Test00BitwiseOrBaseline()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
				{
					var a = ValuesA[i];
					var b = ValuesB[i];
					result = a | b;
				}
				return result;
			}

			[CompetitionBenchmark]
			public int Test01BitwiseOrOperator()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _opBitwiseOr(ValuesA[i], ValuesB[i]);
				return result;
			}

			[CompetitionBenchmark]
			public int Test02BitwiseOrEmitted()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _emittedBitwiseOr(ValuesA[i], ValuesB[i]);
				return result;
			}
		}
		#endregion

		#region LeftShift
		[Test]
		public void RunIntLeftShiftCase() =>
			Competition.Run<IntLeftShiftCase>();

		[CompetitionBurstMode]
		public class IntLeftShiftCase : IntOperatorsBaseCase
		{
			private static readonly Func<int, int, int> _opLeftShift = Operators<int>.LeftShift;
			private static readonly Func<int, int, int> _emittedLeftShift = (a, b) => a << b;

			[CompetitionBaseline]
			public int Test00LeftShiftBaseline()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
				{
					var a = ValuesA[i];
					var b = ValuesB[i];
					result = a << b;
				}
				return result;
			}

			[CompetitionBenchmark]
			public int Test01LeftShiftOperator()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _opLeftShift(ValuesA[i], ValuesB[i]);
				return result;
			}

			[CompetitionBenchmark]
			public int Test02LeftShiftEmitted()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _emittedLeftShift(ValuesA[i], ValuesB[i]);
				return result;
			}
		}
		#endregion

		#region RightShift
		[Test]
		public void RunIntRightShiftCase() =>
			Competition.Run<IntRightShiftCase>();

		[CompetitionBurstMode]
		public class IntRightShiftCase : IntOperatorsBaseCase
		{
			private static readonly Func<int, int, int> _opRightShift = Operators<int>.RightShift;
			private static readonly Func<int, int, int> _emittedRightShift = (a, b) => a >> b;

			[CompetitionBaseline]
			public int Test00RightShiftBaseline()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
				{
					var a = ValuesA[i];
					var b = ValuesB[i];
					result = a >> b;
				}
				return result;
			}

			[CompetitionBenchmark]
			public int Test01RightShiftOperator()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _opRightShift(ValuesA[i], ValuesB[i]);
				return result;
			}

			[CompetitionBenchmark]
			public int Test02RightShiftEmitted()
			{
				var result = default(int);
				for (var i = 0; i < ValuesB.Length; i++)
					result = _emittedRightShift(ValuesA[i], ValuesB[i]);
				return result;
			}
		}
		#endregion
		#endregion
	}
}