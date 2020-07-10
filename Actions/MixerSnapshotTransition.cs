using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Transitions to an AudioMixerSnapshot over a period of time.
	/// </summary>
	public class MixerSnapshotTransition : GameEvent
	{
		protected override bool InstantInternal => throw new System.NotImplementedException();
		public AudioMixerSnapshot snapshot;
		public float transitionTime;

		public override void Stop()
		{
			Debug.LogWarning("Mixer snapshot transition cannot be aborted.");
		}

		protected override IEnumerator RunInternal()
		{
			snapshot.TransitionTo(transitionTime);
			yield return new WaitForSeconds(transitionTime);
		}
	}
}