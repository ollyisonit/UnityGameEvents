using System;
using UnityEditor;
using UnityEngine;

namespace dninosores.UnityGameEvents
{
	public class MenuItems
	{
		private static void Create<T>(MenuCommand menuCommand) where T : Component
		{

			GameObject go = new GameObject(typeof(T).Name);
			Component c = go.AddComponent<T>();
			GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
			Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
			Selection.activeObject = go;
		}


		/// <summary>
		/// Directory to put menu items in
		/// </summary>
		private const string dir = "GameObject/GameEvents/";
		private const string framedir = dir + "Framework/";
		private const string gendir = dir + "General/";
		private const string setdir = dir + "Setters/";
		private const string terdir = dir + "Interpolators/";
		private const string auddir = dir + "Audio/";
		private const string scedir = dir + "Scene/";
		private const string animdir = dir + "Animation/";

		[MenuItem(framedir + "Sequence", false, 10)]
		public static void Sequence(MenuCommand m)
		{
			Create<GameEventSequence>(m);
		}

		[MenuItem(framedir + "Parallel", false, 10)]
		public static void Parallel(MenuCommand m)
		{
			Create<GameEventParallel>(m);
		}



		[MenuItem(framedir + "Fast-Forward", false, 10)]
		public static void FF(MenuCommand m)
		{
			Create<ToggleFastForward>(m);
		}

		[MenuItem(framedir + "Loop", false, 10)]
		public static void Loop(MenuCommand m)
		{
			Create<LoopGameEvent>(m);
		}

		[MenuItem(framedir + "Stop", false, 10)]
		public static void Stop(MenuCommand m)
		{
			Create<StopGameEvent>(m);
		}


		[MenuItem(framedir + "Print Message", false, 10)]
		public static void Print(MenuCommand m)
		{
			Create<PrintMessage>(m);
		}



		[MenuItem(framedir + "Wait Seconds", false, 10)]
		public static void Sec(MenuCommand m)
		{
			Create<WaitSeconds>(m);
		}

		[MenuItem(framedir + "Wait For GameEvent", false, 10)]
		public static void Gam(MenuCommand m)
		{
			Create<WaitForGameEvent>(m);
		}

		[MenuItem(gendir + "Call Reflected", false, 10)]
		public static void Refl(MenuCommand m)
		{
			Create<CallReflected>(m);
		}

		[MenuItem(gendir + "Set Active", false, 10)]
		public static void Act(MenuCommand m)
		{
			Create<SetActive>(m);
		}


		[MenuItem(gendir + "Set Enabled", false, 10)]
		public static void En(MenuCommand m)
		{
			Create<SetEnabled>(m);
		}

		[MenuItem(scedir + "Load Scene Async", false, 10)]
		public static void SLA(MenuCommand m)
		{
			Create<LoadSceneAsync>(m);
		}


		[MenuItem(scedir + "Load Scene", false, 10)]
		public static void LS(MenuCommand m)
		{
			Create<LoadScene>(m);
		}


		[MenuItem(scedir + "Activate Async Scene", false, 10)]
		public static void ActivateScene(MenuCommand m)
		{
			Create<ActivateAsyncScene>(m);
		}

		[MenuItem(auddir + "Change Audio Playback", false, 10)]
		public static void AudioPlayback(MenuCommand m)
		{
			Create<AudioPlayback>(m);
		}

		[MenuItem(auddir + "Mixer Snapshot Transition", false, 10)]
		public static void Trans(MenuCommand m)
		{
			Create<MixerSnapshotTransition>(m);
		}

		[MenuItem(auddir + "Play Oneshot", false, 10)]
		public static void PlayOne(MenuCommand m)
		{
			Create<PlayOneShot>(m);
		}

		[MenuItem(animdir + "Set Animation Parameter", false, 10)]
		public static void Anim(MenuCommand m)
		{
			Create<SetAnimationParameter>(m);
		}

		[MenuItem(setdir + "Set Bool", false, 10)]
		public static void Bool(MenuCommand m)
		{
			Create<SetBool>(m);
		}

		[MenuItem(setdir + "Set Int", false, 10)]
		public static void Int(MenuCommand m)
		{
			Create<SetInt>(m);
		}

		[MenuItem(setdir + "Set Float", false, 10)]
		public static void Float(MenuCommand m)
		{
			Create<SetFloat>(m);
		}

		[MenuItem(setdir + "Set String", false, 10)]
		public static void Str(MenuCommand m)
		{
			Create<SetString>(m);
		}

		[MenuItem(setdir + "Set Vector2", false, 10)]
		public static void V2(MenuCommand m)
		{
			Create<SetVector2>(m);
		}

		[MenuItem(setdir + "Set Vector3", false, 10)]
		public static void V3(MenuCommand m)
		{
			Create<SetVector3>(m);
		}

		[MenuItem(setdir + "Set Color", false, 10)]
		public static void Col(MenuCommand m)
		{
			Create<SetColor>(m);
		}

		[MenuItem(terdir + "Interpolate Int", false, 10)]
		public static void LerpInt(MenuCommand m)
		{
			Create<IntInterpolator>(m);
		}

		[MenuItem(terdir + "Interpolate Float", false, 10)]
		public static void LerpFloat(MenuCommand m)
		{
			Create<FloatInterpolator>(m);
		}

		[MenuItem(terdir + "Interpolate Vector2", false, 10)]
		public static void Lerpv2(MenuCommand m)
		{
			Create<Vector2Interpolator>(m);
		}

		[MenuItem(terdir + "Interpolate Vector3", false, 10)]
		public static void LerpV3(MenuCommand m)
		{
			Create<Vector3Interpolator>(m);
		}

		[MenuItem(terdir + "Interpolate Color", false, 10)]
		public static void LerpCkor(MenuCommand m)
		{
			Create<ColorInterpolator>(m);
		}
	}
}