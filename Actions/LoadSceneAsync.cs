using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace dninosores.UnityGameEvents
{
	/// <summary>
	/// Loads scene asynchronosly and returns when the scene is loaded.
	/// You can connect this to an ActivateLoadSceneAsync object to control when the scene appears in-game.
	/// </summary>
	public class LoadSceneAsync : GameEvent
	{
		protected override bool InstantInternal => false;
		public string sceneName;
		public bool AllowSceneActivation
		{
			get => allowSceneActivation;
			set
			{
				allowSceneActivation = value;
				if (loadOperation != null)
				{
					loadOperation.allowSceneActivation = allowSceneActivation;
				}

			}
		}
		[SerializeField]
		private bool allowSceneActivation;
		public LoadSceneMode loadMode;

		private AsyncOperation loadOperation;

		public override void Stop()
		{
			Debug.LogWarning("Once a LoadSceneAsync operation is started, it cannot be aborted.");
			AllowSceneActivation = false;
		}

		protected override IEnumerator RunInternal()
		{
			loadOperation = SceneManager.LoadSceneAsync(sceneName, loadMode);
			loadOperation.allowSceneActivation = allowSceneActivation;
			while (!loadOperation.isDone)
			{
				yield return null;
			}

		}


		public override void ForceRunInstant()
		{
			SceneManager.LoadScene(sceneName, loadMode);
		}
	}
}