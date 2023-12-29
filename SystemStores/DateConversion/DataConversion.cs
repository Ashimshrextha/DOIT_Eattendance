using System;

namespace SystemStores.DateConversion
{
    public class DataConversion
    {
        private static int[,] bs = new int[91, 13]
    {
      {
        2000,
        30,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        30,
        29,
        31
      },
      {
        2001,
        31,
        31,
        32,
        31,
        31,
        31,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2002,
        31,
        31,
        32,
        32,
        31,
        30,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2003,
        31,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        29,
        30,
        31
      },
      {
        2004,
        30,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        30,
        29,
        31
      },
      {
        2005,
        31,
        31,
        32,
        31,
        31,
        31,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2006,
        31,
        31,
        32,
        32,
        31,
        30,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2007,
        31,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        29,
        30,
        31
      },
      {
        2008,
        31,
        31,
        31,
        32,
        31,
        31,
        29,
        30,
        30,
        29,
        29,
        31
      },
      {
        2009,
        31,
        31,
        32,
        31,
        31,
        31,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2010,
        31,
        31,
        32,
        32,
        31,
        30,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2011,
        31,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        29,
        30,
        31
      },
      {
        2012,
        31,
        31,
        31,
        32,
        31,
        31,
        29,
        30,
        30,
        29,
        30,
        30
      },
      {
        2013,
        31,
        31,
        32,
        31,
        31,
        31,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2014,
        31,
        31,
        32,
        32,
        31,
        30,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2015,
        31,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        29,
        30,
        31
      },
      {
        2016,
        31,
        31,
        31,
        32,
        31,
        31,
        29,
        30,
        30,
        29,
        30,
        30
      },
      {
        2017,
        31,
        31,
        32,
        31,
        31,
        31,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2018,
        31,
        32,
        31,
        32,
        31,
        30,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2019,
        31,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        30,
        29,
        31
      },
      {
        2020,
        31,
        31,
        31,
        32,
        31,
        31,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2021,
        31,
        31,
        32,
        31,
        31,
        31,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2022,
        31,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        29,
        30,
        30
      },
      {
        2023,
        31,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        30,
        29,
        31
      },
      {
        2024,
        31,
        31,
        31,
        32,
        31,
        31,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2025,
        31,
        31,
        32,
        31,
        31,
        31,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2026,
        31,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        29,
        30,
        31
      },
      {
        2027,
        30,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        30,
        29,
        31
      },
      {
        2028,
        31,
        31,
        32,
        31,
        31,
        31,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2029,
        31,
        31,
        32,
        31,
        32,
        30,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2030,
        31,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        29,
        30,
        31
      },
      {
        2031,
        30,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        30,
        29,
        31
      },
      {
        2032,
        31,
        31,
        32,
        31,
        31,
        31,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2033,
        31,
        31,
        32,
        32,
        31,
        30,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2034,
        31,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        29,
        30,
        31
      },
      {
        2035,
        30,
        32,
        31,
        32,
        31,
        31,
        29,
        30,
        30,
        29,
        29,
        31
      },
      {
        2036,
        31,
        31,
        32,
        31,
        31,
        31,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2037,
        31,
        31,
        32,
        32,
        31,
        30,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2038,
        31,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        29,
        30,
        31
      },
      {
        2039,
        31,
        31,
        31,
        32,
        31,
        31,
        29,
        30,
        30,
        29,
        30,
        30
      },
      {
        2040,
        31,
        31,
        32,
        31,
        31,
        31,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2041,
        31,
        31,
        32,
        32,
        31,
        30,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2042,
        31,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        29,
        30,
        31
      },
      {
        2043,
        31,
        31,
        31,
        32,
        31,
        31,
        29,
        30,
        30,
        29,
        30,
        30
      },
      {
        2044,
        31,
        31,
        32,
        31,
        31,
        31,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2045,
        31,
        32,
        31,
        32,
        31,
        30,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2046,
        31,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        29,
        30,
        31
      },
      {
        2047,
        31,
        31,
        31,
        32,
        31,
        31,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2048,
        31,
        31,
        32,
        31,
        31,
        31,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2049,
        31,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        29,
        30,
        30
      },
      {
        2050,
        31,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        30,
        29,
        31
      },
      {
        2051,
        31,
        31,
        31,
        32,
        31,
        31,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2052,
        31,
        31,
        32,
        31,
        31,
        31,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2053,
        31,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        29,
        30,
        30
      },
      {
        2054,
        31,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        30,
        29,
        31
      },
      {
        2055,
        31,
        31,
        32,
        31,
        31,
        31,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2056,
        31,
        31,
        32,
        31,
        32,
        30,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2057,
        31,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        29,
        30,
        31
      },
      {
        2058,
        30,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        30,
        29,
        31
      },
      {
        2059,
        31,
        31,
        32,
        31,
        31,
        31,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2060,
        31,
        31,
        32,
        32,
        31,
        30,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2061,
        31,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        29,
        30,
        31
      },
      {
        2062,
        30,
        32,
        31,
        32,
        31,
        31,
        29,
        30,
        29,
        30,
        29,
        31
      },
      {
        2063,
        31,
        31,
        32,
        31,
        31,
        31,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2064,
        31,
        31,
        32,
        32,
        31,
        30,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2065,
        31,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        29,
        30,
        31
      },
      {
        2066,
        31,
        31,
        31,
        32,
        31,
        31,
        29,
        30,
        30,
        29,
        29,
        31
      },
      {
        2067,
        31,
        31,
        32,
        31,
        31,
        31,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2068,
        31,
        31,
        32,
        32,
        31,
        30,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2069,
        31,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        29,
        30,
        31
      },
      {
        2070,
        31,
        31,
        31,
        32,
        31,
        31,
        29,
        30,
        30,
        29,
        30,
        30
      },
      {
        2071,
        31,
        31,
        32,
        31,
        31,
        31,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2072,
        31,
        32,
        31,
        32,
        31,
        30,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2073,
        31,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        29,
        30,
        31
      },
      {
        2074,
        31,
        31,
        31,
        32,
        31,
        31,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2075,
        31,
        31,
        32,
        31,
        31,
        31,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2076,
        31,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        29,
        30,
        30
      },
      {
        2077,
        31,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        30,
        29,
        31
      },
      {
        2078,
        31,
        31,
        31,
        32,
        31,
        31,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2079,
        31,
        31,
        32,
        31,
        31,
        31,
        30,
        29,
        30,
        29,
        30,
        30
      },
      {
        2080,
        31,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        29,
        30,
        30
      },
      {
        2081,
        31,
        31,
        32,
        32,
        31,
        30,
        30,
        30,
        29,
        30,
        30,
        30
      },
      {
        2082,
        30,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        30,
        30,
        30
      },
      {
        2083,
        31,
        31,
        32,
        31,
        31,
        30,
        30,
        30,
        29,
        30,
        30,
        30
      },
      {
        2084,
        31,
        31,
        32,
        31,
        31,
        30,
        30,
        30,
        29,
        30,
        30,
        30
      },
      {
        2085,
        31,
        32,
        31,
        32,
        30,
        31,
        30,
        30,
        29,
        30,
        30,
        30
      },
      {
        2086,
        30,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        30,
        30,
        30
      },
      {
        2087,
        31,
        31,
        32,
        31,
        31,
        31,
        30,
        30,
        29,
        30,
        30,
        30
      },
      {
        2088,
        30,
        31,
        32,
        32,
        30,
        31,
        30,
        30,
        29,
        30,
        30,
        30
      },
      {
        2089,
        30,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        30,
        30,
        30
      },
      {
        2090,
        30,
        32,
        31,
        32,
        31,
        30,
        30,
        30,
        29,
        30,
        30,
        30
      }
    };
        public static string debug_info = string.Empty;

