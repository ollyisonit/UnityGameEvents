using System.Collections;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Plays audio clip from given audio source.
	/// </summary>
	public class PlayOneshot : InstantGameEvent
	{
		public AudioClip clip;
		float volumeScale = 1;
		public AudioSource source;

		protected override void InstantEvent()
		{
			source.PlayOneShot(clip, volumeScale);
		}
	}
}