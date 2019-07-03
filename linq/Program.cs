using System;

namespace linq
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            LinqTests tests = new LinqTests();
            tests.RunTest1();
            tests.RunTest2();
            tests.RunTest3();
        }
    }
}
