using dninosores.UnityAccessors;
using dninosores.UnityEditorAttributes;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// GameEvent that interpolates a float between two values using a FloatAccessor.
	/// </summary>
	public class IntInterpolator : AbstractIntInterpolator
	{
		[ConditionalHide("overrideStart", true)]
		public IntOrConstantAccessor StartVal;
		public IntOrConstantAccessor End;
		public AnyIntAccessor value;
		protected override Accessor<int> interpolatedValue => value;

		protected override int start => StartVal.Value;

		protected override int end => End.Value;
	}
}