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
        public readonly string FileName;
        private readonly string _fullFileName;
        //private readonly string _sourceDirectory = "/Users/stevelamb/Development/ibcos/investigations/source/";
        private readonly string _sourceDirectory = "c:\\ibcos\\Repositorys\\gold\\source\\";
        private readonly List<Scope> _functions = new List<Scope>();
        private readonly List<Scope> _subRoutines = new List<Scope>();
        private List<string> _lines;

        private const int CONTEXT = 10;

        public DblSourceFile(string fileName)
        {
            FileName = fileName;
            _fullFileName = GetFullFileName(FileName);
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
                try
                {
                    string trimmedLine = line.Trim();
                    if (true == line.StartsWith("function"))
                    {
                        _functions.Add(new Scope());
                    }
                    if (true == line.StartsWith("subroutine"))
                    {
                        _subRoutines.Add(new Scope());
                    }
                } catch (Exception e)
                {

                }
            }

            Console.WriteLine($"{_functions.Count} functions");
            Console.WriteLine($"{_subRoutines.Count} subroutines");
        }

        internal void SetCode(ConsoleOutput code, int lineNumber)
        {
            code.Lines.Clear();

            int codeLineNo = lineNumber - 1;

            for(int i = lineNumber - CONTEXT; i < lineNumber + CONTEXT; i++){
                try
                {
                    if (codeLineNo == i)
                    {
                        code.Lines.Add(new OutputLine($"{i,10:d} > {_lines[i +1].Trim(new[] { '\n', '\r' })}", ConsoleColor.White, ConsoleColor.Red));
                    }
                    else
                    {
                        code.Lines.Add(new OutputLine($"{i,10:d} > {_lines[i +1].Trim(new[] { '\n', '\r' })}"));
                    }
                } catch (Exception e)
                {

                }
            }
            /*
            foreach(string line in _lines.GetRange(lineNumber - 20, 19))
            {
                code.Lines.Add(new OutputLine(line));
            }

            code.Lines.Add(new OutputLine(_lines[lineNumber], ConsoleColor.White, ConsoleColor.Red));

            foreach (string line in _lines.GetRange(lineNumber + 1, 19))
            {
                code.Lines.Add(new OutputLine(line));
            }
            */
        }
    }

    public class Scope
    {
        public Scope()
        {

        }
    }
}
