using System;
using System.Collections.Generic;

namespace FindingCommunicationRoutes
{
    public interface ICommunicationRoutesGui
    {
        event EventHandler LoadNewScheduleFromFile;
        event EventHandler<SoughtConnection> SearchRoute;
        void DisplayBusStops(List<string> listOfBusStopsNames);
        void SetDateAndTime(DateTime dateTime);
        void UpdateInformationAndTimeForProgressBar(string information, int valueOfProgressBar);
        object Invoke(Delegate method, params object[] args);
        bool CheckIfInvokeRequired();
        void SaveThread(System.Threading.Thread threadName);
        void ShowMessage(string message);
        void ShowResultsOfSearching(List<SearchResultConnection> results);
    }
}
