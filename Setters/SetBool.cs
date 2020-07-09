using dninosores.UnityAccessors;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Sets a bool value using an Accessor.
	/// </summary>
	class SetBool : NegatableSetValueEvent<bool>
	{
		public AnyBoolAccessor accessor;
		protected override Accessor<bool> valueAccessor => accessor;

		protected override bool Negate(bool obj)
		{
			return !obj;
		}

		protected override void Reset()
		{
			accessor = new AnyBoolAccessor();
			accessor.Reset(gameObject);
		}
	}
}
