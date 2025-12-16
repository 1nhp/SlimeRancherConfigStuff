using SRML;
using SRML.Console;
using SRML.Utils.Enum;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace srConfigStuff
{
    public class Main : ModEntryPoint
    {
        public override void PreLoad()
        {
            HarmonyInstance.PatchAll();
            SRML.Console.Console.RegisterCommand(new FpsCommand());
            SRML.Console.Console.RegisterCommand(new RenderDistanceCommand());
        }

        public override void Load()
        {
            QualitySettings.vSyncCount = 0;
            Config.Apply();
            Application.targetFrameRate = Config.FPS;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (Camera.main != null)
                Camera.main.farClipPlane = Config.RenderDistance;

            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = Config.FPS;
        }
    }
}