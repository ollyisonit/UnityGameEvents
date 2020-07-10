using dninosores.UnityEditorAttributes;
using System;
using System.Collections;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Sets int, bool, float, or trigger parameter in an animator.
	/// </summary>
	public class SetAnimationParameter : InstantGameEvent
	{
		public enum ParameterType
		{
			Trigger = 0,
			Bool = 1,
			Float = 2,
			Integer = 3
		}
		public Animator animator;
		public string parameterName;
		public ParameterType parameterType;

		[ConditionalHide("parameterType", ParameterType.Bool)]
		public bool boolValue;

		[ConditionalHide("parameterType", ParameterType.Float)]
		public float floatValue;

		[ConditionalHide("parameterType", ParameterType.Integer)]
		public int integerValue;


		protected override void InstantEvent()
		{
			switch (parameterType)
			{
				case ParameterType.Trigger:
					animator.SetTrigger(parameterName);
					break;
				case ParameterType.Bool:
					animator.SetBool(parameterName, boolValue);
					break;
				case ParameterType.Float:
					animator.SetFloat(parameterName, floatValue);
					break;
				case ParameterType.Integer:
					animator.SetInteger(parameterName, integerValue);
					break;
				default:
					throw new NotImplementedException("Case not found for " + parameterType);
			}
		}
	}
}