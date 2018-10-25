using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;

namespace Repository{
    class Structure: BaseElement, IElementParser{
        public Structure(): base(){
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
    }

    class FieldType: BaseElement, IElementParser{
        public FieldType(): base() {
            Tokens["fieldtype"] = @"^Type\s+([A-Z_]+)";
            Tokens["size"] = @"^Size\s+([0-9]+)";
            Tokens["precision"] = @"^Precision\s+([0-9]+)";
            Tokens["prompt"] = @"^Prompt\s+""([^""]+)""";
            Tokens["infoline"] = @"^Info Line\s+""([^""]+)""";
            Tokens["dimension"] = @"^Dimension\s+([0-9]+)";


            Tokens["selectionlist"] = @"^Selection List(.+)";
            //Selection List 1 2 0  Entries "No", "Yes
            Tokens["odbcname"] = @"^ODBC Name\s+([A-Z_0-9]+)";
            Tokens["method"] = @"^([A-Za-z_]+) Method\s+""([^""]+)""";

            Tokens["promptfont"] = @"^Promptfont\s+([A-Z_]+)";
            Tokens["font"] = @"^Font\s+([A-Z_]+)";
        }        
    }

    class Field: FieldType, IElementParser{
        public Field(): base() {
            properties.type = "field";
            Tokens["name"] = @"^Field\s+([A-Z_0-9]+)";
            Tokens["template"] = @"^Template\s+([A-Z_0-9]+)";

            Tokens["longdescription"] = @"^Long Description";            
            Tokens["subdescription"] = @"^""([^""]+)""";            

            Tokens["help"] = @"^Help\s+""([^""]+)""";

            //Prompt "Value:"   Info Line "Enter an Alphanumeric Value"   Font ALPHA_FONT

        }
        protected override void ProcessToken(string name, Match match){
            switch(name){
                case "template":
                    DDFFile template = new DDFFile("template", match.Groups[1].Value);
                    properties.template = new {
                        name = match.Groups[1].Value,
                        structure = template.Parse()
                    };
                    break;
                case "longdescription":
                    properties.longdescription = new List<string>();
                    break;
                case "subdescription":
                    if(!ContainsKey("longdescription")){
                        properties.longdescription = new List<string>();
                    }
                    properties.longdescription.Add(match.Groups[1].Value);
                    break;
                default:
                    DefaultProcessToken(name, match);
                    break;
            }
        }
    }

    class Key: BaseElement, IElementParser{
        public Key(): base() {
            properties.type = "key";
            Tokens["name"] = @"^Key\s+([A-Z_0-9]+)";
        }
    }
    class Tag: BaseElement, IElementParser{
        public Tag(): base() {
            properties.type = "tag";
            Tokens["name"] = @"^Tag\s+([A-Z_0-9]+)";
        }
    }

    class Template: FieldType, IElementParser{
        public Template(): base() {
            properties.type = "template";
            Tokens["name"] = @"^Template\s+([A-Z_0-9]+)";
            Tokens["parent"] = @"^Parent\s+([A-Z_0-9]+)";
        }
        protected override void ProcessToken(string name, Match match){
            switch(name){
                case "parent":
                    DDFFile parent = new DDFFile("template", match.Groups[1].Value);
                    properties.parent = new {
                        name = match.Groups[1].Value,
                        structure = parent.Parse()
                    };
                    break;
                default:
                    DefaultProcessToken(name, match);
                    break;
            }
        }
    }

    public class DDFFile{
        readonly string _fileName;
        dynamic root;
        public DDFFile(string section, string name){
            //string root = @"repository";
            string root = RepositoryInfo.Repository;
            _fileName = Path.Combine(root, section, $"{name}.ddf");
        }

        public dynamic Parse(){
            string line = "";
            IElementParser root = new Dummy();
            IElementParser element = new Dummy();


            using(StreamReader file = new StreamReader(_fileName)){
                while((line = file.ReadLine()) != null){
                    switch(line){
                        case var l when Regex.Match(line, @"^Structure\s+([A-Z]+)").Success:
                            element = new Structure();
                            root = element;
                            break;
                        case var l when Regex.Match(line, @"^Field\s+([A-Za-z0-9_]+)").Success:
                            element = root.Attach(new Field());
                            break;
                        case var l when Regex.Match(line, @"^Key\s+([A-Za-z0-9_]+)").Success:
                            element = root.Attach(new Key());
                            break;
                        case var l when Regex.Match(line, @"^Tag\s+([A-Za-z0-9_]+)").Success:
                            element = root.Attach(new Tag());
                            break;
                        case var l when Regex.Match(line, @"^Template\s+([A-Za-z0-9_]+)").Success:
                            element = new Template();
                            root = element;
                            break;
                        default:
                            break;
                    }
                    if(element != null){
                        element.ParseLine(line);
                    }
                }
            }
            this.root = root;
            return root;
        }

        public void Save(string fileName){
            string outputFile = Path.Combine("output", $"{fileName}.json");
            string json = JsonConvert.SerializeObject(this.root, Formatting.Indented);
            
            if(System.IO.File.Exists(outputFile)){
                System.IO.File.Delete(outputFile);
            }
            System.IO.File.WriteAllText(outputFile, json);

        }
    }
}