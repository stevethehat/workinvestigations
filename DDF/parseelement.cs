using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;

namespace DDF{
    interface IElementParser{
        void ParseLine(string line);
        IElementParser Attach(IElementParser parent);
    }

    class BaseElement{
        public dynamic properties;
        protected Dictionary<string, string> Tokens;
        public BaseElement(){
            properties = new ExpandoObject();
            Tokens = new Dictionary<string, string>();
            Tokens["description"] = @"Description\s+""([^""]+)""";
            Tokens["description2"] = @"Description\s+'([^']+)'";
        }
        public virtual IElementParser Attach(IElementParser child){
            return child;
        }
        public void ParseLine(string line){
            Match match;
            line = line.TrimStart();
            while(line != ""){
                bool matched = false;
                foreach(KeyValuePair<string, string> token in Tokens){
                    match = Regex.Match(line, token.Value);
                    if(match.Success){
                        ProcessToken(token.Key, match);
                        line = line.Substring(match.Groups[0].Value.Length).TrimStart(' ');
                        //Console.WriteLine($"MATCHED = '{match.Groups[0].Value}'");
                        //Console.WriteLine($"NEW LINE = '{line}'");
                        matched = true;
                        break;
                    }
                }
                if(!matched){
                    Console.WriteLine($"NOT MATCHED '{line}'");
                    //Console.ReadLine();
                    break;
                }
            }
        }

        protected void DefaultProcessToken(string name, Match match){
            switch(name){
                case "method":
                    string key = match.Groups[1].Value;
                    string value = match.Groups[2].Value;
                    if(!ContainsKey("methods")){
                        properties.methods = new Dictionary<string, string>();
                    }
                    properties.methods[key] = value;
                    break;
                default:
                    AddKey(name, match.Groups[1].Value);        
                    break;
            }
        }


        protected void AddKey(string name, string value){
            var dictionary = (IDictionary<string, object>)properties;

            if(!dictionary.ContainsKey(name)){
                dictionary.Add(name, value);
            }
        }
        protected bool ContainsKey(string name){
            var dictionary = (IDictionary<string, object>)properties;

            return dictionary.ContainsKey(name);
        }
        protected virtual void ProcessToken(string name, Match match){
            AddKey(name, match.Groups[1].Value);
        }
    }    
    class Dummy: BaseElement, IElementParser{
    
    }
}