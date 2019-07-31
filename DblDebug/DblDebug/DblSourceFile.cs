using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DblDebug
{
    public class DblSourceFile
    {
        private readonly string _fileName;
        private readonly string _fullFileName;
        private readonly string _sourceDirectory = "/Users/stevelamb/Development/ibcos/investigations/source/";
        private readonly List<Scope> _functions = new List<Scope>();
        private readonly List<Scope> _subRoutines = new List<Scope>();
        private List<string> _lines;

        public DblSourceFile(string fileName)
        {
            _fileName = fileName;
            _fullFileName = GetFullFileName(_fileName);
            Parse(_fullFileName);
        }

        private string GetFullFileName(string fileName)
        {
            string result = default(string);

            var sourceDirectories = Directory.GetDirectories(_sourceDirectory);
            foreach(string directory in sourceDirectories)
            {
                string fullFileName = Path.Combine(_sourceDirectory, directory, fileName);

                if(true == File.Exists(fullFileName))
                {
                    result = fullFileName;
                    break;
                }
            }

            return result;
        }

        private void Parse(string fileName)
        {
            _lines = File.ReadAllLines(fileName).ToList();

            foreach(string line in _lines)
            {
                string trimmedLine = line.Trim();
                if(true == line.StartsWith("function"))
                {
                    _functions.Add(new Scope());
                }
                if (true == line.StartsWith("subroutine"))
                {
                    _subRoutines.Add(new Scope());
                }
            }

            Console.WriteLine($"{_functions.Count} functions");
            Console.WriteLine($"{_subRoutines.Count} subroutines");
        }
    }

    public class Scope
    {
        public Scope()
        {

        }
    }
}
