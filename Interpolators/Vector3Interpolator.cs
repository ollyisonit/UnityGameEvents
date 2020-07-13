using dninosores.UnityAccessors;
using dninosores.UnityEditorAttributes;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// GameEvent that interpolates a float between two values using a Vector3Accessor.
	/// </summary>
	public class Vector3Interpolator : AbstractVector3Interpolator
	{
		[ConditionalHide("overrideStart", true)]
		public Vector3OrConstantAccessor StartVal;
		public Vector3OrConstantAccessor End;
		public AnyVector3Accessor value;
		protected override Accessor<Vector3> interpolatedValue => value;

		protected override Vector3 start => StartVal.Value;

		protected override Vector3 end => End.Value;

		protected override void Reset()
		{
			value = new AnyVector3Accessor();
			base.Reset();
		}
	}
}