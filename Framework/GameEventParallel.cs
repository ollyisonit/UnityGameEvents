using dninosores.UnityEditorAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Runs GameEvents in children simultaneously.
	/// </summary>
	public class GameEventParallel : GameEvent
	{
		public enum ParallelMode
		{
			OnSelf = 0,
			InChildren = 1
		}

		public ParallelMode mode;
		[ConditionalHide("mode", ParallelMode.InChildren)]
		public RecursionMode recursionMode;


		/// <summary>
		/// Exposes fields in GameEventSequence so that different instances of it can effectively be run at the same time.
		/// </summary>
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

		/// <summary>
		/// The GameEvents that are currently running in parallel.
		/// </summary>
		private List<GameEvent> currentlyRunning;
		private List<InternalSequence> currentlyRunningSequences;


		protected override void Reset()
		{
			base.Reset();
			mode = ParallelMode.InChildren;
		}


		protected override bool InstantInternal
		{
			get
			{
				switch (mode)
				{
					case ParallelMode.OnSelf:
						return GameEventSequence.GetAttachedEvents(this).All(e => e.Instant);
					case ParallelMode.InChildren:
						return GameEventSequence.GetChildEvents(transform, recursionMode).All(e => e.Instant);
					default:
						throw new NotImplementedException("Case not found for " + mode);
				}
			}
		}


		protected override IEnumerator RunInternal()
		{
			switch (mode)
			{
				case ParallelMode.InChildren:
					List<InternalSequence> sequences = new List<InternalSequence>();

					foreach (Transform child in transform)
					{
						List<GameEvent> events = new List<GameEvent>();
						events.AddRange(child.GetComponents<GameEvent>());
						events.AddRange(GameEventSequence.GetChildEvents(child,
							recursionMode));
						InternalSequence sequence = child.gameObject.AddComponent<InternalSequence>();
						sequence.GameEvents = events;
						sequence.SetFastForward(base.fastForwarding);
						sequences.Add(sequence);
					}
					currentlyRunningSequences = sequences;
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
					currentlyRunning = attachedEvents;
					foreach (GameEvent e in attachedEvents)
					{
						e.SetParentEvent(this);
					}
					yield return RunUntilCompletion(attachedEvents);
					break;

			}

		}


		public override IEnumerator FastForward()
		{
			fastForwarding = true;
			yield return RunInternal();	
		}
		

		public override void ForceRunInstant()
		{
			switch (mode)
			{
				case ParallelMode.InChildren:
					foreach (GameEvent e in GameEventSequence.GetChildEvents(transform, recursionMode))
					{
						e.SetParentEvent(this);
						e.ForceRunInstant();
					}
					break;
				case ParallelMode.OnSelf:
					foreach (GameEvent e in GameEventSequence.GetAttachedEvents(this))
					{
						e.SetParentEvent(this);
						e.ForceRunInstant();
					}
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
				else if (fastForwarding)
				{
					yield return s.FastForward();
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
				else if (fastForwarding)
				{
					yield return s.FastForward();
				}
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

		public override void Stop()
		{
			if (currentlyRunning != null)
			{
				foreach (GameEvent e in currentlyRunning)
				{
					e.Stop();
				}
			}
			if (currentlyRunningSequences != null)
			{
				foreach (InternalSequence s in currentlyRunningSequences)
				{
					s.Stop();
				}
			}
		}
	}
}