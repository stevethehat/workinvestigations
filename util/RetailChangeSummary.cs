using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Util {
    public class RetailChangeSummary {
        public AppDb Db { get; private set; }
        public string CompareField { get; private set; }
        public int CurrentId { get; private set; }
        public int ImportId { get; private set; }

        public int Down { get; private set; }
        public int Same { get; private set; }
        public int Up { get; private set; }


        public RetailChangeSummary(AppDb db, int currentId, int importId, string compareField) {
            Db = db;
            CompareField = compareField;
            CurrentId = currentId;
            ImportId = importId;
        }

        public void GetSummary() {
            Scanner scanner = new Scanner(Db, CurrentId, ImportId, CompareField);

            scanner.Changed = (current, import) => {
                if(current.Retail < import.Retail) {
                    Up++;
                }
                if (current.Retail > import.Retail) {
                    Down++;
                }
                return false;
            };
            scanner.Unchanged = (current, import) => {
                Same++;
                return false;
            };

            scanner.Scan();
        }


    }
}
