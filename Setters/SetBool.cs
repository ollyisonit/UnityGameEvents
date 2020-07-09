using dninosores.UnityAccessors;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Sets a bool value using an Accessor.
	/// </summary>
	class SetBool : SetValueEvent<bool>
	{
		public AnyBoolAccessor accessor;
		protected override Accessor<bool> valueAccessor => accessor;

		protected override void Reset()
		{
			accessor = new AnyBoolAccessor();
			accessor.Reset(gameObject);
		}
	}
}
