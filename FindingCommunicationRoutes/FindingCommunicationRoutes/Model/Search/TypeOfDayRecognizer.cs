using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public class TypeOfDayRecognizer
    {
        #region Constructors

        public TypeOfDayRecognizer()
        {
        }

        #endregion

        #region Public methods

        public List<string> RecognizeTypeOfDay(DateTime date)
        {
            List<string> result = new List<string>();

            int day = date.Day;
            int month = date.Month;
            int year = date.Year;
            DayOfWeek dayOfWeek = date.DayOfWeek;

            if ((day == 1 && month == 1) || (day == 20 && month == 4) || (day == 25 && month == 12))
            {
                result.Add("1 I, 25 XII, Niedziela Wielkanocna");

            }
            else if (day == 1 && month == 11)
            {
                result.Add("Wszystkich ĹšwiÄ™tych");
            }
            else if (day == 24 && month == 12)
            {
                result.Add("Wigilia BoĹĽego Narodzenia");
            }
            else if (day == 31 && month == 12)
            {
                result.Add("Sylwester");
            }


            bool isFreeDayInMall = ((day == 1 && month == 1) || (day == 6 && month == 1) ||(day == 20 && month == 4) 
                || (day == 21 && month == 4)|| (day == 1 && month == 5) || (day == 3 && month == 5)
                || (day == 8 && month == 6) || (day == 19 && month == 6) || (day == 15 && month == 8)
                || (day == 1 && month == 11) || (day == 11 && month == 11) || (day == 25 && month == 12) 
                || (day == 26 && month == 12));

            if (isFreeDayInMall && dayOfWeek != DayOfWeek.Sunday)
            {
                result.Add("Dni wolne od pracy w centrach handlowych");
                result.Add("Niedziele i ĹšwiÄ™ta");
                result.Add("Dni Wolne");
            }
            else if (isFreeDayInMall && dayOfWeek == DayOfWeek.Sunday)
            {
                result.Add("Dni wolne od pracy w centrach handlowych");
            }


            bool isHoliday = (month == 7) || (month == 8) || ((day >= 28) && (month == 6));
            if ((dayOfWeek == DayOfWeek.Sunday) && (isHoliday))
            {
                result.Add("Niedziele i ĹšwiÄ™ta w wakacje");
                
            }
            else if ((dayOfWeek == DayOfWeek.Sunday) && (!isHoliday) && (!isFreeDayInMall))
            {
                result.Add("Niedziela poza wakacjami i dniami wolnymi od pracy w centrach handlowych");
            }

            if (dayOfWeek == DayOfWeek.Sunday)
            {
                result.Add("Niedziele i ĹšwiÄ™ta");
                result.Add("Dni Wolne");
            }


            if ((dayOfWeek == DayOfWeek.Saturday) && (isHoliday))
            {
                result.Add("Soboty w wakacje");
            }
            else if ((dayOfWeek == DayOfWeek.Saturday) && (!isHoliday))
            {
                result.Add("Soboty poza wakacjami");
            }

            if (dayOfWeek == DayOfWeek.Saturday)
            {
                result.Add("Sobota");
                result.Add("Dni Wolne");
            }

            bool isWorkingDay = dayOfWeek == DayOfWeek.Monday || dayOfWeek == DayOfWeek.Tuesday || dayOfWeek == DayOfWeek.Wednesday || dayOfWeek == DayOfWeek.Thursday || dayOfWeek == DayOfWeek.Friday;
            bool isWinterHoliday = (day >= 20 && month == 1) || (day <= 2 && month == 2);
            bool isBreakInSchool = (day >= 17 && day <= 22 && month == 4) || (day >= 23 && month == 12);
            if (isWorkingDay && !isHoliday && !isBreakInSchool && !isFreeDayInMall)
            {
                result.Add("Robocze szkolne i w ferie");
            }
            else if (isWorkingDay && isHoliday && !isFreeDayInMall)
            {
                result.Add("Dni robocze w wakacje");
            }

            if (isWorkingDay && !isHoliday && !isWinterHoliday && !isBreakInSchool && !isFreeDayInMall)
            {
                result.Add("Dni Robocze Szkolne");
            }
            else if (isWorkingDay && (isHoliday || isWinterHoliday) && !isFreeDayInMall)
            {
                result.Add("Dni Robocze w Ferie i Wakacje");
            }

            if (isWorkingDay && !isFreeDayInMall)
            {
                result.Add("Robocze");
            }

            return result;
        }

        #endregion
    }
}
