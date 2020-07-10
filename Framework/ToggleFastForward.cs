using System.Collections;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Starts or stops fast forwarding in a GameEvent sequence.
	/// </summary>
	public class ToggleFastForward : GameEvent
	{
		public bool fastForward;
		protected override bool InstantInternal => true;

		public override void Stop()
		{
			// Happens in one frame, so can't be interrupted.
		}

		protected override IEnumerator RunInternal()
		{
			if (fastForward)
			{
				StartFastForward();
			}
			else
			{
				StopFastForward();
			}
			yield break;
		}
	}
}