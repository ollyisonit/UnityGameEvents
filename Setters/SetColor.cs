using dninosores.UnityAccessors;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Sets a Color value using an Accessor.
	/// </summary>
	class SetColor : SetValueEvent<Color>
	{
		public AnyColorAccessor accessor;
		protected override Accessor<Color> valueAccessor => accessor;

		protected override void Reset()
		{
			accessor = new AnyColorAccessor();
			accessor.Reset(gameObject);
		}
	}
}
