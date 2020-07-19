using dninosores.UnityEditorAttributes;
using dninosores.UnityGameEvents;
using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Uses reflections to call a method or run a coroutine by name.
	/// </summary>
	public class CallReflected : GameEvent
	{
		public UnityEngine.Object Object;
		public FastForwardBehavior fastForwardBehavior;
		public string methodName;
		public Argument[] arguments;


		/// <summary>
		/// Represents an argument that can be passed to a method.
		/// </summary>
		[Serializable]
		public class Argument
		{
			public enum ArgType
			{
				Bool = 0,
				Int = 1,
				Float = 2,
				String = 3,
				Vector2 = 4,
				Vector3 = 5,
				Color = 6,
				UnityObject = 7
			}

			public ArgType type;

			[ConditionalHideAttribute("type", ArgType.Bool, "Value")]
			public bool boolValue;

			[ConditionalHideAttribute("type", ArgType.Int, "Value")]
			public int intValue;

			[ConditionalHideAttribute("type", ArgType.Float, "Value")]
			public float floatValue;

			[ConditionalHideAttribute("type", ArgType.String, "Value")]
			public string stringValue;

			[ConditionalHideAttribute("type", ArgType.Vector2, "Value")]
			public Vector2 vector2Value;

			[ConditionalHideAttribute("type", ArgType.Vector3, "Value")]
			public Vector3 vector3Value;

			[ConditionalHideAttribute("type", ArgType.Color, "Value")]
			public Color colorValue;

			[ConditionalHideAttribute("type", ArgType.UnityObject, "Value")]
			public UnityEngine.Object objValue;

			public Type GetArgType()
			{
				switch (type)
				{
					case ArgType.Bool:
						return typeof(bool);
					case ArgType.Int:
						return typeof(int);
					case ArgType.String:
						return typeof(string);
					case ArgType.Float:
						return typeof(float);
					case ArgType.Vector2:
						return typeof(Vector2);
					case ArgType.Vector3:
						return typeof(Vector3);
					case ArgType.Color:
						return typeof(Color);
					case ArgType.UnityObject:
						return objValue.GetType();
					default:
						throw new NotImplementedException("Case not found for " + type);
				}
			}


			public object GetArgValue()
			{
				switch (type)
				{
					case ArgType.Bool:
						return boolValue;
					case ArgType.Int:
						return intValue;
					case ArgType.String:
						return stringValue;
					case ArgType.Float:
						return floatValue;
					case ArgType.Vector2:
						return vector2Value;
					case ArgType.Vector3:
						return vector3Value;
					case ArgType.Color:
						return colorValue;
					case ArgType.UnityObject:
						return objValue;
					default:
						throw new NotImplementedException("Case not found for " + type);
				}
			}
		}
		protected override bool InstantInternal => GetMethod().ReturnType != typeof(IEnumerator);

		private bool cancelled;

		private MethodInfo GetMethod()
		{
			string[] fieldsToMethod = methodName.Split('.');
			object target = GetTargetObject();
			string formatName = fieldsToMethod[fieldsToMethod.Length - 1].Replace("()", "");
			Type[] types = new Type[arguments.Length];
			for (int i = 0; i < arguments.Length; i++)
			{
				types[i] = arguments[i].GetArgType();
			}
			MethodInfo method = target.GetType().GetMethod(formatName, types);
			if (method == null)
			{
				string typestring = "";
				foreach (Type t in types)
				{
					typestring += " " + t;
				}
				throw new MissingMethodException("Cannot find method of name '" + formatName + "' taking types" + typestring +
					" on " + target.GetType());
			}
			return method;
		}


		private object GetTargetObject()
		{
			string[] fields = methodName.Split('.');
			object finalObject = Object;
			for (int i = 0; i < fields.Length - 1; i++)
			{
				finalObject = GetFieldOrProperty(finalObject, fields[i]);
			}
			return finalObject;
		}


		public override void ForceRunInstant()
		{
			switch (fastForwardBehavior)
			{
				case FastForwardBehavior.RunInstant:
					base.ForceRunInstant();
					break;
				case FastForwardBehavior.Skip:
					break;
				case FastForwardBehavior.StartParallel:
					StartCoroutine(Run());
					break;
			}
		}

		object GetFieldOrProperty(object declaring, string name)
		{
			FieldInfo field = declaring.GetType().GetField(name);
			if (field != null)
			{
				return field.GetValue(declaring);
			}

			PropertyInfo property = declaring.GetType().GetProperty(name);
			if (property != null)
			{
				return property.GetValue(declaring);
			}

			MethodInfo method = declaring.GetType().GetMethod(name, new Type[] { });
			if (method != null)
			{
				return method.Invoke(declaring, new object[] { });
			}

			throw new MissingFieldException("Cannot find field, property, or no-argument method with name '" + name + "' on " + declaring.GetType());
		}

		public override void Stop()
		{
			cancelled = true;
		}

		protected override IEnumerator RunInternal()
		{
			cancelled = false;
			MethodInfo method = GetMethod();
			object[] args = new object[arguments.Length];
			for (int i = 0; i < args.Length; i++)
			{
				args[i] = arguments[i].GetArgValue();
			}

			if (method.ReturnType != typeof(IEnumerator))
			{
				method.Invoke(GetTargetObject(), args);
				yield break;
			}
			else
			{
				yield return RunWithCancel((IEnumerator)method.Invoke(GetTargetObject(), args));	
			}
		}


		private IEnumerator RunWithCancel(IEnumerator routine)
		{
			while (routine.MoveNext() && !cancelled)
			{
				if (routine.Current is IEnumerator nested)
				{
					yield return RunWithCancel(nested);
				}
				else
				{
					yield return routine.Current;
				}
			}
		}
	}

}