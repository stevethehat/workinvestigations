using IronPython.Runtime;
//using Net.Ibcos.GoldAPIServer.DataLayer.Models;
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
                OutputDict(obj as PythonDictionary);

                return;
            }

            if (typeof(IEnumerable<object>).IsAssignableFrom(variableType))
            {
                OutputList(obj as IEnumerable<object>);

                return;
            }

            OutputObject(obj);
        }

        public void OutputList(IEnumerable<object> list)
        {
            foreach (object item in list)
            {
                Output(item);
            }
        }

        public void OutputDict(PythonDictionary dictionary)
        {
            foreach (object key in dictionary.Keys)
            {
                _console.Lines.Add(new OutputLine($"{key} = {dictionary[key]}"));
            }
            _console.Lines.Add(new OutputLine(""));
        }

        public void OutputObject(object obj)
        {
            Type variableType = (Type)obj.GetType();

            PropertyInfo[] propertyInfo = variableType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            if (propertyInfo.Any())
            {
                foreach (PropertyInfo property in propertyInfo)
                {
                    try
                    {
                        _console.Lines.Add(new OutputLine($"{property.Name} = {property.GetValue(obj)}"));
                    }
                    catch (Exception e)
                    {
                        _console.Lines.Add(new OutputLine(e.Message, ConsoleColor.Red));
                    }
                }
                
                _console.Lines.Add(new OutputLine(""));
            }
            else
            {
                _console.Lines.Add(new OutputLine(obj.ToString()));
            }
        }
    }
}
