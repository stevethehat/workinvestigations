using System;
using System.IO;
using System.Text;

namespace filestest
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "/Users/stevelamb/Development/ibcos/filestest/2018-06-01-09-03-50.txt";
            string line;
            Console.WriteLine("Hello World!");

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            //using(StreamReader file = File.OpenText(fileName)){
            using(StreamReader file = new StreamReader(fileName, Encoding.GetEncoding(1252), true)){
                Console.WriteLine($"loader for {fileName}");
                while((line = file.ReadLine()) != null){
                    Console.WriteLine(line);
                    line = line.Replace("\u2122", "(TM)");
                    line = line.Replace("\u20AC", "EURO");
                    Console.WriteLine(line);
                }
            }
        }
    }
}
