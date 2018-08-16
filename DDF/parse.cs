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
        public dynamic Properties;
        protected Dictionary<string, string> Tokens;
        public BaseElement(){
            Properties = new ExpandoObject();
            Tokens = new Dictionary<string, string>();
        }
        public virtual IElementParser Attach(IElementParser child){
            return child;
        }
        public void ParseLine(string line){
            Match match;
            foreach(KeyValuePair<string, string> token in Tokens){
                match = Regex.Match(line, token.Value);
                if(match.Success){
                    ProcessToken(token.Key, match);
                }
            }
        }
        protected virtual void ProcessToken(string name, Match match){
            //match 
        }
    }

    class Structure: BaseElement, IElementParser{
        public Structure(){
            Properties.Type = "Structure";
            Properties.Fields = new List<IElementParser>();

            Tokens["name"] = @"^Structure\s+([A-Z]+)";
        }
        public override IElementParser Attach(IElementParser child){
            Properties.Fields.Add(child);
            return child;
        }
        protected override void ProcessToken(string name, Match match){
            Properties.Name = match.Groups[1].Value;
        }
    }

    class Field: BaseElement, IElementParser{
        public Field(): base() {
            Properties.Type = "Field";
            Tokens["name"] = @"^Field\s+([A-Z_]+)";
        }
        protected override void ProcessToken(string name, Match match){
            Properties.Name = match.Groups[1].Value;
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