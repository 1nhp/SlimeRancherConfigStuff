using SRML.Console;
using System.Collections.Generic;
using UnityEngine;

namespace srConfigStuff
{
    public class RenderDistanceCommand : ConsoleCommand
    {
        // Init command properties
        public override string ID => "renderdistance";
        public override string Usage => "renderdistance <number>";
        public override string Description => "Set target RenderDistance (-1 for unlimited)";

        public override bool Execute(string[] args)
        {
            if (args.Length == 1 && int.TryParse(args[0], out int renderdistance))
            {
                // Check if framerate is lower or is invalid then show the invalid message
                if (renderdistance < -1 || renderdistance == 0)
                {
                    Debug.LogWarning("Invalid RenderDistance value. Use -1 for unlimited or a positive number.");
                    return false;
                }
                // Save fps values to the config file then set framerate
                Config.RenderDistance = renderdistance;
                Config.Apply();
                Debug.Log($"Target RenderDistance set to {(renderdistance == -1 ? "Unlimited" : renderdistance.ToString())}");

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
                List<string> options = new List<string> { "30", "40", "50", "60", "80", "100", "120", "155", "-1" };
                return options.FindAll(option => option.StartsWith(argText));
            }
            return base.GetAutoComplete(argIndex, argText);
        }
    }
}
