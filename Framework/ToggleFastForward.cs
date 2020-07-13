using dninosores.UnityAccessors;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Starts or stops fast forwarding in a GameEvent sequence.
	/// </summary>
	public class ToggleFastForward : InstantGameEvent
	{
		public BoolOrConstantAccessor fastForward;

		protected override void InstantEvent()
		{
			if (fastForward.Value)
			{
				StartFastForward();
			}
			else
			{
				StopFastForward();
			}
		}
	}
}