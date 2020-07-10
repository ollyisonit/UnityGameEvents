using System.Collections;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Enables or disables a gameObject.
	/// </summary>
	public class SetActive : InstantGameEvent
	{
		public GameObject GameObject;
		public bool active;
		protected override void InstantEvent()
		{
			GameObject.SetActive(active);
		}
	}
}
