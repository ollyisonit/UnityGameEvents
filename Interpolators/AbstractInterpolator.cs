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
		[Header("Curve Settings")]
		public bool relative;

		public enum RelativeMode
		{
			Add = 0,
			Multiply = 1
		}
		[ConditionalHide("relative", true)]
		public RelativeMode relativeMode;
		/// <summary>
		/// Should interpolation read end value once or re-read it every frame?
		/// </summary>
		[Tooltip("Check this box if the end value of the interpolation shouldn't change over time (for example, if the end value is random or stationary).")]
		public bool FreezeEnd;

		/// <summary>
		/// Start value for interpolation.
		/// </summary>
		protected abstract T start
		{
			get;
		}
		/// <summary>
		/// End value for interpolation.
		/// </summary>
		protected abstract T end
		{
			get;
		}

		/// <summary>
		/// Curve to use for easing interpolation.
		/// </summary>
		public AnimationCurve curve;
		/// <summary>
		/// How many seconds the interpolation should take.
		/// </summary>
		public float time;
		/// <summary>
		/// Should the interpolation override the current value of the object being interpolated with a new starting value?
		/// </summary>
		public bool overrideStart;
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
			ResetAccessors.Reset(this, gameObject);
			FreezeEnd = true;
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
			T originalValue = interpolatedValue.Value;

			T overriddenStart = start;
			T frozenEnd = end;

			if (!overrideStart)
			{
				if (relative)
				{
					overriddenStart = default;
				}
				else
				{
					overriddenStart = interpolatedValue.Value;
				}
			}

			while (t < time && !cancelled)
			{
				SetInterpolatedValue(originalValue, Interpolate(overriddenStart, FreezeEnd ? frozenEnd : end, curve.Evaluate(t / time)));
				yield return null;
				t += Time.deltaTime;
			}
			if (!cancelled)
			{
				SetInterpolatedValue(originalValue, Interpolate(overriddenStart, FreezeEnd ? frozenEnd : end, curve.Evaluate(1)));
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