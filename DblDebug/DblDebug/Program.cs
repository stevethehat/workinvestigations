﻿using System;
using DblDebug.Views;

namespace DblDebug
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            App app = new App();
            /*
            CoreDebug debug = new CoreDebug("localhost", 1024);

            string input = "";
            while (debug.Command(input))
            {
                Console.Write("DBG>");
                input = Console.ReadLine();

            }
            */
            Console.WriteLine("Done");
            Console.ReadKey();  
        }
    }
}
