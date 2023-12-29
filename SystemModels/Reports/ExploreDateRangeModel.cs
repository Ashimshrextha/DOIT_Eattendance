using System;

namespace SystemModels.Reports
{
    public class ExploreDateRangeModel
    {
        public Nullable<long> rowNumber { get; set; }
        public Nullable<System.DateTime> DateRange { get; set; }
        public Nullable<int> CountDW { get; set; }
        public string DOW { get; set; }
        public string NepaliDate { get; set; }
        public string DOWNEP { get; set; }
        public string DOWShortNP { get; set; }
        public string DOWShort { get; set; }
    }
}
