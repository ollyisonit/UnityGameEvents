using dninosores.UnityAccessors;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Prints message to console.
	/// </summary>
	public class PrintMessage : InstantGameEvent
	{
		public StringOrConstantAccessor message;

		protected override void InstantEvent()
		{
			Debug.Log(message.Value);
		}
	}
}