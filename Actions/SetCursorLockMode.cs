using UnityEngine;

namespace dninosores.UnityGameEvents.Actions
{
	/// <summary>
	/// Sets lock mode of cursor.
	/// </summary>
	public class SetCursorLockMode : InstantGameEvent
	{
		public CursorLockMode mode;
		protected override void InstantEvent()
		{
			Cursor.lockState = mode;
		}
	}
}
