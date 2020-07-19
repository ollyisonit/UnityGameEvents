#pragma warning disable 0649
using dninosores.UnityAccessors;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Sets a float value using an Accessor.
	/// </summary>
	public class SetInt : AddMultiplySetValueEvent<int>
	{
		public AnyIntAccessor accessor;
		public IntOrConstantAccessor Value;
		protected override Accessor<int> valueAccessor => accessor;

		protected override int value => Value.Value;
	}
}
