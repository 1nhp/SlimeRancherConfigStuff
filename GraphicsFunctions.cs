using UnityEngine;

namespace srConfigStuff
{
    public static class GraphicsFunctions
    {
        public static void ApplyGraphicsSettings()
        {
            Application.targetFrameRate = (int)IniConfig.Fps;

            foreach (var light in GameObject.FindObjectsOfType<Light>())
            {
                light.enabled = false;
            }

            // Set camera far clip plane
            var cam = Camera.main;
            if (cam != null)
                cam.farClipPlane = IniConfig.RenderDistance;
            else
                Debug.LogWarning("Main camera not found!");
        }
    }
}
