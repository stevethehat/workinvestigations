using System;
using System.IO;

namespace DDF{
    public class Parse{
        public Parse(){
            string line;
            using(StreamReader file = new StreamReader("/Users/stevelamb/Development/ibcos/investigations/DDF/repository/structure/PRIREC.DDF")){
                while((line = file.ReadLine()) != null){
                    Console.WriteLine(line);
                }
            }
        }
    }
}