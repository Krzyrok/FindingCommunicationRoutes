using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes.Data
{
    public class DayTypes
    {
        public enum TypeOfDay
        {
            Weekdays,
            Saturday,
            [Description("Sunday and holidays")]
            SundayAndHolidays,
            [Description("Change to/from summer time")]
            ChangeSummerTime,
            [Description("New Year’s Eve")]
            NewYearsEve,
            [Description("Christmas Eve")]
            ChristmasEve,
            [Description("Al. Souls’ Day (November 1)")]
            SoulsDay
        }
    }
}
