using dninosores.UnityAccessors;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Interpolates a Color between two values.
	/// </summary>
	public abstract class AbstractColorInterpolator : AbstractInterpolator<Color>
	{
		protected override Color Interpolate(Color start, Color end, float fraction)
		{
			return InterpolateColor(start, end, fraction);
		}

		public static Color InterpolateColor(Color start, Color end, float fraction)
		{
			return (end - start) * (fraction) + start;
		}

		protected override Color Multiply(Color a, Color b)
		{
			return new Color(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);
		}
	}
}