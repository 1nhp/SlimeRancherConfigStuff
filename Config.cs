using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public static class IniConfig
{
    // Config values with defaults
    public static int Fps = 60;
    public static float RenderDistance = 100f;
    public static bool Lighting = true;

    private static readonly string ConfigFileName = "config.ini";

    public static string ConfigFolder =>
        Path.Combine(Application.persistentDataPath, "SRML", "Config", "srconfigstuff");

    public static string ConfigPath =>
        Path.Combine(ConfigFolder, ConfigFileName);

    // Load INI file
    public static void Load()
    {
        try
        {
            Directory.CreateDirectory(ConfigFolder);

            // Create default config if missing
            if (!File.Exists(ConfigPath))
            {
                Save(); // writes defaults
                return;
            }

            // Map config keys to parsing actions
            var parsers = new Dictionary<string, Action<string>>(StringComparer.OrdinalIgnoreCase)
            {
                { "fps", s => { if (int.TryParse(s, out int val)) Fps = val; } },
                { "renderDistance", s => { if (float.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out float val)) RenderDistance = val; } },
                { "lighting", s => { if (bool.TryParse(s, out bool val)) Lighting = val; } }
            };

            foreach (var line in File.ReadAllLines(ConfigPath))
            {
                var trimmed = line.Trim();
                if (string.IsNullOrWhiteSpace(trimmed) || trimmed.StartsWith(";"))
                    continue; // skip empty lines or comments

                var parts = trimmed.Split(new[] { '=' }, 2);
                if (parts.Length != 2)
                    continue;

                var key = parts[0].Trim();
                var value = parts[1].Trim();

                if (parsers.TryGetValue(key, out var parseAction))
                    parseAction(value);
            }

            Debug.Log($"[srconfigstuff] Loaded FPS = {Fps}, RenderDistance = {RenderDistance}, Lighting = {Lighting}");
        }
        catch (Exception e)
        {
            Debug.LogError($"[srconfigstuff] Failed to load INI: {e}");
        }
    }

    // Save INI file
    public static void Save(int? fps = null, float? renderDistance = null, bool? lighting = null)
    {
        try
        {
            Directory.CreateDirectory(ConfigFolder);

            if (fps.HasValue) Fps = fps.Value;
            if (renderDistance.HasValue) RenderDistance = renderDistance.Value;
            if (lighting.HasValue) Lighting = lighting.Value;

            string content =
            $@"fps = {Fps}
renderDistance = {RenderDistance.ToString(CultureInfo.InvariantCulture)}
lighting = {Lighting}";



            File.WriteAllText(ConfigPath, content);

            Debug.Log($"[srconfigstuff] Saved FPS = {Fps}, RenderDistance = {RenderDistance}, Lighting = {Lighting}");
        }
        catch (Exception e)
        {
            Debug.LogError($"[srconfigstuff] Failed to save INI: {e}");
        }
    }
}
