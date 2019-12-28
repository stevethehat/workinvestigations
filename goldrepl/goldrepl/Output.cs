using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldRepl
{
    public class _ConsoleOutput
    {
        public int MaxLines { get; set; } = 40;
        public void Write(bool clear = false)
        {
            int count = 0;
            foreach (OutputLine line in Lines)
            {
                line.Write();
                count++;

                if(MaxLines == count)
                {
                    Console.WriteLine("Press anykey to continue...");
                    Console.ReadLine();
                    count = 0;
                }
            }
            if(true == clear)
            {
                Lines.Clear();
            }
        }

        public List<OutputLine> Lines { get; set; } = new List<OutputLine>();
    }

    public class OutputLine
    {
        public static OutputLine Blank = new OutputLine("");
        public List<OutputChunk> Line { get; set; } = new List<OutputChunk>();

        public OutputLine(
            string line,
            ConsoleColor foregroundColor = ConsoleColor.White,
            ConsoleColor backgroundColor = ConsoleColor.Black
        )
        {
            Line.Add(new OutputChunk(line, foregroundColor, backgroundColor));
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
            foreach (OutputChunk chunk in Line)
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
