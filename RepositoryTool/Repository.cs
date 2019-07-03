using System.IO;
using System.Collections.Generic;

namespace Repository{
    public class RepositoryInfo{
        public static string Repository = "C:\\ibcos\\Repositorys\\gold\\repository";
        public static List<string> GetIsamTypes(string filter){
            List<string> result = new List<string>();
            string structureFolder = Path.Combine(RepositoryInfo.Repository, "structure");
            filter = filter.ToLower();

            foreach(string file in Directory.GetFiles(structureFolder)){
                string fileName = Path.GetFileName(file);
                if(Path.GetExtension(fileName).ToLower() == ".ddf"){
                    if(fileName.ToLower().StartsWith(filter)){
                        result.Add(Path.GetFileNameWithoutExtension(fileName));
                    }
                }
            }
            result.Sort();
            return result;
        }
    }
}