using dninosores.UnityAccessors;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// GameEvent that interpolates a float between two values using a Vector2Accessor.
	/// </summary>
	public class Vector2Interpolator : AbstractVector2Interpolator
	{
		public AnyVector2Accessor value;
		protected override Accessor<Vector2> interpolatedValue => value;

		protected override void Reset()
		{
			value = new AnyVector2Accessor();
			base.Reset();
		}
	}
}