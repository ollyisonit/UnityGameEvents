using System.Collections;

namespace dninosores.UnityGameEvents
{
	public class WaitForGameEvent : GameEvent
	{
		public GameEvent gameEvent;
		protected override bool InstantInternal => !gameEvent.InProgress;

		private bool cancelled;
		public override void Stop()
		{
			cancelled = true;
		}

		protected override IEnumerator RunInternal()
		{
			while (!cancelled && gameEvent.InProgress)
			{
				yield return null;
			}
		}

		public override void ForceRunInstant()
		{
			// Do nothing, just skip the wait.
		}
	}
}