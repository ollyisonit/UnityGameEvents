using dninosores.UnityAccessors;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Sets a float value using an Accessor.
	/// </summary>
	class SetFloat : AddMultiplySetValueEvent<float>
	{
		public AnyFloatAccessor accessor;
		protected override Accessor<float> valueAccessor => accessor;

		protected override void Reset()
		{
			accessor = new AnyFloatAccessor();
			accessor.Reset(gameObject);
		}
	}
}
