using System;
using System.Collections.Generic;

namespace FindingCommunicationRoutes
{
    public interface ICommunicationRoutesGui
    {
        event Delegates.LoadNewSchedule LoadNewScheduleFromFile;
        void DisplayBusStops(List<string> listOfBusStopsNames);
        void SetDateAndTime(DateTime dateTime);
        void UpdateInformationAndTimeForLoadingNewSchedule(string information, int valueOfProgressBar);
        object Invoke(Delegate method, params object[] args);
    }
}
