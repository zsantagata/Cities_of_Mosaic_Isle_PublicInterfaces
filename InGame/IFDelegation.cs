using Cities_of_Mosaic_Isle_PublicInterfaces.Helper;
using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFDelegation : IFUID, IFSpriteSheetOverrideable, IFNamedDebugObject
    {
        //for this SO class, establish consts and enums:
        public const int cDelegationTypeCount = 7; //max index + 1
        public enum eDelegationType //these are exclusive!
        {
             cNone = 0
            ,cWar = 1 //delegations for war are meant to kill pops, destroy buildings, and conquer if possible (communities can surrender to be conquered)
                      //  note to self: a war delegation sent by the master community will not accept surrender
            ,cRaiding = 2 //delegations for raiding are hostile like war delegations but are meant to steal pops and/or resources; they cannot conquer but can kill pops and destroy buildings.
                          //Unlike War delegations, these delegations can choose to offer an ultimatum to the target community.  If accepted, the raiders could still attack, but if rejected, the target community is better prepared for an attack due to the warning.
            ,cDiplomacy = 3 //delegations for diplomacy are meant to improve relations with a foreign community/some portion of citizenry
            ,cTrade = 4 //delegations for trade are meant to offer goods in trade for other goods.
                        //  note to self: trade delegations can ask for more goods than are being traded by expending goodwill with the target community
            ,cEmigration = 5 //delegations for emigration have been cast out of their home and are sent to a foreign community to join it (or become Wanderers if rejected).  AKA exile.
            ,cSettlement = 6 //delegations for settlement are meant to establish a new community in a vacant or razed location.
                             //No settlement delegation can be sent while other delegations are away from home.  No other delegation can be sent while a settlement delegation is away from home.
                             //The AI sends settlement delegations as well, but with strict conditions.
        }
        public static readonly string[] cDelegationTypeNames = //these are the translatedStrings names
        {
             "delegation_type_none_name"
            ,"delegation_type_war_name"
            ,"delegation_type_raiding_name"
            ,"delegation_type_diplomacy_name"
            ,"delegation_type_trade_name"
            ,"delegation_type_emigration_name"
            ,"delegation_type_settlement_name"
        };

        //delegations work like this: //TODO settlement delegations won't work like this lol
        //a delegation will be constructed from its home community.  Then, it will travel on the map to its target.
        //if it is a player delegation, it will travel on the map back home.  Then, the game will pause, and the player will be forced to resolve the delegation before time continues.
        //first, MTTH travel events on the path from home to the target are resolved, if any
        //then (depending on the type of delegation -- war and raiding are different), arrival event(s) and arrival base occurrence(s) are resolved
        //then, MTTH travel events on the path from the target to home are resolved, if any
        //then, a date is set upon which the delegation will return home (this is done because delegations may have been delayed by choices made, or wounds/illnesses)
        //right after the pops and resources return, a simultaneous-with-return event fires, if applicable
        //lastly, once the return date has passed and the delegation's pops and resources are returned to the community, the igDelegation just waits to be cleaned up by C# code

        //foreign delegations will be assembled and travel to their destination on their own
        //if the destination is a foreign community, scripts will handle the occurrence in the background and the player will see nothing
        //if the destination is the player community, the delegation will be placed on the local map and a simultaneous event fires, if applicable
        //a variable with a common name will indicate that pops in the delegation want to stay on the map (the simultaneous event will do this)
        //when there are no more alive pops from the delegation left on the map, the delegation travels home (if there are any alive pops left)
        //lastly, once the return date has passed and the delegation's pops and resources are returned to the community, the igDelegation just waits to be cleaned up by C# code
        public const int cDelegationStateCount = 9;
        public const int cDelegationNoDisplayAtOrAboveThisState = 5;
        public enum eDelegationState //exclusive
        {
             cTravelingOnMap = 0

                //player-sourced delegations:
            ,cEvaluatingGoingToTarget = 1
            ,cEvaluatingArrivalChanceEvent = 2 //this is an event that happens (and this specific event probably doesn't happen all the time), not the base occurrence that happens every time
            ,cEvaluatingArrivalBaseOccurrence = 3 //this is not an event that happens; it is the base occurrence that happens every time
            ,cEvaluatingComingBackHome = 4
            ,cFullyEvaluatedAndWaitingToReturn = 5
            ,cHasReturnedHome = 6 //delegations in this state are waiting to be cleaned up by C# code

                //foreign-sourced delegations:
            ,cForeignSourcedDelegationHangingOutOnPlayerMap = 7 //all delegations are always immediately dumped on the player map at the same time as the pre-event occurs
            ,cForeignSourcedDelegationTravelingHome = 8
        }

        public eDelegationState getState();
        //setState is not available to scripts
        public bool isTotalLoss(); //a delegation is totally lost if it has no alive, loyal pops in it

        public IFCommunity getSourceCommunity();
        public IFCommunity getTargetCommunity();
        public IFResourcePool getResourcePool();
        public ReadOnlyCollection<IFPop> getPopsInDelegation(); //this cannot be modified directly; instead use below function or the IFPop function setDelegation.  Also, remember that not all pops in a delegation are necessarily the loyal to the source community, if an event happened or the delegation took prisoners.
        public void modifyListOfPopsInDelegation(bool inRemove, IFPop inPop);

        public double getDiploMapLocationX();
        public double getDiploMapLocationY();
        public IFTerrainBiome getBiomeOfCurrentLocation();

        public eDelegationType getDelegationType();
        public IFCommunity.ePathTypes getPathType();
        public Int64 getLaunchDate();
        public double getExpectedArrivalDate();
        public double getExpectedReturnDate();
        public void addToReturnDate(double inDaysDelay); //note that, besides siege days gone by, nothing in the C# code calls this or stops a delegation from returning home at the expected time.  Therefore, if a modder/I wish to indicate a delegation has slowed down, the day-equivalent time loss must be fed into this function
        public IFCalendar.calendarDate getExpectedReturnDateAsCalendarDate();
        public Int64 getNextDateToCalcEventsFor(); //events and scripts of wound/ill/etc.  If this is = to or past return date, no more events and scripts should be calc'd
    }
}
