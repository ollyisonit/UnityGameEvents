using System.Collections;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Enables or disables a behaviour.
	/// </summary>
	public class SetEnabled : InstantGameEvent
	{
		public Behaviour behaviour;
		public bool Enabled;
		protected override void InstantEvent()
		{
			behaviour.enabled = Enabled;
		}
	}
}
