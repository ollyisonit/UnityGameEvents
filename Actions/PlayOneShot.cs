#pragma warning disable 0649
using dninosores.UnityAccessors;
using System.Collections;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Plays audio clip from given audio source.
	/// </summary>
	public class PlayOneShot : InstantGameEvent
	{
		public AudioClip clip;
		FloatOrConstantAccessor volumeScale;
		public AudioSource source;


		protected override void Reset()
		{
			base.Reset();
			volumeScale.Value = 1;
		}


		protected override void InstantEvent()
		{
			source.PlayOneShot(clip, volumeScale.Value);
		}
	}
}