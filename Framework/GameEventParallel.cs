using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Runs GameEvents in children 
	/// </summary>
	public class GameEventParallel : GameEvent
	{
		public enum ParallelMode
		{
			OnSelf,
			InChildren
		}

		public ParallelMode mode;


		private class InternalSequence : GameEventSequence
		{
			public List<GameEvent> GameEvents;
			public bool FastForwarding => fastForwarding;

			public void SetFastForward(bool fastForward)
			{
				fastForwarding = fastForward;
			}

			protected override List<GameEvent> GetContainedEvents()
			{
				return GameEvents;
			}
		}


		protected override bool InstantInternal => false;


		protected override IEnumerator RunInternal()
		{
			switch (mode)
			{
				case ParallelMode.InChildren:
					List<InternalSequence> sequences = new List<InternalSequence>();

					foreach (Transform child in transform)
					{
						List<GameEvent> events = GameEventSequence.GetChildEvents(child,
							GameEventSequence.RecursionMode.OnlyIfEmpty);
						InternalSequence sequence = child.gameObject.AddComponent<InternalSequence>();
						sequence.GameEvents = events;
						sequence.SetFastForward(base.fastForwarding);
					}

					yield return RunUntilCompletion(sequences);

					if (sequences.Any(s => s.FastForwarding))
					{
						StartFastForward();
					}
					else
					{
						StopFastForward();
					}
					break;
				case ParallelMode.OnSelf:
					List<GameEvent> attachedEvents = GameEventSequence.GetAttachedEvents(this);
					yield return RunUntilCompletion(attachedEvents);
					break;

			}

		}

		private IEnumerator RunUntilCompletion(List<InternalSequence> events)
		{
			int finishedCount = 0;
			IEnumerator RunSequence(GameEvent s)
			{
				if (s.Instant)
				{
					s.RunInstant();
				}
				else
				{
					yield return s.Run();
				}
				finishedCount += 1;
			}

			foreach (GameEvent s in events)
			{
				StartCoroutine(RunSequence(s));
			}

			while (finishedCount < events.Count)
			{
				yield return null;
			}
		}




		private IEnumerator RunUntilCompletion(List<GameEvent> events)
		{
			int finishedCount = 0;
			IEnumerator RunSequence(GameEvent s)
			{
				if (s.Instant)
				{
					s.RunInstant();
				}
				else
				{
					yield return s.Run();
				}
				finishedCount += 1;
			}

			foreach (GameEvent s in events)
			{
				StartCoroutine(RunSequence(s));
			}

			while (finishedCount < events.Count)
			{
				yield return null;
			}
		}
	}
}