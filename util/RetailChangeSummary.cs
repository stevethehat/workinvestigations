using System;
using System.Collections.Generic;
using System.Text;

//using Xunit;

namespace Api.Util {
    public class RetailChangeSummary {
        public AppDb Db { get; private set; }
        public string CompareField { get; private set; }
        public int CurrentId { get; private set; }
        public int ImportId { get; private set; }

        public int Down { get; private set; }
        public float MaxPercentageDown { get; private set; }
        public int Same { get; private set; }
        public int Up { get; private set; }
        public float MaxPercentageUp { get; private set; }


        public RetailChangeSummary(AppDb db, int currentId, int importId, string compareField) {
            Db = db;
            CompareField = compareField;
            CurrentId = currentId;
            ImportId = importId;
        }

        public void GetSummary() {
            Scanner scanner = new Scanner(Db, CurrentId, ImportId, CompareField);

            scanner.Changed = (current, import) => {
                float change = import.Retail - current.Retail;
                float percentageChange = change / current.Retail * 100;

                if (current.Retail < import.Retail) {
                    Up++;
                    if(percentageChange > MaxPercentageUp) {
                        MaxPercentageUp = percentageChange;
                    }
                }
                if (current.Retail > import.Retail) {
                    Down++;
                    if (percentageChange < MaxPercentageUp) {
                        MaxPercentageDown = percentageChange;
                    }
                }
                return false;
            };
            scanner.Unchanged = (current, import) => {
                Same++;
                return false;
            };

            scanner.Scan();
        }

        public void GetPercentageChangeBands() {
            GetSummary();

            float UpBand = MaxPercentageUp / 10;
            float DownBand = MaxPercentageDown / 10;
        }
        

    }
}
