using IronPython.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GoldRepl
{
    public partial class Repl
    {
        public void Output(object obj)
        {
            Type variableType = (Type)obj.GetType();

            if (variableType.Name == "PythonDictionary")
            {
                PythonDictionary dictionary = obj as PythonDictionary;
                foreach (object key in dictionary.Keys)
                {
                    Console.WriteLine($"{key} = {dictionary[key]}");
                }

                return;
            }

            PropertyInfo[] propertyInfo = variableType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (PropertyInfo property in propertyInfo)
            {
                try
                {
                    Console.WriteLine($"{property.Name} = {property.GetValue(obj)}");
                }
                catch (Exception e)
                {

                }
            }
        }
    }
}
