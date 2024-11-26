namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFHistoryTarget
    {
        public enum eTargetKind
        {
             cWorld = 0
            ,cPop = 1
            ,cCommunity = 2
            ,cRace = 3
            ,cPlayer = 4
            ,cDelegation = 5
            ,cBuilding = 6
            ,cResource = 7
            ,cMap = 8
        }
        public eTargetKind getTargetKind();
        public string getTargetDescription();

        public UInt64 getTargetID(); //if target kind is pop/community/building/map, this is UID.  if target kind is race/resource, this is MOID.  if target kind is delegation, this UID is the community UID.  Otherwise this is 0.
    }
}
