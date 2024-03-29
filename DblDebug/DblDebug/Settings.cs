﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DblDebug
{
    public class Settings
    {
        private readonly Dictionary<string, string> _values = new Dictionary<string, string>(){
            { "autoviewlines", "10" },
            { "ripgrepfilename", "rg" }
        };

        public Settings()
        {
            HomeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            SettingsDirectory = Path.Combine(HomeDirectory, ".dbldebug");
            if (false == Directory.Exists(SettingsDirectory))
            {
                Directory.CreateDirectory(SettingsDirectory);
            }

            SettingsFileName = Path.Combine(SettingsDirectory, "settings.json");

            if(true == File.Exists(SettingsFileName))
            {
                string settings = File.ReadAllText(SettingsFileName);
                _values = JsonConvert.DeserializeObject<Dictionary<string, string>>(settings);
            }
        }

        public void Save()
        {
            File.WriteAllText(SettingsFileName, JsonConvert.SerializeObject(_values));
        }

        public IEnumerable<string> Keys { get => _values.Keys; }
        public string HomeDirectory { get; }
        public string SettingsDirectory { get; }
        public string SettingsFileName { get; }

        internal string Get(string setting)
        {
            string result = default(string);

            if (_values.ContainsKey(setting))
            {
                result = _values[setting];
            }

            return result;
        }

        internal Task<bool> Set(CoreDebug debug, string enteredCommand)
        {
            string[] command = enteredCommand.Split(' ');

            if (2 == command.Length && "?" == command[1])
            {
                var settings = _values.OrderBy(kv => kv.Key)
                                      .Select(kv => new OutputLine($"{kv.Key} = {kv.Value}"));

                debug.Outputs.General.Lines.AddRange(settings);
                /*
                foreach (KeyValuePair<string, string> savedSetting in _values)
                {
                    debug.Outputs.General.Lines.Add(new OutputLine($"{savedSetting.Key} = {savedSetting.Value}"));
                }
                */
            }

            if (3 == command.Length)
            {
                string setting = command[1];
                string value = command[2];

                if (_values.ContainsKey(setting))
                {
                    _values[setting] = value;
                }
                else
                {
                    _values.Add(setting, value);
                }
            }

            return Task.FromResult<bool>(true);
        }
    }
}
