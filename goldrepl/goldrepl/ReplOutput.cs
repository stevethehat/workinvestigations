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

            if(variableType.Name == "String")
            {
                Console.WriteLine(obj);
                Console.WriteLine();
                return;
            }

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
                Console.WriteLine($"{key} = {dictionary[key]}");
            }
            Console.WriteLine("");
        }

        public void OutputObject(object obj)
        {
            Type variableType = (Type)obj.GetType();

            PropertyInfo[] propertyInfo = variableType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            if (propertyInfo.Any())
            {
                int width = propertyInfo.Max(p => p.Name.Length);
                foreach (PropertyInfo property in propertyInfo)
                {
                    try
                    {
                        Console.WriteLine($"{property.Name.PadRight(width, ' ')} = {property.GetValue(obj)}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine(obj.ToString());
                Console.WriteLine("");
            }
        }

        public void Info(Type type)
        {
            //Table table = new Table();
            MethodInfo[] methodInfo = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            var methods = methodInfo.Where(m => !(m.Name.StartsWith("get_") || m.Name.StartsWith("set_")))
                                    .Select(mi => new OutputLine(mi.Name))
                                    .ToList();
            //Console.Lines.AddRange(methods);

            PropertyInfo[] propertyInfo = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            var properties = propertyInfo.Select(p => new OutputLine(p.Name)).ToList();

            foreach(PropertyInfo property in propertyInfo)
            {
                //table.Lines.Add(new TableLine(new string[] { property.Name, "col2" }));
            }

            //table.Output(Console);
            //Console.Lines.AddRange(properties);
            Console.Write(true);
        }
    }

    /*
    public class Table
    {
        public List<TableLine> Lines { get; set; } = new List<TableLine>();
        public void Output(ConsoleOutput output)
        {
            foreach(TableLine line in Lines)
            {
                line.Output(output);
            }
        }
    }

    public class TableLine
    {
        public TableLine(string[] columns)
        {
            _columns = columns;
        }
        private string[] _columns;

        internal void Output(ConsoleOutput output)
        {
            output.Lines.Add(new OutputLine(string.Join(" ", _columns)));
        }
    }
    */
}
