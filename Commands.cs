using SRML.Console;
using System.Collections.Generic;
using UnityEngine;

namespace srConfigStuff
{
    //
    //
    // Renderdistance command
    //
    //

    public class renderdistanceCommand : ConsoleCommand
    {
        // Init command properties
        public override string ID => "renderdistance";
        public override string Usage => "renderdistance <number>";
        public override string Description => "Set target RenderDistance (-1 for unlimited)";

        public override bool Execute(string[] args)
        {
            return CommandUtils.TryApplyIntSetting(
                args,
                Usage,
                "RenderDistance",
                renderdistance =>
                {
                    var cam = Camera.main;
                    if (!cam)
                    {
                        Debug.LogWarning("Camera not ready yet.");
                        return;
                    }

                    cam.farClipPlane = renderdistance == -1 ? 10000f : renderdistance;
                    IniConfig.Save(renderDistance: renderdistance);
                }
            );
        }


        private static readonly string[] RenderDistanceOptions =
        {
            "30", "40", "50", "60", "80", "100", "120", "155", "-1"
        };

        public override List<string> GetAutoComplete(int argIndex, string argText)
        {
            return CommandUtils.AutoCompleteFromOptions(argIndex, argText, RenderDistanceOptions)
                ?? base.GetAutoComplete(argIndex, argText);
        }

        //
        //
        // Fps command
        //
        //

        public class FpsCommand : ConsoleCommand
        {
            // Init command properties
            public override string ID => "fps";
            public override string Usage => "fps <number>";
            public override string Description => "Set target FPS (-1 for unlimited)";

            public override bool Execute(string[] args)
            {
                return CommandUtils.TryApplyIntSetting(
                    args,
                    Usage,
                    "Target FPS",
                    fps =>
                    {
                        QualitySettings.vSyncCount = 0;
                        Application.targetFrameRate = fps;
                        IniConfig.Save(fps);
                    }
                );
            }

            private static readonly string[] FpsOptions =
            {
            "30", "60", "120", "144", "165", "240", "360", "500", "-1"
        };

            public override List<string> GetAutoComplete(int argIndex, string argText)
            {
                return CommandUtils.AutoCompleteFromOptions(argIndex, argText, FpsOptions)
                    ?? base.GetAutoComplete(argIndex, argText);
            }
        }

        //
        //
        // lighting command
        //
        //

        public class LightingCommand : ConsoleCommand
        {
            // Init command properties
            public override string ID => "lighting";
            public override string Usage => "lighting <true/false>";
            public override string Description => "Set lighting false will disable it true will enable it";

            public override bool Execute(string[] args)
            {
                return CommandUtils.TryApplyBoolSetting(
                    args,
                    Usage,
                    "lighting",
                    lighting =>
                    {
                        foreach (var light in GameObject.FindObjectsOfType<Light>())
                        {
                            light.enabled = false;
                            IniConfig.Save(lighting: lighting);
                        }
                    }
                );
            }

            private static readonly string[] lightingOptions =
            {
            "false", "true"
            };

            public override List<string> GetAutoComplete(int argIndex, string argText)
            {
                return CommandUtils.AutoCompleteFromOptions(argIndex, argText, lightingOptions)
                    ?? base.GetAutoComplete(argIndex, argText);
            }
        }


    }
}
