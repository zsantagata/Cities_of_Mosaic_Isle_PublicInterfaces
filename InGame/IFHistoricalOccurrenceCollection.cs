using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFHistoricalOccurrenceCollection
    {
        public void add(IFHistoricalOccurrence inToAdd);
        public ReadOnlyCollection<IFHistoricalOccurrence> getAllHistoryBetweenDates(Int64 inEarlierDate, Int64 inLaterDate);
        public void forgetOccurrencesBeforeDateWithConditions(Int64 inDate,
            bool inRememberOccurrencesWithNoUnknownActorsOrTargets = false,
            int inRememberOccurrencesWithEffectImpactStrongerThanThis = IFHistoryEffect.cMaxImpact, //this will use absolute value
            List<string> inRememberOccurrencesWithTheseActionTags = null,
            List<string> inRememberOccurrencesWithTheseEffectTags = null
            );
    }
}
