using SRML.Config.Attributes;
using UnityEngine;

namespace srConfigStuff
{
    [ConfigFile("srfpslimiterConfig", "OPTIONS")]
    public static class Config
    {
        [ConfigName("Fps")]
        [ConfigComment("Framerate of the game (-1 for unlimited)")]
        public static int FPS = 60;

        [ConfigName("RenderDistance")]
        [ConfigComment("Render distance of game's viewport (can improve performance)")]
        public static int RenderDistance = 200;

        // Optional: Call this to apply stuff from config
        public static void Apply()
        {
            Application.targetFrameRate = FPS;
            Camera.main.farClipPlane = RenderDistance;
        }
    }
}
