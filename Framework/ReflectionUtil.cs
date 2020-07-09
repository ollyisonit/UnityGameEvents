using System;
using System.Reflection;

namespace dninosores.UnityGameEvents
{
	public static class ReflectionUtil
	{
		public static MethodInfo GetMethod(Type type, string name, params Type[] inputs)
		{
			MethodInfo method = type.GetMethod(name, inputs);
			if (method == null)
			{
				throw new NotImplementedException("No suitible " + name + " method found!");
			}
			return method;
			
		}

		public static T EvaluateBinaryOperator<T>(string name, T left, T right)
		{
			return (T)GetMethod(typeof(T), name, typeof(T), typeof(T)).Invoke(left, new object[] { left, right });
		}
	}
}
