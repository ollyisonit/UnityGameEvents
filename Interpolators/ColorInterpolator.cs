using dninosores.UnityAccessors;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// GameEvent that interpolates a float between two values using a ColorAccessor.
	/// </summary>
	public class ColorInterpolator : AbstractColorInterpolator
	{
		public AnyColorAccessor value;
		protected override Accessor<Color> interpolatedValue => value;

		protected override void Reset()
		{
			value = new AnyColorAccessor();
			base.Reset();
		}
	}
}