using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Executes GameEvents in the children of the GameObject this script is attached to in child order.
	/// </summary>
	public class GameEventSequence : GameEvent
	{
		/// <summary>
		/// Should events that are attached to the same object as this one be added to the event sequence?
		/// Only events that appear after this object will be added.
		/// </summary>
		[Tooltip("Should events that are attached to the same object as this one be added to the event sequence?" +
			"Only events that appear after this object will be added.")]
		public bool executeOnParent = true;

		protected virtual List<GameEvent> GetContainedEvents()
		{
			List<GameEvent> childEvents = new List<GameEvent>();
			if (executeOnParent)
			{
				childEvents.AddRange(GetAttachedEvents(this));
			}
			childEvents.AddRange(GetChildEvents(transform, recursionMode));
			return childEvents;
		}

		public enum RecursionMode
		{
			OnlyIfEmpty,
			TopLevelOnly,
			Complete
		}

		public RecursionMode recursionMode;

		protected override bool InstantInternal => GetContainedEvents().All(e => e.Instant);

		protected override IEnumerator RunInternal()
		{
			List<GameEvent> childEvents = GetContainedEvents();
			foreach (GameEvent e in childEvents)
			{
				e.SetParentEvent(this);
			}

			foreach (GameEvent e in childEvents)
			{
				if (fastForwarding)
				{
					yield return e.FastForward();
				}
				else
				{
					if (e.Instant)
					{
						e.RunInstant();
					}
					else
					{
						yield return e.Run();
					}
				}
			}
		}


		public override IEnumerator FastForward()
		{
			fastForwarding = true;
			yield return RunInternal();
		}


		/// <summary>
		/// Gets all GameEvents after given GameEvent that are attached to the same GameObject.
		/// </summary>
		public static List<GameEvent> GetAttachedEvents(GameEvent g)
		{
			List<GameEvent> childEvents = new List<GameEvent>();

				bool afterThis = false;
				foreach (GameEvent e in g.GetComponents<GameEvent>())
				{
					if (!afterThis)
					{
						if (e == g)
						{
							afterThis = true;
						}
					}
					else
					{
						childEvents.Add(e);
					}
				}
			return childEvents;

		}


		/// <summary>
		/// Gets all GameEvents that are attached to children of the given transform according to
		/// the given RecursionMode.
		/// </summary>
		public static List<GameEvent> GetChildEvents(Transform t, RecursionMode mode)
		{
			List<GameEvent> events = new List<GameEvent>();

			for (int i = 0; i < t.childCount; i++)
			{
				Transform child = t.GetChild(i);
				GameEvent[] childEvents = child.GetComponents<GameEvent>();
				events.AddRange(childEvents);
				if (mode == RecursionMode.Complete || (mode == RecursionMode.OnlyIfEmpty && childEvents.Length == 0))
				{
					events.AddRange(GetChildEvents(child, mode));
				}
			}

			return events;
		}
	}

}