using dninosores.UnityAccessors;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Sets a Vector2 value using an Accessor.
	/// </summary>
	class SetVector2 : SetValueEvent<Vector2>
	{
		public AnyVector2Accessor accessor;
		protected override Accessor<Vector2> valueAccessor => accessor;

		protected override void Reset()
		{
			accessor = new AnyVector2Accessor();
			accessor.Reset(gameObject);
		}
	}
}
