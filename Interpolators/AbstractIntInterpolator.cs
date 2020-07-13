using dninosores.UnityAccessors;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// GameEvent that interpolates a float between two values.
	/// </summary>
	public abstract class AbstractIntInterpolator : AbstractInterpolator<int>
	{
		protected override int Interpolate(int start, int end, float fraction)
		{
			return Mathf.RoundToInt(AbstractFloatInterpolator.InterpolateFloat(start, end, fraction));
		}
	}
}