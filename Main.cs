using SRML;
using SRML.Console;
using UnityEngine;
using UnityEngine.SceneManagement;
using static srConfigStuff.renderdistanceCommand;

namespace srConfigStuff
{
    public class Main : ModEntryPoint
    {
        public override void PreLoad()
        {
            HarmonyInstance.PatchAll();
            Console.RegisterCommand(new renderdistanceCommand());
            Console.RegisterCommand(new FpsCommand());
            Console.RegisterCommand(new LightingCommand());
        }

        public override void Load()
        {
            IniConfig.Load();

            GraphicsFunctions.ApplyGraphicsSettings();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public override void Unload()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            GraphicsFunctions.ApplyGraphicsSettings();
        }
    }
}
