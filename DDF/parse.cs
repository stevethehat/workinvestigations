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
                        Console.WriteLine($"MATCHED = '{match.Groups[0].Value}'");
                        Console.WriteLine($"NEW LINE = '{line}'");
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
        protected void AddKey(string name, string value){
            var dictionary = (IDictionary<string, object>)properties;

            if(!dictionary.ContainsKey(name)){
                dictionary.Add(name, value);
            }
        }
        protected virtual void ProcessToken(string name, Match match){
        }
    }

    class Structure: BaseElement, IElementParser{
        public Structure(){
            properties.type = "structure";
            properties.fields = new List<IElementParser>();
            Tokens["name"] = @"^Structure\s+([A-Z_]+)";
            Tokens["dbl"] = @"^DBL";
            Tokens["isam"] = @"^ISAM";
        }
        public override IElementParser Attach(IElementParser child){
            properties.fields.Add(child);
            return child;
        }
        protected override void ProcessToken(string name, Match match){
            properties.name = match.Groups[1].Value;
        }
    }

    class Field: BaseElement, IElementParser{
        public Field(): base() {
            properties.type = "field";
            Tokens["name"] = @"^Field\s+([A-Z_0-9]+)";
            Tokens["template"] = @"Template\s+([A-Z_]+)";
            Tokens["fieldtype"] = @"Type\s+([A-Z_]+)";
            Tokens["size"] = @"Size\s+([0-9]+)";
            Tokens["dimensions"] = @"Dimensions\s+([0-9]+)";
            Tokens["precision"] = @"Precision\s+([0-9]+)";

            Tokens["help"] = @"Help\s+""([^""]+)""";
            Tokens["prompt"] = @"Prompt\s+""([^""]+)""";

            Tokens["odbcname"] = @"ODBC Name\s+([A-Z_]+)";
        }
        protected override void ProcessToken(string name, Match match){
            switch(name){
                case "template":
                    properties.template = match.Groups[1].Value;
                    break;
                default:
                    AddKey(name, match.Groups[1].Value);
                    break;
            }
        }
    }

    class Key: BaseElement, IElementParser{
        public Key(): base() {
            properties.type = "key";
            Tokens["name"] = @"^Field\s+([A-Z_0-9]+)";
            Tokens["template"] = @"Template\s+([A-Z_]+)";
        }
        protected override void ProcessToken(string name, Match match){
            switch(name){
                case "template":
                    properties.template = match.Groups[1].Value;
                    break;
                default:
                    AddKey(name, match.Groups[1].Value);
                    break;
            }
        }
    }

    class Dummy: BaseElement, IElementParser{
    }

    public class Parse{
        public Parse(){
            string line = "";
            IElementParser structure = new Dummy();
            IElementParser element = new Dummy();


            using(StreamReader file = new StreamReader("/Users/stevelamb/Development/ibcos/investigations/DDF/repository/structure/PRIREC.DDF")){
                while((line = file.ReadLine()) != null){
                    switch(line){
                        case var l when Regex.Match(line, @"^Structure\s+([A-Z]+)").Success:
                            element = new Structure();
                            structure = element;
                            element.ParseLine(l);
                            break;
                        case var l when Regex.Match(line, @"^Field\s+([A-Za-z0-9_]+)").Success:
                            element = structure.Attach(new Field());
                            element.ParseLine(l);
                            break;
                        case var l when Regex.Match(line, @"^Key\s+([A-Za-z0-9_]+)").Success:
                            element = structure.Attach(new Key());
                            element.ParseLine(l);
                            break;
                        default:
                            if(element != null){
                                element.ParseLine(line);
                            }
                            break;
                    }
                }
            }
            Console.WriteLine(JsonConvert.SerializeObject(structure, Formatting.Indented));
        }
    }
}