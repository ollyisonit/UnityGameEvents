using System;
using System.Collections;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Plays, pauses, unpauses, or stops an AudioSource.
	/// </summary>
	public class ChangeAudioPlayback : InstantGameEvent
	{
		public enum PlaybackAction
		{
			Play = 0,
			Pause = 1,
			UnPause = 2,
			Stop = 3
		}

		public AudioSource source;
		public PlaybackAction action;

		protected override void InstantEvent()
		{
			switch (action)
			{
				case PlaybackAction.Play:
					source.Play();
					break;
				case PlaybackAction.Stop:
					source.Stop();
					break;
				case PlaybackAction.Pause:
					source.Pause();
					break;
				case PlaybackAction.UnPause:
					source.UnPause();
					break;
				default:
					throw new NotImplementedException("Case not found for " + action);
			}
		}
	}
}