#pragma warning disable 0649
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
		public Vector3OrConstantAccessor Value;
		protected override Accessor<Vector3> valueAccessor => accessor;

		protected override Vector3 value => Value.Value;


		protected override Vector3 Multiply(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
		}
	}
}
