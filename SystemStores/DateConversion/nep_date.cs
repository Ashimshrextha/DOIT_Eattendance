namespace SystemStores.DateConversion
{
    public class nep_date
    {
        public string nepDate { get; set; }

        public int year { get; set; }

        public int month { get; set; }

        public int day { get; set; }

        public string weekDayInWord { get; set; }

        public string nmonth { get; set; }

        public int weekDayInNum { get; set; }

        public int totalDaysInMonth { get; set; }

        public string fiscalYear { get; set; }

        public int TaxableMonth { get; set; }

        public int totalDaysInEndOfFiscalYear { get; set; }

        public double effectiveMthInFiscalYear { get; set; }
    }
}
