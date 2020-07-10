using System.Collections;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Waits for given time in seconds.
	/// </summary>
	public class WaitSeconds : GameEvent
	{
		public float time;
		protected override bool InstantInternal => time == 0;

		private bool cancelled;

		public override void Stop()
		{
			cancelled = true;
		}

		protected override IEnumerator RunInternal()
		{
			float t = time;
			cancelled = false;
			while (t > 0 && !cancelled)
			{
				yield return null;
				t -= Time.deltaTime;
			}
		}

		public override void ForceRunInstant()
		{
			// Do nothing, just skip the wait.
		}
	}
}