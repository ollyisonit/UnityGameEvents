#pragma warning disable 0649
using dninosores.UnityAccessors;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Sets a string value using an Accessor.
	/// </summary>
	class SetString : AppendableSetValueEvent<string>
	{
		public AnyStringAccessor accessor;
		public StringOrConstantAccessor Value;
		protected override Accessor<string> valueAccessor => accessor;

		protected override string value => Value.Value;

		protected override string Append(string first, string second)
		{
			return first + second;
		}

	}
}
