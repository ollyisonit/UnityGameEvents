using dninosores.UnityAccessors;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Sets a Vector3 value using an Accessor.
	/// </summary>
	class SetVector3 : AddMultiplySetValueEvent<Vector3>
	{
		public AnyVector3Accessor accessor;
		protected override Accessor<Vector3> valueAccessor => accessor;

		protected override void Reset()
		{
			accessor = new AnyVector3Accessor();
			accessor.Reset(gameObject);
		}


		protected override Vector3 Multiply(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
		}
	}
}
