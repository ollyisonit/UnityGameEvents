using dninosores.UnityAccessors;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// GameEvent that interpolates a float between two values.
	/// </summary>
	public class FloatInterpolator : AbstractFloatInterpolator
	{
		public AnyFloatAccessor value;
		protected override Accessor<float> interpolatedValue => value;
	}
}