        public static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(timestamp);
        }

        public static bool IsLeapYear(int year)
        {
            if (year % 100 == 0)
                return year % 400 == 0;
            return year % 4 == 0;
        }

        private static string GetNepaliMonth(int month)
        {
            string str = string.Empty;
            switch (month)
            {
                case 1:
                    str = "Baishak";
                    break;
                case 2:
                    str = "Jestha";
                    break;
                case 3:
                    str = "Ashad";
                    break;
                case 4:
                    str = "Shrawn";
                    break;
                case 5:
                    str = "Bhadra";
                    break;
                case 6:
                    str = "Ashwin";
                    break;
                case 7:
                    str = "kartik";
                    break;
                case 8:
                    str = "Mangshir";
                    break;
                case 9:
                    str = "Poush";
                    break;
                case 10:
                    str = "Magh";
                    break;
                case 11:
                    str = "Falgun";
                    break;
                case 12:
                    str = "Chaitra";
                    break;
            }
            return str;
        }

        private static string GetEnglishMonth(int month)
        {
            string str = string.Empty;
            switch (month)
            {
                case 1:
                    str = "January";
                    break;
                case 2:
                    str = "February";
                    break;
                case 3:
                    str = "March";
                    break;
                case 4:
                    str = "April";
                    break;
                case 5:
                    str = "May";
                    break;
                case 6:
                    str = "June";
                    break;
                case 7:
                    str = "July";
                    break;
                case 8:
                    str = "August";
                    break;
                case 9:
                    str = "September";
                    break;
                case 10:
                    str = "October";
                    break;
                case 11:
                    str = "November";
                    break;
                case 12:
                    str = "December";
                    break;
            }
            return str;
        }

