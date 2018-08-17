using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;

namespace DDF{
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
            Tokens["precision"] = @"Precision\s+([0-9]+)";

            Tokens["dimension"] = @"Dimension\s+([0-9]+)";
            Tokens["longdescription"] = @"Long Description";            
            Tokens["subdescription"] = @"^""([^""]+)""";            

            Tokens["help"] = @"Help\s+""([^""]+)""";
            Tokens["prompt"] = @"Prompt\s+""([^""]+)""";
            Tokens["infoline"] = @"Info Line\s+""([^""]+)""";

            //Prompt "Value:"   Info Line "Enter an Alphanumeric Value"   Font ALPHA_FONT

            Tokens["odbcname"] = @"ODBC Name\s+([A-Z_]+)";
        }
        protected override void ProcessToken(string name, Match match){
            switch(name){
                case "template":
                    properties.template = match.Groups[1].Value;
                    break;
                case "longdescription":
                Console.WriteLine("Long description");
                    //Console.ReadLine();
                    properties.subdescriptions = new List<string>();
                    break;
                case "subdescription":
                    if(!ContainsKey("subdescriptions")){
                        properties.subdescriptions = new List<string>();
                    }
                    //Console.WriteLine($"sub desc '{match.Groups[1].Value}'");
                    //Console.ReadLine();
                    properties.subdescriptions.Add(match.Groups[1].Value);
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
    class Tag: BaseElement, IElementParser{
        public Tag(): base() {
            properties.type = "tag";
        }
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
                            break;
                        case var l when Regex.Match(line, @"^Field\s+([A-Za-z0-9_]+)").Success:
                            element = structure.Attach(new Field());
                            break;
                        case var l when Regex.Match(line, @"^Key\s+([A-Za-z0-9_]+)").Success:
                            element = structure.Attach(new Key());
                            break;
                        case var l when Regex.Match(line, @"^Tag\s+([A-Za-z0-9_]+)").Success:
                            element = structure.Attach(new Tag());
                            break;
                        default:
                            break;
                    }
                    if(element != null){
                        element.ParseLine(line);
                    }
                }
            }
            Console.WriteLine(JsonConvert.SerializeObject(structure, Formatting.Indented));
        }
    }
}