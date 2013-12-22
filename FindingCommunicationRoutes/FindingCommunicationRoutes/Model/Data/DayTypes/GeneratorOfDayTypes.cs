using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes.Data
{
    public class GeneratorOfDayTypes
    {
        #region Public methods

        public List<string> GenerateListOfDayTypes()
        {
            List<string> listOfDayTypes = new List<string>();
            foreach (DayTypes.TypeOfDay enumTypeOfDay in Enum.GetValues(typeof(DayTypes.TypeOfDay)))
            {
                listOfDayTypes.Add(EnumDescription.GetEnumDescription(enumTypeOfDay));
            }

            return listOfDayTypes;
        }

        #endregion
    }
}
