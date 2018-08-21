using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Util
{
    public class BandChanges{
        public BandChanges(float max){

        }
        public void Add(float value){
            
        }
    }
    public class RetailChangeSummary
    {
        public AppDb Db { get; private set; }
        public string CompareField { get; private set; }
        public int CurrentId { get; private set; }
        public int ImportId { get; private set; }

        private Scanner scanner { get; set; }

        public int Down { get; private set; }
        public float MaxPercentageDown { get; private set; }
        public int Same { get; private set; }
        public int Up { get; private set; }
        public float MaxPercentageUp { get; private set; }

        public BandChanges UpChanges { get; private set; }
        public BandChanges DownChanges { get; private set; }
        public RetailChangeSummary(AppDb db, int currentId, int importId, string compareField)
        {
            Db = db;
            CompareField = compareField;
            CurrentId = currentId;
            ImportId = importId;
        }

        public void Scan()
        {
            Down = 0;
            Same = 0;
            Up = 0;

            scanner = new Scanner(Db, CurrentId, ImportId, CompareField);

            scanner.Changed = (current, import) =>
            {
                float change = import.Retail - current.Retail;
                float percentageChange = change / current.Retail * 100;

                if (current.Retail < import.Retail)
                {
                    Up++;
                    if (percentageChange > MaxPercentageUp)
                    {
                        MaxPercentageUp = percentageChange;
                    }
                }
                if (current.Retail > import.Retail)
                {
                    Down++;
                    if (percentageChange < MaxPercentageUp)
                    {
                        MaxPercentageDown = percentageChange;
                    }
                }
                return true;
            };
            scanner.Unchanged = (current, import) =>
            {
                Same++;
                return true;
            };

            scanner.Scan();
        }

        public void GetPercentageChangeBands()
        {
            Scan();

            UpChanges = new BandChanges(MaxPercentageUp);
            DownChanges = new BandChanges(MaxPercentageDown);
            //scanner.Scan();
        }
    }
}
