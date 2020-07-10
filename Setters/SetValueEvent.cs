using dninosores.UnityAccessors;
using System.Collections;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// A GameEvent that instantly sets a value using an Accessor.
	/// </summary>
	public abstract class SetValueEvent<T> : GameEvent
	{
		public T value;
		protected abstract Accessor<T> valueAccessor
		{
			get;
		}

		protected override bool InstantInternal => true;

		protected override IEnumerator RunInternal()
		{
			valueAccessor.Value = GetTargetValue(valueAccessor.Value, value);
			yield break;
		}

		protected abstract T GetTargetValue(T originalValue, T targetValue);


		public override void Stop()
		{
			// Always happens in one frame, so can't be interrupted.
		}
	}
}
