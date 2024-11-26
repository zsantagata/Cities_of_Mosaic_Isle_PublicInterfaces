namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFHistoryActor
    {
        public enum eActorKind
        {
             cWorld = 0
            ,cPop = 1
            ,cCommunity = 2
            ,cRace = 3
            ,cPlayer = 4
        }
        public eActorKind getActorKind();
        public string getActorDescription();

        public UInt64 getActorID(); //if actor kind is pop/community, this is UID.  if actor kind is race, this is MOID.  Otherwise this is 0.
    }
}
