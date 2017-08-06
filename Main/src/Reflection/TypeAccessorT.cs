﻿#if !FW35
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CodeJam.Reflection
{
	/// <summary>
	/// Provides fast access to type and its members.
	/// </summary>
	public class TypeAccessor<T> : TypeAccessor
	{
		static TypeAccessor()
		{
			// Create Instance.
			//
			var type = typeof(T);

			if (type.IsValueType)
			{
				CreateInstanceExpression = () => default(T);
				_createInstance          = () => default(T);
			}
			else
			{
				var ctor = type.IsAbstract ? null : type.GetDefaultConstructor();

				if (ctor == null)
				{
					Expression<Func<T>> mi;

					if (type.IsAbstract) mi = () => ThrowAbstractException();
					else                 mi = () => ThrowException();

					var body = Expression.Call(null, ((MethodCallExpression)mi.Body).Method);

					CreateInstanceExpression = Expression.Lambda<Func<T>>(body);
				}
				else
				{
					CreateInstanceExpression = Expression.Lambda<Func<T>>(Expression.New(ctor));
				}

				_createInstance = CreateInstanceExpression.Compile();
			}

			foreach (var memberInfo in type.GetMembers(BindingFlags.Instance | BindingFlags.Public))
			{
				if (memberInfo.MemberType == MemberTypes.Field ||
					memberInfo.MemberType == MemberTypes.Property && ((PropertyInfo)memberInfo).GetIndexParameters().Length == 0)
				{
					_members.Add(memberInfo);
				}
			}

			// Add explicit interface implementation properties support
			// Or maybe we should support all private fields/properties?
			//
			if (!type.IsInterface && !type.IsArray)
			{
				var interfaceMethods = type.GetInterfaces().SelectMany(ti => type.GetInterfaceMap(ti).TargetMethods).ToList();

				if (interfaceMethods.Count > 0)
				{
					foreach (var pi in type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance))
					{
						if (pi.GetIndexParameters().Length == 0)
						{
							var getMethod = pi.GetGetMethod(true);
							var setMethod = pi.GetSetMethod(true);

							if ((getMethod == null || interfaceMethods.Contains(getMethod)) &&
								(setMethod == null || interfaceMethods.Contains(setMethod)))
							{
								_members.Add(pi);
							}
						}
					}
				}
			}
		}

		private static T ThrowException() =>
			throw new InvalidOperationException($"The '{typeof(T).FullName}' type must have default or init constructor.");

		private static T ThrowAbstractException() =>
			throw new InvalidOperationException($"Cant create an instance of abstract class '{typeof(T).FullName}'.");

		// ReSharper disable once StaticMemberInGenericType
		private static readonly List<MemberInfo> _members = new List<MemberInfo>();

		internal TypeAccessor()
		{
			foreach (var member in _members)
				AddMember(new MemberAccessor(this, member));
		}

		/// <summary>
		/// Returns create instance expression.
		/// </summary>
		public static Expression<Func<T>> CreateInstanceExpression { get; }

		private static readonly Func<T> _createInstance;

		/// <summary>
		/// Creates an instance of <see cref="TypeAccessor"/>.
		/// </summary>
		/// <returns>Instance of <see cref="TypeAccessor"/>.</returns>
		public override object CreateInstance() => _createInstance();

		/// <summary>
		/// Creates an instance of <see cref="TypeAccessor"/>.
		/// </summary>
		/// <returns>Instance of <see cref="TypeAccessor"/>.</returns>
		public T Create() => _createInstance();

		/// <summary>
		/// Type to access.
		/// </summary>
		public override Type Type => typeof(T);

		/// <summary>
		/// Creates an instance of <see cref="TypeAccessor{T}"/>.
		/// </summary>
		/// <returns>Instance of <see cref="TypeAccessor{T}"/>.</returns>
		public static TypeAccessor<T> GetAccessor()
			=> GetAccessor<T>();
	}
}
#endif