        private static string GetDayOfWeek(int d)
        {
            string str = string.Empty;
            switch (d)
            {
                case 1:
                    str = "Sunday";
                    break;
                case 2:
                    str = "Monday";
                    break;
                case 3:
                    str = "Tuesday";
                    break;
                case 4:
                    str = "Wednesday";
                    break;
                case 5:
                    str = "Thursday";
                    break;
                case 6:
                    str = "Friday";
                    break;
                case 7:
                    str = "Saturday";
                    break;
            }
            return str;
        }

        private static bool IsRangeEnglish(int yy, int mm, int dd)
        {
            if (yy < 1943 || yy > 2034)
            {
                DataConversion.debug_info = "Supported only between 1944-2034";
                return false;
            }
            if (mm >= 1 && mm <= 12)
                return true;
            DataConversion.debug_info = "Error! value 1-12 only";
            return false;
        }

        public static bool IsRangeNepali(int yy, int mm, int dd)
        {
            if (yy < 2000 || yy > 2090)
            {
                DataConversion.debug_info = "Supported only between 2000-2089";
                return false;
            }
            if (mm < 1 || mm > 12)
            {
                DataConversion.debug_info = "Error! value 1-12 only";
                return false;
            }
            if (dd >= 1 && dd <= DataConversion.bs[yy - 2000, mm])
                return true;
            DataConversion.debug_info = "Error! value 1-" + (object)DataConversion.bs[yy - 2000, mm + 1] + " only";
            return false;
        }

        public static nep_date EnglishToNepali(int yy, int mm, int dd)
        {
            if (!DataConversion.IsRangeEnglish(yy, mm, dd))
                return (nep_date)null;
            int[] numArray1 = new int[12]
            {
        31,
        28,
        31,
        30,
        31,
        30,
        31,
        31,
        30,
        31,
        30,
        31
            };
            int[] numArray2 = new int[12]
            {
        31,
        29,
        31,
        30,
        31,
        30,
        31,
        31,
        30,
        31,
        30,
        31
            };
            int num1 = 1944;
            int num2 = 2000;
            int num3 = 9;
            int num4 = 16;
            int num5 = 0;
            int d = 6;
            for (int index1 = 0; index1 < yy - num1; ++index1)
            {
                if (DataConversion.IsLeapYear(num1 + index1))
                {
                    for (int index2 = 0; index2 < 12; ++index2)
                        num5 += numArray2[index2];
                }
                else
                {
                    for (int index2 = 0; index2 < 12; ++index2)
                        num5 += numArray1[index2];
                }
            }
            for (int index = 0; index < mm - 1; ++index)
            {
                if (DataConversion.IsLeapYear(yy))
                    num5 += numArray2[index];
                else
                    num5 += numArray1[index];
            }
            int num6 = num5 + dd;
            int index3 = 0;
            int index4 = num3;
            int num7 = num4;
            int index5 = num3;
            int year = num2;
            for (; num6 != 0; --num6)
            {
                int b = DataConversion.bs[index3, index4];
                ++num7;
                ++d;
                if (num7 > b)
                {
                    ++index5;
                    num7 = 1;
                    ++index4;
                }
                if (d > 7)
                    d = 1;
                if (index5 > 12)
                {
                    ++year;
                    index5 = 1;
                }
                if (index4 > 12)
                {
                    index4 = 1;
                    ++index3;
                }
            }
            int num8 = d;
            int num9 = 0;
            string nepaliFiscalYear = DataConversion.get_Nepali_FiscalYear(year, index5);
            int num10 = int.Parse(nepaliFiscalYear.Split('/')[1]);
            int num11 = 0;
            for (int index1 = 0; index1 <= DataConversion.bs.Length; ++index1)
            {
                if (DataConversion.bs[index1, 0] == num10)
                {
                    if (index5 > 3)
                    {
                        num9 = DataConversion.bs[index1 - 1, index5];
                        num11 = DataConversion.bs[index1, 3];
                        break;
                    }
                    num9 = DataConversion.bs[index1, index5];
                    num11 = DataConversion.bs[index1, 3];
                    break;
                }
            }
            return new nep_date()
            {
                nepDate = year.ToString() + "-" + index5.ToString().PadLeft(2, '0') + "-" + num7.ToString().PadLeft(2, '0'),
                year = year,
                month = index5,
                day = num7,
                weekDayInWord = DataConversion.GetDayOfWeek(d),
                nmonth = DataConversion.GetNepaliMonth(index5),
                weekDayInNum = num8,
                totalDaysInMonth = num9,
                fiscalYear = nepaliFiscalYear,
                TaxableMonth = DataConversion.get_TaxableMonth(index5),
                totalDaysInEndOfFiscalYear = num11,
                effectiveMthInFiscalYear = (double)num7 * 1.0 / (double)num9 + (double)(12 - DataConversion.get_TaxableMonth(index5))
            };
        }

