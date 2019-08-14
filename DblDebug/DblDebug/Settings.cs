using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DblDebug
{
    public class Settings
    {
        private readonly Dictionary<string, string> _values = new Dictionary<string, string>(){
            { "autocompletelines", "10" }
        };

        public Settings()
        {
        }

        public IEnumerable<string> Keys { get => _values.Keys; }

        internal string Get(string setting)
        {
            string result = default(string);

            if (_values.ContainsKey(setting))
            {
                result = _values[setting];
            }

            return result;
        }

        internal Task<bool> Set(string enteredCommand)
        {
            string[] command = enteredCommand.Split(' ');
            if(3 == command.Length)
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
