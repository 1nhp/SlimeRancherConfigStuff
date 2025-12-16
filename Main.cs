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
            RemoveRGB(Camera.main);

            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = Config.FPS;
        }

        private void RemoveRGB(Camera cam)
        {
            if (cam == null) return;

            var renderers = cam.GetComponentsInChildren<Renderer>(true);
            foreach (var r in renderers)
            {
                foreach (var mat in r.materials)
                {
                    if (mat == null) continue;
                    string shaderName = mat.shader.name.ToLower();
                    if (shaderName.Contains("rgb") || shaderName.Contains("chromatic"))
                    {
                        // Replace with a simple shader
                        Shader plain = Shader.Find("Unlit/Texture");
                        if (plain != null) mat.shader = plain;
                        SRML.Console.Console.Log($"Replaced shader {shaderName} with Unlit/Texture");
                    }
                }
            }
        }
    }
}