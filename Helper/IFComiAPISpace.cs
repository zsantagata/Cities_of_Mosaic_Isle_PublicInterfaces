using Cities_of_Mosaic_Isle_PublicInterfaces.Helper.Services;
using Cities_of_Mosaic_Isle_PublicInterfaces.InGame;
using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.Helper
{
    //when any script calls getAPI(), this is what is returned.  The class implementation has no state variables
    public interface IFComiAPISpace
    {
        //general note: functions with the same exact names but different inputs (such as putPopsFromListOnMap(List<IFPop> inPops) and putPopsFromListOnMap(ReadOnlyCollection<IFPop> inPops)) can be expected to perform the same behavior

        //accessors for important global classes:
        public IFModdableGameConsts getConsts();
        public IFModdableCustomConsts getCustomConsts();
        public IFModdableCustomScripts getCustomScripts();
        public IFModdableTranslatedStrings getTranslatedStrings();
        public IFVariableHandlerServiceProvider getVariableHandler();
        public IFCalendar getCalendar();
        public IFSaveableDifficulty getSaveableDifficulty();
        //unsure of why a script may need to access this but it won't hurt anything
        public IFModdableDifficulty getModdableDifficulty();

        //an alert is a message at the top of the screen with a corresponding message, color, and place to focus the player's attention
        //if inPop, inBuilding, inCommunity, or inDelegation To Center On is non-null, those will be the focus first (in order)
        //else if the locationX and Y are both not -1, a location will be centered on
        //else if inViewToSwitchTo is not cAlerts, an info view menu will be focused on
        //else no focus
        //(this is technically not a saveable object and is instead a UI element but this section of the API is about creating things)
        public void createAlert(string inText, int inRed = 255, int inGreen = 255, int inBlue = 255, bool inPause = false,
            IFPop? inPopToCenterOn = null, IFBuilding? inBuildingToCenterOn = null, IFCommunity? inCommunityToCenterOn = null, IFDelegation? inDelegationToCenterOn = null,
            double inLocationToCenterOnX = -1.0d, double inLocationToCenterOnY = -1.0d, bool inLocationIsOnLocalMap = false, //if inLocationIsOnLocalMap is false that means it's the diplo map
            int inViewToSwitchTo = 10 //alerts themselves is an invalid target so any other target will indicate this field is active.
            );
        /* //This is the enum of view substates:
        public enum eInfoViewSubState
        {
             cEvents = 0
            ,cCityOverview = 1
            ,cMilitary = 2
            ,cDiplomacy = 3
            ,cRaceOverview = 4
            ,cRaceHappiness = 5
            ,cRacePermissions = 6
            ,cIndustries = 7
            ,cResources = 8
            ,cHistory = 9
            ,cAlerts = 10
        };*/
        public string getDisplayStringForQuality(double inQuality); //same thing as gameConsts' getQualityFormattedString
        public string getDisplayStringForQuantity(double inQuantity);
        public string getDisplayStringForDouble(double inQuantity);
        //these functions fetch player-chosen settings from the related Info View menu:
        public bool canIllPopsOfRaceWorkInIndustry(IFRace inRace, IFIndustry inIndustry);
        public bool isRaceAllowedToHouseInBKnd(IFRace inRace, IFBuildingKind inBuildingKind);
        public bool isRaceAllowedToConsumeResource(IFRace inRace, IFResource inResource);
        public ReadOnlyCollection<bool> getResourcesEnabledListForDistributionBuildingKind(IFBuildingKind inKind);

        //pathfinding:
        public ReadOnlyCollection<string> getAllPathfindingStates();
        public double getMinTimePathfindingState(string inPathfindingState); //note that C# code internally converts from seconds to day portions, so this value is likely different than what was provided in mod files
        public double getUrgentTimePathfindingState(string inPathfindingState); //same note as above
        public ReadOnlyCollection<string> getTargetChoiceCategories(); //for pathfinding
        public bool isBuildingAccessibleByPop(IFPop inPop, IFBuilding inBuilding);
        public bool isPopAccessibleByPop(IFPop inPop, IFPop inTargetPop);
        public bool isMapEdgeAccessibleByPop(IFPop inPop);
        public bool isPointAccessibleByPop(IFPop inPop, double inX, double inY);

        //simultaneous events and delegations:
        public void setSimultaneousEventFlagIgnoreNextOn(IFEvent.eSimultaneousWithOccurrence inFlagToIgnore);
        public void setSimultaneousEventFlagIgnoreNextOff(IFEvent.eSimultaneousWithOccurrence inFlagToNoLongerIgnore);
        public void setSimultaneousEventToForceNext(IFEvent inEvent, bool inWanderers, bool inTheLady, bool inRebels, bool inGenericEnemy);
        //here, a custom passthrough for forcing certain events for certain delegations:
        public void setSimultaneousEventToForceForDelegation(IFEvent inEvent, IFDelegation inDelegation, bool inArrival);
        //this function is a helper function for foreign delegation simultaneous events:
        public IFDelegation getDelegationCurrentlyAssociatedWithEvent();
        //this function is a helper function for player delegation travel/arrival events (shouldn't be null during a delegation's event, but may be null otherwise):
        public IFDelegation getDelegationBeingResolved();

        //music:
        public ReadOnlyCollection<string> getMusicGroupNames();
        public void playMusicFromSoundGroup(string inSoundGroupName);
        public void setMusicContextualLoudnessMultiplier(double inMultiplier);

        //local map:
        public ReadOnlyCollection<IFPop> getPopsWithinLocalCommunityMap(); //NOTE: this is any pop which has pop location cOnCommunityMap, or cInHome/cInWorkplace if those buildings are on the local map.  That includes dead pops.
        public void putPopsFromListOnMap(List<IFPop> inPops);
        public void putPopsFromListOnMap(ReadOnlyCollection<IFPop> inPops);

        //diplomacy map:
        public bool getRoundTripTimeAndKindBetweenCommunities(IFCommunity inSourceCommunity, IFCommunity inTargetCommunity, ReadOnlyCollection<IFPop> inPopsInvolved, out IFCommunity.ePathTypes outPathType, out double outEstimatedReturnDate);
        public bool getRoundTripTimeAndKindBetweenCommunities(IFCommunity inSourceCommunity, IFCommunity inTargetCommunity, List<IFPop> inPopsInvolved, out IFCommunity.ePathTypes outPathType, out double outEstimatedReturnDate);
        public string getNewNameOfCommunityBeingChangedRightNow();
        //this function is a general diplo map helper function:
        public Tuple<Int64, Int64> getDiploMapDimensions();
        public double getDiploMapUpscaleFromSandboxSelectionMapRatio();

        //military-related functions:
        public bool isHostileCommunity(IFCommunity inCommunity);
        public ReadOnlyCollection<IFCommunity> getHostileCommunities();
        public bool isBattleOngoing();
        //this function indicates that a pops on the map are now hostile (all of those that are loyal to the input community):
        public void makePopsOnLocalMapHostile(IFCommunity inLoyaltyOfHostilePops);//note that the local community, invisible communities, abstract communities, and GenericEnemy and Rebels (because they are always counted as hostile) as inputs won't do anything

        //functions related to moddable object fetching:
        public int getTotalCountOfMO(string inType);
        public IFMOID getMOFromMOID(string inType, UInt32 inMOID); //returns null if it does not exist
        public IFMOID getMOFromMOID(string inType, Int64 inMOID); //returns null if it does not exist
        public IFMOID getMOFromMOID(string inType, UInt64 inMOID); //returns null if it does not exist
        public IFMOID getMOFromMOID(string inType, int inMOID); //returns null if it does not exist
        public IFMOID getMOFromInternalName(string inType, string inName);
        //scripts may commonly need a list of resources with certain qualities (such as "a list of all military equipment"):
        public ReadOnlyCollection<IFResource> getResourcesWithAnyOfResQualityFlags(Int64 inQualities);

        //functions related to saveable object fetching:
        public int getTotalCountOfSO(string inType);
        public List<UInt64> getUIDsOfAllSO(string inType);
        public IFUID getSO(string inType, int inUID); //returns null if it does not exist
        public IFUID getSO(string inType, Int64 inUID); //returns null if it does not exist
        public IFUID getSO(string inType, UInt32 inUID); //returns null if it does not exist
        public IFUID getSO(string inType, UInt64 inUID); //returns null if it does not exist
        //these are unique communities which scripts may commonly need:
        public IFCommunity getLocalCommunity();
        public IFCommunity getWandererCommunity();
        public IFCommunity getWanderersCommunity(); //returns same as above; just a rename for convenience
        public IFCommunity getRebelCommunity();
        public IFCommunity getRebelsCommunity(); //returns same as above; just a rename for convenience
        public IFCommunity getGenericEnemyCommunity();
        public IFCommunity getGenericEnemiesCommunity(); //returns same as above; just a rename for convenience
        public IFCommunity getGenericEnemysCommunity(); //returns same as above; just a rename for convenience
        public IFCommunity getTheLadyCommunity();
        public IFCommunity getTheLadysCommunity(); //returns same as above; just a rename for convenience

        //functions related to saveable object creation:
        //this function creates a pop:
        public IFPop generateNewPop(
            //race and community are necessary; all else can be skipped if desired
            IFRace inRace,
            IFCommunity inCommunityLoyalty,
            /*basics*/
        string inName = "", //skipping will pull from the race pool of names
            Int64 inDOB = Int64.MinValue, //skipping will calculate a date matching racial life expectancy
            double inSoldierSkill = double.MinValue,
            IFPop.ePopSex inSex = IFPop.ePopSex.cUnisexNone, //skipping will either make the pop UnisexNone or a valid sex randomly chosen of the sexes.  Note that if input is invalid for the race, it will be rerolled.
            /*equipment*/
            Dictionary<IFResource, double>? inEquipment = null, //skipping will have pop holding nothing
            /*location*/
            IFDelegation? inDelegation = null, //skipping means the pop is not in a delegation
            bool inPlaceOnMap = false, //skipping means the pop is not on the map
            double inMapLocationX = 0.0d, //skipping means the pop, if it is on the map, will be placed at X=0.0d
            double inMapLocationY = 0.0d, //skipping means the pop, if it is on the map, will be placed at Y=0.0d
            /*health*/
            Int64 inWoundedDayOver = -1L, Int64 inIllDayOver = -1L, Int64 inPregnantDayOver = -1L, //skipping means the pop will not be wounded/ill/pregnant.  The pregnant baby's race will be the mother's race (the caller will have to change that manually afterwards if they care).
            double inMCalHealth = double.MaxValue, double inProteinHealth = double.MaxValue, double inWaterHealth = double.MaxValue, double inHStasisHealth = double.MaxValue, double inOverallHealth = double.MaxValue //skipping means the pop will have full health values
            );

        //this function creates a foreign community and places it on the diplo map.  the script caller will still need to initialize master/servant relations and add resources to the resource pool
        public IFCommunity createCommunity(
            Dictionary<IFRace, UInt64> inUndeclaredPops,
            IFCommunity.eCommunityType inCommunityType, //only normal/invisible/abstract are valid
            bool inOpenPathToPlayerCommunity,
            Int64 inForeignCommunityStatus,
            IFForeignAI inAIOverride, //if this is null, AI will be assigned through random draw of valid ones, and inMilStrengthCalcOverride and inEconStrengthCalcOverride will be ignored
            IFEconStrengthCalculation inEconStrengthCalcOverride, //this will be used directly if inAIOverride is not null
            IFMilStrengthCalculation inMilStrengthCalcOverride, //this will be used directly if inAIOverride is not null
            Int64 inDiploMapLocationX, //if the diplo map location is off the map, the community will not be generated.  However, remember that communities can be on top of one another (even if they really shouldn't be without being hidden)
            Int64 inDiploMapLocationY, //if the diplo map location is off the map, the community will not be generated.  However, remember that communities can be on top of one another (even if they really shouldn't be without being hidden)
            double inForeignUnderwaterRatio = 0.5d,
            string inName = ""
            );

        //this function creates a building but does not place it on any map:
        public IFBuilding createBuildingNotPlaced(
            IFBuildingKind inBuildingKind,
            Int64 inMapLocationX, //building location can't change after creation, so if this building is going to be placed on a map, make sure these values are correct
            Int64 inMapLocationY, //building location can't change after creation, so if this building is going to be placed on a map, make sure these values are correct
            IFResource inConstructionResource,
            string inName = ""
            );

        //this function creates a delegation and places it on the diplo map:
        //(it can be used with the source community as the player's community, but usually that's done deliberately by the player rather than being called by scripts.  Emigration delegations as a result of pops abandoning the community could be a common exception.)
        public IFDelegation createDelegation(
            IFCommunity inSourceCommunity,
            IFCommunity inTargetCommunity,
            IFDelegation.eDelegationType inDelegationType,
            Dictionary<IFResource, Tuple<double, double>> inResourcesDelegationStartsWith, //first double is quantity, second is quality.  This is NOT subtracted from the source community's resource pool (the caller must do so if desired)
            List<IFPop> inPopsOnExpedition //it is the responsibility of the caller to make sure this only has alive, loyal, not-already-on-a-delegation pops if that is what the caller desires
            );

        //note that the caller will still have to add the historical occurrence to the community's history for all these
        public IFHistoricalOccurrence createNewHistoricalOccurrenceToday(
            IFHistoryActor.eActorKind inActorKind, UInt64 inActorID,
            string inActionText, string inActionMajorAdjective, List<string> inActionMinorAdjectives,
            IFHistoryTarget.eTargetKind inTargetKind, UInt64 inTargetID,
            Int64 inEffectCBImpact, Dictionary<string, int> inEffectOtherImpacts);
        public IFHistoricalOccurrence createNewHistoricalOccurrenceToday(
            IFHistoryActor.eActorKind inActorKind, UInt64 inActorID,
            string inActionText, string inActionMajorAdjective, ReadOnlyCollection<string> inActionMinorAdjectives,
            IFHistoryTarget.eTargetKind inTargetKind, UInt64 inTargetID,
            Int64 inEffectCBImpact, Dictionary<string, int> inEffectOtherImpacts);
        public IFHistoricalOccurrence createNewHistoricalOccurrenceOnDate(Int64 inDate,
            IFHistoryActor.eActorKind inActorKind, UInt64 inActorID,
            string inActionText, string inActionMajorAdjective, List<string> inActionMinorAdjectives,
            IFHistoryTarget.eTargetKind inTargetKind, UInt64 inTargetID,
            Int64 inEffectCBImpact, Dictionary<string, int> inEffectOtherImpacts);
        public IFHistoricalOccurrence createNewHistoricalOccurrenceOnDate(Int64 inDate,
            IFHistoryActor.eActorKind inActorKind, UInt64 inActorID,
            string inActionText, string inActionMajorAdjective, ReadOnlyCollection<string> inActionMinorAdjectives,
            IFHistoryTarget.eTargetKind inTargetKind, UInt64 inTargetID,
            Int64 inEffectCBImpact, Dictionary<string, int> inEffectOtherImpacts);

        //calculations passthroughs:
        public void shuffleList<T>(List<T> inList); //this function randomly rearranges the order of the list.  USER BEWARE: this changes inList itself so if you needed the original order you need to keep a deep copy
        public T getOneRandomItemFromList<T>(List<T> inList); //this function randomly returns one element from within the list.  Better to use this than shuffleList if you need only one (or a small number, allowing repeats, of) element(s) from a list.  If the list is empty this returns the default of T, which is usually zero/null string/null reference/similar sort of things to those.
        public T getOneRandomItemFromList<T>(ReadOnlyCollection<T> inList); //this function randomly returns one element from within the list.  Better to use this than shuffleList if you need only one (or a small number, allowing repeats, of) element(s) from a list.  If the list is empty this returns the default of T, which is usually zero/null string/null reference/similar sort of things to those.
        public bool calcProb(double inProb); //input between 0.0d and 1.0d inclusive
        public bool calcProb(int inNum, int inDenom); //inNum non-negative, inDenom positive
        public bool calcProb(Int64 inNum, Int64 inDenom);
        public bool calcProb(double inNum, double inDenom);
        public double calcDistanceBetweenPoints(Tuple<double, double> inFirstPoint, Tuple<double, double> inSecondPoint);
        public double calcDistanceBetweenMapRectangleAndMapTile(int inRectX, int inRectY, int inRectWidth, int inRectHeight, int inTileX, int inTileY);
        public double calcDistanceBetweenPopAndBuildingOnMap(IFPop inPop, IFBuilding inBuilding, IFMap inMap);
        public Tuple<double, double> calcNoiseBasedOnParameters(
            Int64 inWidth,
            Int64 inHeight,
            double inFrequency,
            Int64 inNoiseType,
            Int64 inOctaves,
            float inCellularJitter,
            out List<List<double>> outNoise,
            bool inCenterBias = false
            );
        public Tuple<double, double> calcNoiseFractalParameters(
            Int64 inWidth,
            Int64 inHeight,
            double inFrequency,
            Int64 inOctaves,
            float inCellularJitter,
            double inFractalGain,
            double inFractalLacunarity,
            out List<List<double>> outNoise
            );
        public double calcOutputFromInputs(List<double> inQuantities, List<double> inQualities, bool inComplements, bool inSubstitutes);
        public double calcQQOutputAngleFromQQDialVal(IFBuilding inBuilding);
        public double calcRand(); //output is [0.0, 1.0d)
        public Int64 calcRandIntUnder(Int64 inArgument); //0 returns 0, otherwise input must be positive
    }
}
