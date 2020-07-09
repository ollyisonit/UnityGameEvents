
using System;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Sets a value that can be overwritten or negated.
	/// </summary>
	public abstract class NegatableSetValueEvent<T> : SetValueEvent<T>
	{
		public enum WriteMode
		{
			Overwrite,
			Negate
		}

		public WriteMode writeMode;

		protected abstract T Negate(T obj);


		protected override T GetTargetValue(T originalValue, T targetValue)
		{
			switch (writeMode)
			{
				case WriteMode.Overwrite:
					return targetValue;
				case WriteMode.Negate:
					return Negate(originalValue);
				default:
					throw new NotImplementedException("Case not found for " + writeMode);
			}
		}
	}
}
