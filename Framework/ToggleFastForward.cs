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