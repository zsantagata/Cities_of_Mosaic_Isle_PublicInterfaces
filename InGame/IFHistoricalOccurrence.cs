namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFHistoricalOccurrence
    {
        //a historical occurrence is a self-contained thing that happened on a single date some time in the past.
        //an ACTOR took ACTION upon TARGET, causing EFFECT -- this is how all historical occurrences are organized.

        public Int64 getCalendarDate();

        public IFHistoryActor getActor();
        public IFHistoryAction getAction();
        public IFHistoryTarget getTarget();
        public IFHistoryEffect getEffect();

        public string getText(); //this is a line of text connecting ACTOR, ACTION, TARGET, EFFECT, and date.
        
        //this function checks Actor/Target IDs with savegame to check if any of the historical figures have been cleaned up by the savegame
        public bool isAnythingForgotten();
    }
}
