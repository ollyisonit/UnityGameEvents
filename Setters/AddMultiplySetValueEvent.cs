using dninosores.UnityAccessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dninosores.UnityGameEvents
{
	public abstract class AddMultiplySetValueEvent<T> : SetValueEvent<T>
	{
		public enum WriteMode
		{
			Overwrite = 0,
			Add = 1,
			Multiply = 2
		}
		public WriteMode writeMode;

		protected override Accessor<T> valueAccessor => throw new NotImplementedException();

		protected override T GetTargetValue(T originalValue, T targetValue)
		{
			switch (writeMode) {
				case WriteMode.Overwrite:
					return targetValue;
				case WriteMode.Add:
					return Add(originalValue, targetValue);
				case WriteMode.Multiply:
					return Multiply(originalValue, targetValue);
				default:
					throw new NotImplementedException("Case not found for write mode " + writeMode);
			}
		}

		protected virtual T Add(T a, T b)
		{
			return ReflectionUtil.EvaluateBinaryOperator("op_Addition", a, b);
		}


		protected virtual T Multiply(T a, T b)
		{
			return ReflectionUtil.EvaluateBinaryOperator("op_Multiply", a, b);
		}
	}
}
