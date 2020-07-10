using dninosores.UnityEditorAttributes;
using dninosores.UnityAccessors;
using System.Collections;
using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// GameEvent that interpolates between two values over a period of time.
	/// </summary>
	public abstract class AbstractInterpolator<T> : GameEvent
	{
		/// <summary>
		/// Should the interpolation happen relative to the object's starting value or set the value absolutely?
		/// </summary>
		public bool relative;
		public enum RelativeMode
		{
			Add = 0,
			Multiply = 1
		}
		[ConditionalHide("relative", true)]
		public RelativeMode relativeMode;
		/// <summary>
		/// Should the interpolation override the current value of the object being interpolated with a new starting value?
		/// </summary>
		[Header("Curve Settings")]
		public bool overrideStart;
		/// <summary>
		/// Start value for interpolation.
		/// </summary>
		[ConditionalHide("overrideStart", true)]
		public T start;
		/// <summary>
		/// End value for interpolation.
		/// </summary>
		public T end;
		/// <summary>
		/// Curve to use for easing interpolation.
		/// </summary>
		public AnimationCurve curve;
		/// <summary>
		/// How many seconds the interpolation should take.
		/// </summary>
		public float time;

		/// <summary>
		/// Has the interpolation been cancelled?
		/// </summary>
		private bool cancelled;


		/// <summary>
		/// Accessor for value to interpolate.
		/// </summary>
		protected abstract Accessor<T> interpolatedValue {
			get;
		}

		protected override bool InstantInternal => time == 0;

		protected override void Reset()
		{
			base.Reset();
			interpolatedValue.Reset(gameObject);
			curve = AnimationCurve.Linear(0, 0, 1, 1);
			time = 1;
			overrideStart = false;
		}


		/// <summary>
		/// Sums two values of type T together.
		/// </summary>
		protected virtual T Sum(T a, T b)
		{
			Type type = typeof(T);
			MethodInfo sum = type.GetMethod("op_Addition", new Type[] { typeof(T), typeof(T) });
			if (sum == null)
			{
				throw new NotImplementedException("No suitible addition operator found! You need to " +
					"override the Multiply method for " + typeof(T));
			}
			return (T) sum.Invoke(a, new object[] { a, b });
		}


		/// <summary>
		/// Multiplies two values of type T together.
		/// </summary>
		protected virtual T Multiply(T a, T b)
		{
			Type type = typeof(T);
			MethodInfo sum = type.GetMethod("op_Multiply", new Type[] { typeof(T), typeof(T) });
			if (sum == null)
			{
				throw new NotImplementedException("No suitible multiplication operator found! You need to " +
					"override the Multiply method for " + typeof(T));
			}
			return (T)sum.Invoke(a, new object[] { a, b });
		}


		protected override IEnumerator RunInternal()
		{
			cancelled = false;
			float t = 0;
			T originalValue = interpolatedValue.GetValue();

			if (!overrideStart)
			{
				if (relative)
				{
					start = default;
				}
				else
				{
					start = interpolatedValue.GetValue();
				}
			}

			while (t < time && !cancelled)
			{
				SetInterpolatedValue(originalValue, Interpolate(start, end, curve.Evaluate(t / time)));
				yield return null;
				t += Time.deltaTime;
			}
			if (!cancelled)
			{
				SetInterpolatedValue(originalValue, Interpolate(start, end, curve.Evaluate(1)));
			}
		}


		public override void Stop()
		{
			cancelled = true;
		}


		private void SetInterpolatedValue(T originalValue, T targetValue)
		{
			if (relative)
			{
				switch (relativeMode)
				{
					case RelativeMode.Add:
						interpolatedValue.Value = Sum(originalValue, targetValue);
						break;
					case RelativeMode.Multiply:
						interpolatedValue.Value = Multiply(originalValue, targetValue);
						break;
				}
			}
			else
			{
				interpolatedValue.Value = targetValue;
			}
		}


		/// <summary>
		/// Linear interpolation between start and end values where fraction=0 returns start and fraction=1 returns end.
		/// For fraction values less than zero or greater than one, a value will be extrapolated based on the start and end values.
		/// </summary>
		protected abstract T Interpolate(T start, T end, float fraction);
		
	}
}