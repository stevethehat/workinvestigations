using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DblDebug
{
    public class ConsoleOutput
    {
        public void Write()
        {
            Console.Write(string.Join("\n", Lines));
        }

        public List<string> Lines { get; set; } = new List<string>();
    }
}
