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
            result.Add(new T());
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
        public string Description { get; set; } = "Description";
        public double Value { get; set; }
        public string AMethod()
        {
            return "";
        }
        public static void Info()
        {
            Console.WriteLine("Arec Info");
        }

        public Arec()
        {
            Random r = new Random();
            Value = r.NextDouble() * 1000;
        }
    }
}