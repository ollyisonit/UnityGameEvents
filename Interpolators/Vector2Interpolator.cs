using dninosores.UnityAccessors;
using dninosores.UnityEditorAttributes;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// GameEvent that interpolates a float between two values using a Vector2Accessor.
	/// </summary>
	public class Vector2Interpolator : AbstractVector2Interpolator
	{
		[ConditionalHide("overrideStart", true)]
		public Vector2OrConstantAccessor StartVal;
		public Vector2OrConstantAccessor End;
		public AnyVector2Accessor Value;
		protected override Accessor<Vector2> interpolatedValue => Value;

		protected override Vector2 start => StartVal.Value;

		protected override Vector2 end => End.Value;

		protected override void Reset()
		{
			Value = new AnyVector2Accessor();
			base.Reset();
		}
	}
}