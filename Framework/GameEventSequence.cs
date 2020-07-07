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
		public enum RecursionMode
		{
			TopLevelOnly,
			OnlyIfEmpty,
			Complete
		}

		public RecursionMode recursionMode;
		protected override bool InstantInternal => GetChildEvents(transform, recursionMode).All(e => e.Instant);

		protected override IEnumerator RunInternal()
		{
			foreach (GameEvent e in GetChildEvents(transform, recursionMode))
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

		private static List<GameEvent> GetChildEvents(Transform t, RecursionMode mode)
		{
			List<GameEvent> events = new List<GameEvent>();

			for (int i = 0; i < t.childCount; i++)
			{
				Transform child = t.GetChild(i);
				GameEvent[] childEvents = child.GetComponents<GameEvent>();
				events.AddRange(childEvents);
				if (mode == RecursionMode.Complete || (mode == RecursionMode.OnlyIfEmpty && childEvents.Length == 0))
				{
					events.AddRange(GetChildEvents(t, mode));
				}
			}

			return events;
		}
	}

}