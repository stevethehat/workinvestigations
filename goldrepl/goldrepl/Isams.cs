using System;
using System.Collections.Generic;

namespace GoldRepl
{
    public class Isams
    {
        public void Write(string output)
        {
            Console.WriteLine($"write >> {output}");
        }
        public Dictionary<string, object> Get(string isam)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            result.Add("one", 1);
            result.Add("s", "a string");

            return result;
        }

        public List<T> GetList<T>() where T: new()
        {
            List<T> result = new List<T>();
            result.Add(new T());
            return result;
        }
    }

    /// <summary>
    /// An Arec
    /// </summary>
    public class Arec
    {
        /// <summary>
        /// Description string
        /// </summary>
        public string Description { get; set; }
        public static void Info()
        {
            Console.WriteLine("Arec Info");
        }
    }
}