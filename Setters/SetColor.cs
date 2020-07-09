using dninosores.UnityAccessors;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Sets a Color value using an Accessor.
	/// </summary>
	class SetColor : AddMultiplySetValueEvent<Color>
	{
		public AnyColorAccessor accessor;
		protected override Accessor<Color> valueAccessor => accessor;

		protected override void Reset()
		{
			accessor = new AnyColorAccessor();
			accessor.Reset(gameObject);
		}


		protected override Color Multiply(Color a, Color b)
		{
			return new Color(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);
		}
	}
}
