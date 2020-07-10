using System.Collections;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Starts or stops fast forwarding in a GameEvent sequence.
	/// </summary>
	public class ToggleFastForward : InstantGameEvent
	{
		public bool fastForward;

		protected override void InstantEvent()
		{
			if (fastForward)
			{
				StartFastForward();
			}
			else
			{
				StopFastForward();
			}
		}
	}
}