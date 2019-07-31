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
            foreach(OutputLine line in Lines)
            {
                line.Write();
            }
        }

        public IEnumerable<OutputLine> Lines { get; set; } = new List<OutputLine>();
    }

    public class OutputLine
    {
        public List<OutputChunk> Line { get; set; } = new List<OutputChunk>();

        public OutputLine(string line)
        {
            Line.Add(new OutputChunk()
            {
                Text = line
            });
        }
        public void Write()
        {
            foreach(OutputChunk chunk in Line)
            {
                Console.BackgroundColor = chunk.BackgroundColor;
                Console.ForegroundColor = chunk.ForgrioundColor;
                Console.Write(chunk.Text);
            }

            Console.Write("\n");
        }
    }

    public class OutputChunk
    {
        public string Text { get; set; }
        public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.Black;
        public ConsoleColor ForgrioundColor { get; set; } = ConsoleColor.White;
    }
}
