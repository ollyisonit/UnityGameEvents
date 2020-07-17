using System;
namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Stops the given GameEvent.
	/// </summary>
	public class StopGameEvent : InstantGameEvent
	{
		public GameEvent gameEvent;
		protected override void InstantEvent()
		{
			gameEvent.Stop();
		}
	}
}
