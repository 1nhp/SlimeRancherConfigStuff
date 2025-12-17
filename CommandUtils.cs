using System;
using System.Collections.Generic;
using UnityEngine;

namespace srConfigStuff
{
    public static class CommandUtils
    {
        public static bool TryApplyIntSetting(
            string[] args,
            string usage,
            string settingName,
            Action<int> applyValue)
        {
            if (args.Length != 1 || !int.TryParse(args[0], out int value))
            {
                Debug.LogWarning($"Usage: {usage}");
                return false;
            }

            if (value < -1 || value == 0)
            {
                Debug.LogWarning($"Invalid {settingName} value. Use -1 or a positive number.");
                return false;
            }

            applyValue(value);

            Debug.Log($"{settingName} set to {(value == -1 ? "Unlimited" : value.ToString())}");
            return true;
        }

        public static bool TryApplyBoolSetting(
            string[] args,
            string usage,
            string settingName,
            Action<bool> applyValue)
        {
            if (args.Length != 1)
            {
                Debug.LogWarning($"Usage: {usage}");
                return false;
            }

            string input = args[0].ToLower();
            bool value;

            if (input == "true")
                value = true;
            else if (input == "false")
                value = false;
            else
            {
                Debug.LogWarning($"Invalid {settingName} value. Use true or false.");
                return false;
            }

            applyValue(value);

            Debug.Log($"{settingName} set to {value}");
            return true;
        }


        public static List<string> AutoCompleteFromOptions(
                int argIndex,
                string argText,
                IEnumerable<string> options)
            {
                if (argIndex != 0)
                    return null;

                List<string> results = new List<string>();
                foreach (var option in options)
                {
                    if (option.StartsWith(argText))
                        results.Add(option);
                }
                return results;
        }

        public static List<string> AutoCompleteBool(int argIndex, string argText)
        {
            if (argIndex != 0)
                return null;

            List<string> results = new List<string>();
            foreach (var option in new[] { "true", "false" })
            {
                if (option.StartsWith(argText.ToLower()))
                    results.Add(option);
            }
            return results;
        }
    }
}