        public static int getTaxableMonthJulyToJune(DateTime date)
        {
            if (date.Month >= 7 && date.Month <= 12)
                return date.Month - 6;
            return date.Month + 6;
        }

        public static eng_date NepaliToEnglish(int yy, int mm, int dd)
        {
            int num1 = 1943;
            int num2 = 4;
            int num3 = 13;
            int num4 = 2000;
            int num5 = 0;
            int d = 3;
            int index1 = 0;
            int[] numArray1 = new int[13]
            {
        0,
        31,
        28,
        31,
        30,
        31,
        30,
        31,
        31,
        30,
        31,
        30,
        31
            };
            int[] numArray2 = new int[13]
            {
        0,
        31,
        29,
        31,
        30,
        31,
        30,
        31,
        31,
        30,
        31,
        30,
        31
            };
            if (!DataConversion.IsRangeNepali(yy, mm, dd))
                return (eng_date)null;
            for (int index2 = 0; index2 < yy - num4; ++index2)
            {
                for (int index3 = 1; index3 <= 12; ++index3)
                    num5 += DataConversion.bs[index1, index3];
                ++index1;
            }
            for (int index2 = 1; index2 < mm; ++index2)
                num5 += DataConversion.bs[index1, index2];
            int num6 = num5 + dd;
            int num7 = num3;
            int month = num2;
            int year = num1;
            for (; num6 != 0; --num6)
            {
                int num8 = !DataConversion.IsLeapYear(year) ? numArray1[month] : numArray2[month];
                ++num7;
                ++d;
                if (num7 > num8)
                {
                    ++month;
                    num7 = 1;
                    if (month > 12)
                    {
                        ++year;
                        month = 1;
                    }
                }
                if (d > 7)
                    d = 1;
            }
            int num9 = d;
            return new eng_date()
            {
                year = year,
                month = month,
                day = num7,
                weekDayInWord = DataConversion.GetDayOfWeek(d),
                emonth = DataConversion.GetEnglishMonth(month),
                weekDayInNum = num9,
                totalDaysInMonth = DateTime.Parse(month.ToString() + "/" + (object)num7 + "/" + (object)year).Day,
                engDate = DateTime.Parse(month.ToString() + "/" + (object)num7 + "/" + (object)year)
            };
        }

        private static string get_Nepali_FiscalYear(int year, int month)
        {
            if (month >= 1 && month <= 3)
                return (year - 1).ToString() + "/" + (object)year;
            return year.ToString() + "/" + (object)(year + 1);
        }

        public static int get_TaxableMonth(int normalMonth)
        {
            int num = normalMonth - 3;
            if (num <= 0)
                num += 12;
            return num;
        }

        public static int get_YearForGivenNpMth(string fiscalYear, int normalMonth)
        {
            string[] strArray = fiscalYear.Split('/');
            return normalMonth <= 3 || normalMonth > 12 ? int.Parse(strArray[1]) : int.Parse(strArray[0]);
        }

        public static eng_date GetFirstEngDateInAGivenNpMth(string fiscalYear, int month)
        {
            string[] strArray = fiscalYear.Split('/');
            return DataConversion.NepaliToEnglish(month <= 3 || month > 12 ? int.Parse(strArray[1]) : int.Parse(strArray[0]), month, 1);
        }

        public static int GetTotalDaysInAGivenNpMth(string fiscalYear, int month)
        {
            int num = 0;
            int yearForGivenNpMth = DataConversion.get_YearForGivenNpMth(fiscalYear, month);
            for (int index = 0; index <= DataConversion.bs.Length; ++index)
            {
                if (DataConversion.bs[index, 0] == yearForGivenNpMth)
                {
                    num = DataConversion.bs[index, month];
                    break;
                }
            }
            return num;
        }
    }
}
