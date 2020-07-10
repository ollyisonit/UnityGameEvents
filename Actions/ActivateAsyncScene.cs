using System.Collections;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Activates a scene that is currently loading asynchronously.
	/// </summary>
	public class ActivateAsyncScene : InstantGameEvent
	{
		public LoadSceneAsync sceneLoader;
		public bool allowSceneActivation = true;

		protected override void InstantEvent()
		{
			sceneLoader.AllowSceneActivation = allowSceneActivation;
		}
	}
}