using dninosores.UnityAccessors;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// GameEvent that interpolates a float between two values using a Vector3Accessor.
	/// </summary>
	public class Vector3Interpolator : AbstractVector3Interpolator
	{
		public AnyVector3Accessor value;
		protected override Accessor<Vector3> interpolatedValue => value;


		protected override void Reset()
		{
			value = new AnyVector3Accessor();
			base.Reset();
		}
	}
}