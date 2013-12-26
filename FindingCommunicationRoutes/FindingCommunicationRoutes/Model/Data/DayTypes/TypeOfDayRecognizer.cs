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

        public string RecognizeTypeOfDay(DateTime day)
        {
            string result = "";

            DayOfWeek dayOfWeek = day.DayOfWeek;

            return result;
        }

        #endregion
    }
}
