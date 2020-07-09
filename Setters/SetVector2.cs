using dninosores.UnityAccessors;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Sets a Vector2 value using an Accessor.
	/// </summary>
	class SetVector2 : AddMultiplySetValueEvent<Vector2>
	{
		public AnyVector2Accessor accessor;
		protected override Accessor<Vector2> valueAccessor => accessor;

		protected override void Reset()
		{
			accessor = new AnyVector2Accessor();
			accessor.Reset(gameObject);
		}

		protected override Vector2 Multiply(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x * b.x, a.y * b.y);
		}
	}
}
