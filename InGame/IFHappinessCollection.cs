using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFHappinessCollection
    {
        public double getRealRaceHappiness(IFRace inRace);
        public double getDisplayedRaceHappiness(IFRace inRace); //the difference between this and the above is that this does not include "hidden" happiness causes
        public ReadOnlyCollection<IFHappinessCause> getAllCausesWithName(IFRace inRace, string inName);
        public ReadOnlyCollection<IFHappinessCause> getAllCauses(IFRace inRace);
        public ReadOnlyCollection<IFHappinessCause> getDisplayedCausesWithName(IFRace inRace, string inName); //the difference between this and the above is that this does not include "hidden" happiness causes
        public ReadOnlyCollection<IFHappinessCause> getDisplayedCauses(IFRace inRace); //the difference between this and the above is that this does not include "hidden" happiness causes

        public void addNewHappinessCauseToday(IFRace inRace, string inCauseName, string inCauseDescription, double inStartValue, bool inPermanent, bool inHiddenFromPlayer = false);
        public void addNewHappinessCause(IFRace inRace, string inCauseName, string inCauseDescription, double inStartValue, bool inPermanent, Int64 inStartDate, bool inHiddenFromPlayer = false);
        public void removeHappinessCause(IFRace inRace, IFHappinessCause inCause);
    }
}
