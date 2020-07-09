namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Starts GameEvent when scene is started.
	/// </summary>
	public class TriggerOnStart : GameEventTrigger
	{
		void Start()
		{
			StartEvent();
		}
	}
}