using dninosores.UnityAccessors;
using System;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Sets a value that can be overwritten or appended to.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class AppendableSetValueEvent<T> : SetValueEvent<T>
	{
		public enum WriteMode
		{
			Overwrite = 0,
			Append = 1
		}

		public WriteMode writeMode;

		protected abstract T Append(T first, T second);

		protected override T GetTargetValue(T originalValue, T targetValue)
		{
			switch (writeMode)
			{
				case WriteMode.Overwrite:
					return targetValue;
				case WriteMode.Append:
					return Append(originalValue, targetValue);
				default:
					throw new NotImplementedException("Case not found for " + writeMode);
			}
		}
	}
}
