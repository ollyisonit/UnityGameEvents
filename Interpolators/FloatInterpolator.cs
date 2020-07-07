using dninosores.UnityValueAccessors;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// GameEvent that interpolates a float between two values.
	/// </summary>
	public class FloatInterpolator : AbstractFloatInterpolator
	{
		public AnyFloatValueAccessor value;
		protected override ValueAccessor<float> interpolatedValue => value;
	}
}