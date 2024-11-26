using Cities_of_Mosaic_Isle_PublicInterfaces.Helper;
using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFCommunity : IFUID, IFSpriteSheetOverrideable, IFNamedDebugObject
    {
        //for this SO class, establish consts and enums:
        //besides "normal" there are four hard-coded community types used for events
        //there are also two statuses for semi-normal communities: invisible and abstract
        //invisible communities will not be displayed to the player, nor will they send out any delegations.  TODO Calculations for them (C# and scripts)?  Communities are made, or start as, invisible because the time when they exist and the time when some script/event wants the community to be active are not the same times.
        //abstract communities will be displayed to the player, but no information about them is available, and they will not send out delegations.  TODO Calculations for them (C# and scripts)?
        public enum eCommunityType
        {
             cNormal = 0
            ,cWanderers = 1
            ,cTheLady = 2
            ,cGenericEnemy = 3
            ,cRebels = 4
            ,cInvisible = 5
            ,cAbstract = 6
        }

        //NOTE TO SELF: if a condition is calculated in other ways, it should not be one of eForeignCommunityStatus
        public enum eForeignCommunityStatus //flags
        {
             cNone = 0x0
            ,cUntouchedSettlement = 0x1 //this flag is true if the community has never been settled before and never held any pops before //TODO what is this really used for? //TODO in almost all things, this should act as a "this isn't really a community but just a spot where the player can settle to start a community"
        }

        public const int cPathTypesMask = 0x7;
        [Flags]
        public enum ePathTypes //these are relative to the player community and TODO will be recalculated when the player changes communities
        {
             cNoPath = 0 //communities with no path between them cannot send each other delegations
            ,cWaterOnlyAvailable = 0x1 //if a water-only path is not available, pops who cannot breathe overwater cannot be sent on delegations to this community
            ,cLandOnlyAvailable = 0x2  //if a land-only path is not available, pops who cannot breathe underwater cannot be sent on delegations to this community
            ,cMixedAvailable = 0x4     //if only a mixed land/water path is available, only pops who can breathe both overwater and underwater can be sent on delegations to this community
        }

        public void changeDisplayNameTo(string inNewDisplayName);

        public IFCommunity.eCommunityType getCommunityType();
        public bool isNormalCommunity();
        public bool isWanderers();
        public bool isTheLady();
        public bool isGenericEnemy();
        public bool isRebels();
        public bool isInvisible();
        public bool isAbstractCommunity();

        public Int64 getDiploMapLocationX();
        public Int64 getDiploMapLocationY();
        public Tuple<Int64, Int64> getDiploMapLocation();
        public void setDiploMapLocation(Int64 inNewX, Int64 inNewY);

        public IFTerrainBiome getLandBiome();
        public bool hasLandBiome();
        public void setLandBiome(IFTerrainBiome inLandBiome); //note that this will not change anything for communities with domestic properties; their maps persist and therefore the biome has been permanently set
        public IFTerrainBiome getWaterBiome();
        public bool hasWaterBiome();
        public void setWaterBiome(IFTerrainBiome inWaterBiome); //note that this will not change anything for communities with domestic properties; their maps persist and therefore the biome has been permanently set
        public double getWaterRatio(); //if there is a domestic component, this fetches the map's water ratio.  Otherwise, foreign component holds this directly.  If neither exist (for communities like Wanderers), this returns 0.5d.
        public void setWaterRatio(double inWaterRatio);

        public IFCommunity getMasterCommunity();
        public bool hasMasterCommunity();
        public ReadOnlyCollection<IFCommunity> getServantCommunities();
        public bool hasServantCommunities();
        //note that calling any of the below functions will also set the internal member variables of the related community correctly
        public void setMasterCommunity(IFCommunity inNewMaster);
        public void addServantCommunity(IFCommunity inNewServant);
        public void removeServantCommunity(IFCommunity inNoLongerServant);

        public IFResourcePool getResourcePool(); //whether foreign or domestic, all communities (besides Wanderers/Generic Enemies/The Lady/Rebels) have a resource pool
        public IFHistoricalOccurrenceCollection getHistory();
        public IFLeaderCollection getLeaderCollection();

        public ReadOnlyCollection<IFDelegation> getDelegations();
        //add delegation is handled internally (when scripts create a delegation it is automatically added to the parent community's list)
        //remove delegation is handled internally

        public ReadOnlyCollection<IFPop> getPopsLoyalToCommunity(); //this function, unlike all the below functions, includes dead pops that are loyal to this community
        public ReadOnlyCollection<IFPop> getPopsLoyalToCommunity(bool inOnlyAlive, bool inOnlyDead);
        public UInt64 getPopCountOfRace(IFRace inRace); //this function gets the total count of pops of a race in the community, including the pops found in IFCommunity.getPopsLoyalToCommunity() and the extra pops in the IFForeignCommunityComponent if it exists.  alive only.
        public UInt64 getPopCountOfRace(UInt64 inRaceMOID); //same as above
        public ReadOnlyDictionary<IFRace, ReadOnlyCollection<IFPop>> getLoyalPopsByRace();
        public ReadOnlyDictionary<IFRace, ReadOnlyCollection<IFPop>> getLoyalPopsByRace(bool inOnlyAlive, bool inOnlyDead);
        public IFRace getMostPopulousRace(); //alive only

        //a community only has a domestic component if it is currently being played, or was played once before
        public bool hasDomesticComponent();
        public IFDomesticCommunityComponent getDomesticComponent();

        //all communities have a "foreign component".  These functions will not change the community state if called on the community currently being played.

        //this is the count of pops in the community with these races, *not including* the pops found in IFCommunity.getPopsLoyalToCommunity().  Therefore it is the count of "undeclared" pops left over
        //use the dictionary directly to modify counts
        //note that the player community will always return an empty dictionary, and changes to this dictionary will not remain
        public Dictionary<IFRace, UInt64> getAdditionalUndeclaredPopCounts();

        public Int64 getForeignCommunityStatus();
        public void setForeignCommunityStatus(Int64 inStatus);
        public bool isForeignCommunityUntouchedSettlement();
        public bool isForeignCommunityEmpty(); //this returns true if there are no alive, loyal pops for this community

        //player communities have null as their current AI, but have mil strength and econ strength calculations that are specific to player communities.
        public IFMilStrengthCalculation getMilStrengthCalculation();
        public void setMilStrengthCalculation(IFMilStrengthCalculation inMilStrengthCalculation);
        public double getMilStrength();
        public void setMilStrength(double inMilStrength);
        public IFEconStrengthCalculation getEconStrengthCalculation();
        public void setEconStrengthCalculation(IFEconStrengthCalculation inEconStrengthCalculation);
        public double getEconStrength();
        public void setEconStrength(double inEconStrength);
        public string getOpinion(); //player community will return empty string
        public void setOpinion(string inOpinion);
        public bool hasCurrentAI(); //player community will return false
        public IFForeignAI getCurrentAI(); //player community will return null
        public void setCurrentAINull();
        public void setCurrentAIDirect(IFForeignAI inAI);
        public void setCurrentAIFromWeights();

        //note that these are all in relation to the community currently being played.  Calling these on the IFForeignCommunityComponent of the player community has no effect
        public Int64 getPathTypesToPlayerCommunity();
        public void closeAllPathsBetweenCommunityAndPlayerCommunity(); //this will disable any delegations from being sent to the community from now on.  Delegations currently underway will still resolve.
        public void openAllPathsBetweenCommunityAndPlayerCommunity(); //this will cause path types to be recalculated once again
        public void setPathTypesToPlayerCommunity(Int64 inPathType);
        public bool hasPathToPlayerCommunity();
        public bool hasWaterOnlyPathToPlayerCommunity();
        public bool hasLandOnlyPathToPlayerCommunity();
        public bool hasMixedPathToPlayerCommunity();
    }
}
