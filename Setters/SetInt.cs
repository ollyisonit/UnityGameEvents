#pragma warning disable 0649
using dninosores.UnityAccessors;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Sets a float value using an Accessor.
	/// </summary>
	class SetInt : AddMultiplySetValueEvent<int>
	{
		public AnyIntAccessor accessor;
		public IntOrConstantAccessor Value;
		protected override Accessor<int> valueAccessor => accessor;

		protected override int value => Value.Value;

		protected override void Reset()
		{
			base.Reset();
			accessor = new AnyIntAccessor();
			accessor.Reset(gameObject);
		}
	}
}
