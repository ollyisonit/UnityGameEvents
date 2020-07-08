using dninosores.UnityAccessors;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// GameEvent that interpolates a float between two values using a FloatAccessor.
	/// </summary>
	public class FloatInterpolator : AbstractFloatInterpolator
	{
		public AnyFloatAccessor value;
		protected override Accessor<float> interpolatedValue => value;

		protected override void Reset()
		{
			value = new AnyFloatAccessor();
			base.Reset();
		}
	}
}