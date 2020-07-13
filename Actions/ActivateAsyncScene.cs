using dninosores.UnityAccessors;
using System.Collections;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Activates a scene that is currently loading asynchronously.
	/// </summary>
	public class ActivateAsyncScene : InstantGameEvent
	{
		public LoadSceneAsync sceneLoader;
		public BoolOrConstantAccessor allowSceneActivation;

		protected override void Reset()
		{
			base.Reset();
			allowSceneActivation.Value = true;
		}

		protected override void InstantEvent()
		{
			sceneLoader.AllowSceneActivation = allowSceneActivation.Value;
		}
	}
}