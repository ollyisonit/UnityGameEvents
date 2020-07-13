using dninosores.UnityAccessors;
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
		public BoolOrConstantAccessor Enabled;
		protected override void InstantEvent()
		{
			behaviour.enabled = Enabled.Value;
		}
	}
}
