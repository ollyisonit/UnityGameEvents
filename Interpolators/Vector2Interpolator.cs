using dninosores.UnityAccessors;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// GameEvent that interpolates a float between two values using a Vector2Accessor.
	/// </summary>
	public class Vector2Interpolator : AbstractVector2Interpolator
	{
		public AnyVector2Accessor Value;
		protected override Accessor<Vector2> interpolatedValue => Value;

		protected override void Reset()
		{
			Value = new AnyVector2Accessor();
			base.Reset();
		}
	}
}