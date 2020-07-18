using dninosores.UnityAccessors;
using dninosores.UnityEditorAttributes;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// GameEvent that interpolates a float between two values using a ColorAccessor.
	/// </summary>
	public class ColorInterpolator : AbstractColorInterpolator
	{
		[ConditionalHide("overrideStart", true)]
		public ColorOrConstantAccessor StartVal;
		public ColorOrConstantAccessor End;
		public AnyColorAccessor value;
		protected override Color start => StartVal.Value;

		protected override Color end => End.Value;

		protected override Accessor<Color> interpolatedValue => value;
	}
}