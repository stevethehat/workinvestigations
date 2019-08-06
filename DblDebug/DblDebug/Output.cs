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

        public List<OutputLine> Lines { get; set; } = new List<OutputLine>();
    }

    public class OutputLine
    {
        public static OutputLine Blank = new OutputLine("");
        public List<OutputChunk> Line { get; set; } = new List<OutputChunk>();

        public OutputLine(
            string          line, 
            ConsoleColor    foregroundColor = ConsoleColor.White, 
            ConsoleColor    backgroundColor = ConsoleColor.Black
        )
        {
            Line.Add(new OutputChunk(line, backgroundColor, foregroundColor));
        }

        public OutputLine()
        {

        }

        public static void WriteLine
        (
            string line,
            ConsoleColor foregroundColor = ConsoleColor.White,
            ConsoleColor backgroundColor = ConsoleColor.Black
        )
        {
            OutputLine output = new OutputLine(line, foregroundColor, backgroundColor);
            output.Write();
        }

        public void Write()
        {
            foreach(OutputChunk chunk in Line)
            {
                Console.BackgroundColor = chunk.BackgroundColor;
                Console.ForegroundColor = chunk.ForegroundColor;
                Console.Write(chunk.Text);
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n");
        }
    }

    public class OutputChunk
    {
        public OutputChunk(
            object text,
            ConsoleColor foregroundColor = ConsoleColor.White,
            ConsoleColor backgroundColor = ConsoleColor.Black
        )
        {
            Text = text.ToString();
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }
        public readonly string Text;
        public readonly ConsoleColor BackgroundColor = ConsoleColor.Black;
        public readonly ConsoleColor ForegroundColor = ConsoleColor.White;
    }
}
