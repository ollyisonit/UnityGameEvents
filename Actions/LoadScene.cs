using System.Collections;
using UnityEngine.SceneManagement;

namespace dninosores.UnityGameEvents
{
	public class LoadScene : InstantGameEvent
	{
		public string sceneName;
		public LoadSceneMode loadMode;

		protected override void InstantEvent()
		{
			SceneManager.LoadScene(sceneName, loadMode);
		}
	}
}
