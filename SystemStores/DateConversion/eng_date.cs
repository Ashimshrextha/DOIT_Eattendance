using System;

namespace SystemStores.DateConversion
{
    public class eng_date
    {
        public int year { get; set; }

        public int month { get; set; }

        public int day { get; set; }

        public string weekDayInWord { get; set; }

        public string emonth { get; set; }

        public int weekDayInNum { get; set; }

        public int totalDaysInMonth { get; set; }

        public DateTime engDate { get; set; }
    }
}
