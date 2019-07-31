﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DblDebug
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            ReadLine.HistoryEnabled = true;
            try
            {
                var result = GoAsync().GetAwaiter().GetResult();

            }
            catch (ArgumentException aex)
            {

            }

            Console.WriteLine("Done");
            Console.ReadKey();  
        }

        private static async Task<bool> GoAsync()
        {
            CoreDebug debug = new CoreDebug("172.16.128.21", 1024);

            bool startResponse = await debug.Start();
            string input = null;
            List<string> response = new List<string>();
            while (default(List<string>) != response)
            {
                input = ReadLine.Read();

                response = await debug.Command(input);

                Console.Write(string.Join("\n", response));
            }

            return startResponse;
        }
    }
}
