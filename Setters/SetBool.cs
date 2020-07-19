#pragma warning disable 0649
using dninosores.UnityAccessors;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Sets a bool value using an Accessor.
	/// </summary>
	public class SetBool : NegatableSetValueEvent<bool>
	{
		public AnyBoolAccessor accessor;
		public BoolOrConstantAccessor Value;
		protected override Accessor<bool> valueAccessor => accessor;

		protected override bool value => Value.Value;

		protected override bool Negate(bool obj)
		{
			return !obj;
		}
	}
}
