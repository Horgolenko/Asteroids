#if !UNITY_EDITOR
using UnityEngine;
#endif
using Game;
using UnityEngine.SceneManagement;

namespace Scene
{
    public static class SceneLoader
    {
        public static void StartGame()
        {
            Load(SceneName.Game);
        }
        
        public static void LoadMainMenu()
        {
            Load(SceneName.MainMenu);
        }
        
        public static void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private static void Load(string sceneName)
        {
            var asyncOperation = SceneManager.LoadSceneAsync(SceneName.Loading);
            asyncOperation.completed += _ => SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
