using Cities_of_Mosaic_Isle_PublicInterfaces.Helper;
using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFEventStatus : IFUID, IFNamedDebugObject
    {
        //a passthrough tagList accessor; event scripts may want to check if the event has a tag, and that makes it much easier:
        public ReadOnlyCollection<string> getEventTagList();

        public bool getEnabled();
        public void setEnabled(bool inNowEnabled);

        public Int64 getActiveCount(); //active count should always be equal to the number of event reports of this event exist already
        //setActiveCount is controlled internally

        public Int64 getLastOccurrenceDay();
        public bool hasOccurredBefore();
        //setLastOccurrenceDay is controlled internally

        public Int64 getMTTH();
        public void setMTTH(Int64 inNewMTTH);
        public bool hasValidMTTH();

        public double getWeight();
        public void setWeight(double inWeight);
        public bool hasValidWeight();

        public Int64 getForceDate();
        public bool hasValidForceDate();
        public bool isUseForceDateInstead(); //"instead" of MTTH
        public void setForceDate(Int64 inForceDate);
    }
}
