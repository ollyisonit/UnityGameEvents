using dninosores.UnityEditorAttributes;
using dninosores.UnityAccessors;
using System.Collections;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// GameEvent that interpolates between two values over a period of time.
	/// </summary>
	public abstract class AbstractInterpolator<T> : GameEvent
	{
		/// <summary>
		/// Should the interpolation override the current value of the object being interpolated with a new starting value?
		/// </summary>
		public bool overrideStart;
		[ConditionalHide("overrideStart", true)]
		public T start;
		public T end;
		public AnimationCurve curve;
		public float time;

		/// <summary>
		/// Accessor for value to interpolate.
		/// </summary>
		protected abstract Accessor<T> interpolatedValue { get; }

		protected override bool InstantInternal => time == 0;

		protected override void Reset()
		{
			base.Reset();
			interpolatedValue.Reset(gameObject);
			curve = AnimationCurve.Linear(0, 0, 1, 1);
			time = 1;
			overrideStart = false;
		}


		protected override IEnumerator RunInternal()
		{
			float t = 0;

			if (!overrideStart)
			{
				start = interpolatedValue.GetValue();
			}

			while (t < time)
			{
				interpolatedValue.SetValue(Interpolate(start, end, curve.Evaluate(t / time)));
				yield return null;
				t += Time.deltaTime;
			}

			interpolatedValue.SetValue(Interpolate(start, end, curve.Evaluate(1)));
		
		}


		/// <summary>
		/// Linear interpolation between start and end values where fraction=0 returns start and fraction=1 returns end.
		/// </summary>
		protected abstract T Interpolate(T start, T end, float fraction);
		
	}
}