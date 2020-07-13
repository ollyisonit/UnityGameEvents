using dninosores.UnityAccessors;
using System.Collections;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// A GameEvent that instantly sets a value using an Accessor.
	/// </summary>
	public abstract class SetValueEvent<T> : InstantGameEvent
	{
		/// <summary>
		/// Value to set accessor to.
		/// </summary>
		protected abstract T value { get; }

		/// <summary>
		/// Accessor whose value will be set
		/// </summary>
		protected abstract Accessor<T> valueAccessor
		{
			get;
		}

		protected override void InstantEvent()
		{
			valueAccessor.Value = GetTargetValue(valueAccessor.Value, value);
		}

		protected abstract T GetTargetValue(T originalValue, T targetValue);

	}
}
