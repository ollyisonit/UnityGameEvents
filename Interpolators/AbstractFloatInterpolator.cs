using dninosores.UnityValueAccessors;

namespace dninosores.UnityGameEvents
{
	public abstract class AbstractFloatInterpolator : AbstractInterpolator<float>
	{
		protected override float Interpolate(float start, float end, float fraction)
		{
			return InterpolateFloat(start, end, fraction);
		}

		public static float InterpolateFloat(float start, float end, float fraction)
		{
			return (end - start) * (fraction) + start;
		}
	}
}