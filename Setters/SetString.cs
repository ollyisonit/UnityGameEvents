using dninosores.UnityAccessors;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Sets a string value using an Accessor.
	/// </summary>
	class SetString : AppendableSetValueEvent<string>
	{
		public AnyStringAccessor accessor;
		protected override Accessor<string> valueAccessor => accessor;

		protected override string Append(string first, string second)
		{
			return first + second;
		}

		protected override void Reset()
		{
			accessor = new AnyStringAccessor();
			accessor.Reset(gameObject);
		}
	}
}
