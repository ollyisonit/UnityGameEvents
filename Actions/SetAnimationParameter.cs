using dninosores.UnityAccessors;
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
		public StringOrConstantAccessor parameterName;
		public ParameterType parameterType;

		[ConditionalHide("parameterType", ParameterType.Bool)]
		public BoolOrConstantAccessor boolValue;

		[ConditionalHide("parameterType", ParameterType.Float)]
		public FloatOrConstantAccessor floatValue;

		[ConditionalHide("parameterType", ParameterType.Integer)]
		public IntOrConstantAccessor integerValue;


		protected override void InstantEvent()
		{
			switch (parameterType)
			{
				case ParameterType.Trigger:
					animator.SetTrigger(parameterName.Value);
					break;
				case ParameterType.Bool:
					animator.SetBool(parameterName.Value, boolValue.Value);
					break;
				case ParameterType.Float:
					animator.SetFloat(parameterName.Value, floatValue.Value);
					break;
				case ParameterType.Integer:
					animator.SetInteger(parameterName.Value, integerValue.Value);
					break;
				default:
					throw new NotImplementedException("Case not found for " + parameterType);
			}
		}
	}
}