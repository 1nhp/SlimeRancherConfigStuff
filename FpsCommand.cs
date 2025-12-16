using SRML.Console;
using System.Collections.Generic;
using UnityEngine;

namespace srConfigStuff
{
    public class FpsCommand : ConsoleCommand
    {
        // Init command properties
        public override string ID => "fps";
        public override string Usage => "fps <number>";
        public override string Description => "Set target FPS (-1 for unlimited)";

        public override bool Execute(string[] args)
        {
            if (args.Length == 1 && int.TryParse(args[0], out int fps))
            {
                // Check if framerate is lower or is invalid then show the invalid message
                if (fps < -1 || fps == 0)
                {
                    Debug.LogWarning("Invalid FPS value. Use -1 for unlimited or a positive number.");
                    return false;
                }
                // Save fps values to the config file then set framerate
                Config.FPS = fps;
                Config.Apply();
                Debug.Log($"Target FPS set to {(fps == -1 ? "Unlimited" : fps.ToString())}");

                return true;
            }

            Debug.LogWarning($"Usage: {Usage}");
            return false;
        }

        // Show autocomplete options
        public override List<string> GetAutoComplete(int argIndex, string argText)
        {
            if (argIndex == 0)
            {
                List<string> options = new List<string> { "30", "60", "120", "144", "165", "240", "360", "500", "-1" };
                return options.FindAll(option => option.StartsWith(argText));
            }
            return base.GetAutoComplete(argIndex, argText);
        }
    }
}
