using UnityEngine.UI;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Triggers GameEvent when button is pressed.
	/// </summary>
	public class TriggerOnButton : GameEventTrigger
	{
		public Button button;

		void Start()
		{
			button.onClick.AddListener(StartEvent);
		}

		void OnDestroy()
		{
			button.onClick.RemoveListener(StartEvent);
		}
	}
}