using dninosores.UnityEditorAttributes;
using System;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Triggers a GameEvent to start based on some condition in the scene.
	/// </summary>
	public abstract class GameEventTrigger : MonoBehaviour
	{
		[Tooltip("If this box is checked, the GameEventTrigger will automatically reference other GameEvents attached to this object")]
		public bool automatic;

		public enum TriggerMode
		{
			Single,
			Multiple
		}

		public TriggerMode triggerMode;

		[Serializable]
		public class ArrayContainer
		{
			public GameEvent[] gameEvents;
		}

		[ConditionalHide(new string[] { "automatic", "triggerMode" }, new object[] { false, TriggerMode.Multiple })]
		public ArrayContainer gameEvents;

		[ConditionalHide(new string[] { "automatic", "triggerMode" }, new object[] { false, TriggerMode.Single })]
		public GameEvent gameEvent;



		/// <summary>
		/// Sets editor default values. Don't forget to call base.Reset() if overriding!
		/// </summary>
		protected void Reset()
		{
			automatic = true;
			triggerMode = TriggerMode.Single;
			gameEvents = new ArrayContainer();
			gameEvents.gameEvents = new GameEvent[0];
		}


		/// <summary>
		/// Call this method at the appropriate time to start the contained event.
		/// </summary>
		protected void StartEvent()
		{
			if (isActiveAndEnabled)
			{
				if (automatic)
				{
					switch (triggerMode)
					{

						case TriggerMode.Multiple:
							foreach (GameEvent e in GetComponents<GameEvent>())
							{
								e.StartRunning();
							}
							break;
						case TriggerMode.Single:
							GetComponent<GameEvent>().StartRunning();
							break;
					}
				}
				else
				{
					switch (triggerMode)
					{
						case TriggerMode.Single:
							gameEvent?.StartRunning();
							break;
						case TriggerMode.Multiple:
							foreach (GameEvent e in gameEvents.gameEvents)
							{
								e.StartRunning();
							}
							break;
					}
				}
			}
		}

	}
}