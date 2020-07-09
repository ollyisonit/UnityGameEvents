using dninosores.UnityAccessors;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Sets a Vector3 value using an Accessor.
	/// </summary>
	class SetVector3 : SetValueEvent<Vector3>
	{
		public AnyVector3Accessor accessor;
		protected override Accessor<Vector3> valueAccessor => accessor;

		protected override void Reset()
		{
			accessor = new AnyVector3Accessor();
			accessor.Reset(gameObject);
		}
	}
}
