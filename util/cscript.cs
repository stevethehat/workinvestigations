using System;
using CSScriptLibrary;

namespace Api.Util{
    public class CScript{
        public void Test(){
            var test = CSScript.Evaluator.CreateDelegate<int>("int test(){return 10;}");
            var result = test();

        }
    }
}