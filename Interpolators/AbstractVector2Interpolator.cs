using dninosores.UnityAccessors;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Interpolates a Vector2 between two values.
	/// </summary>
	public abstract class AbstractVector2Interpolator : AbstractInterpolator<Vector2>
	{
		protected override Vector2 Interpolate(Vector2 start, Vector2 end, float fraction)
		{
			return InterpolateVector2(start, end, fraction);
		}

		public static Vector2 InterpolateVector2(Vector2 start, Vector2 end, float fraction)
		{
			return (end - start) * (fraction) + start;
		}
	}
}