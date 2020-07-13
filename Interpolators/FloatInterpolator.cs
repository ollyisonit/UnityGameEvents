using dninosores.UnityAccessors;
using dninosores.UnityEditorAttributes;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// GameEvent that interpolates a float between two values using a FloatAccessor.
	/// </summary>
	public class FloatInterpolator : AbstractFloatInterpolator
	{
		[ConditionalHide("overrideStart", true)]
		public FloatOrConstantAccessor StartVal;
		public FloatOrConstantAccessor End;
		public AnyFloatAccessor value;
		protected override Accessor<float> interpolatedValue => value;

		protected override float start => StartVal.Value;

		protected override float end => End.Value;

		protected override void Reset()
		{
			value = new AnyFloatAccessor();
			base.Reset();
		}
	}
}