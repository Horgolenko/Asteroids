#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Editor
{
	[InitializeOnLoad]
	public class AutosaveOnRun
	{
		static AutosaveOnRun()
		{
			EditorApplication.playModeStateChanged += (PlayModeStateChange obj) =>
			{
				if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
				{
					Debug.Log("Auto-Saving scene before entering Play mode: " + EditorSceneManager.GetActiveScene().name);

					EditorSceneManager.SaveOpenScenes();
					AssetDatabase.SaveAssets();
				}
			};
		}
	}
}

#endif