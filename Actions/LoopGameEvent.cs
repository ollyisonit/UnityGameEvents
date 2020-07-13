using dninosores.UnityAccessors;
using System.Collections;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Runs another game event a specified number of times.
	/// </summary>
	public class LoopGameEvent : GameEvent
	{
		/// <summary>
		/// GameEvent to loop.
		/// </summary>
		public GameEvent gameEvent;
		/// <summary>
		/// Should the amount of times to run be recalcuated after each loop?
		/// </summary>
		public bool dynamic;
		/// <summary>
		/// Number of times to loop gameEvent.
		/// </summary>
		public IntOrConstantAccessor timesToRun;
		protected override bool InstantInternal => gameEvent.Instant;

		private bool cancelled;

		public override void Stop()
		{
			cancelled = true;
			gameEvent.Stop();
		}

		protected override IEnumerator RunInternal()
		{
			int loopCount = timesToRun.Value;
			cancelled = false;
			for (int i = 0; i < loopCount; i++)
			{
				if (dynamic)
				{
					loopCount = timesToRun.Value;
				}
				if (cancelled)
				{
					break;
				}
				if (gameEvent.Instant)
				{
					gameEvent.RunInstant();
				}
				else
				{
					yield return gameEvent.Run();
				}
			}
		}
	}
}