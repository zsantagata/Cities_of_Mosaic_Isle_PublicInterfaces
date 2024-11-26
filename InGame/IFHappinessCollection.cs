using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFHappinessCollection
    {
        public double getRaceHappiness(IFRace inRace);
        public ReadOnlyCollection<IFHappinessCause> getAllCausesWithName(IFRace inRace, string inName);
        public ReadOnlyCollection<IFHappinessCause> getAllCauses(IFRace inRace);

        public void addNewHappinessCauseToday(IFRace inRace, string inCauseName, string inCauseDescription, double inStartValue, bool inPermanent);
        public void addNewHappinessCause(IFRace inRace, string inCauseName, string inCauseDescription, double inStartValue, bool inPermanent, Int64 inStartDate);
        public void removeHappinessCause(IFRace inRace, IFHappinessCause inCause);
    }
}
