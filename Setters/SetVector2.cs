#pragma warning disable 0649

using dninosores.UnityAccessors;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Sets a Vector2 value using an Accessor.
	/// </summary>
	public class SetVector2 : AddMultiplySetValueEvent<Vector2>
	{
		public AnyVector2Accessor accessor;
		public Vector2OrConstantAccessor Value;
		protected override Accessor<Vector2> valueAccessor => accessor;

		protected override Vector2 value => Value.Value;


		protected override Vector2 Multiply(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x * b.x, a.y * b.y);
		}
	}
}
