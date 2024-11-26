using Cities_of_Mosaic_Isle_PublicInterfaces.Helper;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFPop : IFUID, IFSpriteSheetOverrideable, IFNamedDebugObject
    {
        //for this SO class, establish consts and enums:
        public const int cPopDestinationKinds = 5; //highest index + 1
        public enum ePopDestinationKind //this enum helps us determine what behavior should be taken when the pop ends its path
        {
            cStandAround = 0, //only when we have no destination is this true
            cPoint = 1, //just a place; nothing special to do once the pop reaches this destination
            cBuilding = 2, //when we arrive at the destination, the pop *may* (not always) take shelter in the building.  It may instead just loiter around the building.
            cMapEdge = 3, //when we arrive at the map edge, unless conditions have changed, the pop is meant to leave the map
            cOtherPop = 4 //when we arrive at where the other pop should be, if we're still in battle and the pop is a nearby enemy, engage it.  If not, probably re-target that pop.
        }

        public enum ePopDeathReason
        {
             cUnspecified = 0
            ,cViolence = 1
            ,cWounds = 2
            ,cIllness = 3
            ,cOldAge = 4
        }

        public const int cPopSexNum = 4; //highest index + 1
        public enum ePopSex
        {
             cUnisexNone = 0
            ,cMale = 1
            ,cFemale = 2
            ,cUnisexBoth = 3
        }
        public static readonly string[] cPopSexAdjectives = //these are the translatedStrings names
        {
             "pop_sex_adjective_unisexnone"
            ,"pop_sex_adjective_male"
            ,"pop_sex_adjective_female"
            ,"pop_sex_adjective_unisexboth"
        };

        public enum ePopLocation
        {
             cUnspecifiedNowhere = 0
            ,cOnCommunityMap = 1
            ,cInHome = 2
            ,cInWorkplace = 3
            ,cInDelegation = 4
            ,cInForeignCommunity = 5  //we should only really track pops for other communities in a limited manner, unless they are playable communities
                                      //anything else? 
        }

        public void changeDisplayNameTo(string inNewDisplayName);
        public IFRace getRace();
        public IFPop.ePopSex getSex();

        public bool isDead();
        public void setIsDead(bool inNowDead, IFPop.ePopDeathReason inReason, IFPop inPopCausingDeath = null); //TODO make it clear that pops cannot resurrect

        public Int64 getDayOfBirth();
        public Int64 getDayOfDeath(); //this will return Int64.MinValue if the pop is not dead
        public Int64 getWoundedDayOver();
        public Int64 getIllDayOver();
        public Int64 getPregnantDayOver();
        public bool isWounded();
        public bool isIll();
        public bool isPregnant();
        public bool isPregnantImmobile(); //unlike "isPregnant", which is just an indicator that the pop is carrying one or more fetuses, this indicates that the pop cannot work as a result of advanced pregnancy
        public bool isPopNoHealthStatus(); //returns !isWounded && !isIll && !isPregnantImmobile && !isDead()

        public void setWoundedDayOver(Int64 inDayOverWound); //while this can be called directly to make a pop wounded, 'addWound' is the appropriate function to call through to the moddable script that adds a wound to a pop.  All scripts other than the addWound script should probably call the addWound function.
        public void setIllDayOver(Int64 inDayOverIll); //while this can be called directly to make a pop ill, 'addIllness' is the appropriate function to call through to the moddable script that adds an illness to a pop.  All scripts other than the addIllness script should probably call the addIllness function.
        public void addWound(bool inSourceExposure = false, bool inSourceWorkplace = false, bool inSourceIllness = false, bool inSourceCombat = false, IFPop inPopCausingCombatWound = null, UInt64 inDaysWounded = 0); //these inputs are ordered: if any of the bools are true, the following inputs don't matter
        public void addIllness(bool inFromEnvironment = true, IFPop inPopContractedFrom = null, UInt64 inRawDaysIllness = 0); //these inputs are ordered: if the first is true, the others don't matter; if the second is non-null, the third doesn't matter
        public void setNoLongerWounded();
        public void setNoLongerIll();

        public IFRace getPregnantBabyRace();
        public void setPregnantBabyRace(IFRace inRace);
        public void setPregnantDayOver(Int64 inDayOverPregnant); //while this can be called directly to make a pop pregnant, 'letMakeBabyWithPop' is the appropriate function to call through to the moddable script that possibly makes a pop pregnant.  All scripts other than the letMakeBabyWithPop script should probably call the letMakeBabyWithPop function.
        public void setNoLongerPregnant();
        public bool canMakeBabyWithPop(IFPop inOtherPop); //calls through to script; the script will work no matter who it is called on
        public void letMakeBabyWithPop(IFPop inOtherPop); //calls through to script; the script will work no matter who it is called on.  The script calls 'canMakeBabyWithPop'
        public IFPop giveBirth(); //calls through to script; this script will only be executed if this pop is indeed valid to give birth.  If the pop does not give birth, this returns null.

        public double getMCalHealth();
        public double getProteinHealth();
        public double getWaterHealth();
        public double getHStasisHealth();
        public double getOverallHealth();

        public void setMCalHealth(double inMCalHealth);
        public void setProteinHealth(double inProteinHealth);
        public void setWaterHealth(double inWaterHealth);
        public void setHStasisHealth(double inHStasisHealth);
        public void setOverallHealth(double inOverallHealth);

        public IFPop.ePopLocation getPopLocation();
        public void placePopOnMapLocation(double inMapLocationX, double inMapLocationY); //this handles map location, map target, and pop location all at once (and clears pathfinding nodes)
        public void removePopFromMap(IFPop.ePopLocation inNewPopLocation); //this will set new pop location (and also handles sprite and pathfinding stuff under the hood)

        public Tuple<double, double> getMapLocationTuple();
        public double getMapLocationX();
        public double getMapLocationY();
        public void setMapLocation(double inMapLocationX, double inMapLocationY);

        public IFBuilding getHomeBuilding();
        public bool hasHomeBuilding();
        public void setHomeBuilding(IFBuilding inNewHome); //note: this will also set up things correctly as far as the building's member variables go.  It shouldn't harm anything if you do so yourself, but it is redundant

        public bool isCapableOfWorking(); //pops are capable of working if they have a home building and no health statuses (TODO there is a player-controllable toggle to allow ill pops to work)
        public IFBuilding getWorkplaceBuilding();
        public bool hasWorkplaceBuilding();
        public void setWorkplaceBuilding(IFBuilding inNewWork);//note: this will also set up things correctly as far as the building's member variables go.  It shouldn't harm anything if you do so yourself, but it is redundant

        public bool isCapableOfMustering(); //pops are capable of mustering if they are not wounded or pregnant immobile
        public IFBuilding getMusteringPoint();
        public bool hasMusteringPoint();
        public void setMusteringPoint(IFBuilding inNewMusteringPoint); //note: this will also set up things correctly as far as the building's member variables go.  It shouldn't harm anything if you do so yourself, but it is redundant
        public void synchronizeEquipmentWithMusteringPointCommand(); //this function tells the IFPop to change (or not) equipment as dictated by the mustering point.  This only works for loyal pops and updates the resource pool.
        public void setIsMustered(bool inIsMustered);
        public bool isMustered();

        public string getPathfindingState(); //note that all pathfinding states are held internally as lowercase (using .ToLower()), so this will return a value consistent with that
        public void setPathfindingRecalculationUrgent();
        public Tuple<double, double> getPathfindingTgtTuple(); //return is -1,-1 if there is not a target
        public ePopDestinationKind getPopDestinationKind();
        public void forcePathfindingState(string inForcedPathfindingState, bool inClearTarget); //this should be used only if a circumstance should force a pop to abandon its current pathfinding state and enter another state, probably abandoning its previous target.  Follow up with a call to setPathfindingRecalculationUrgent if target recalculation is necessary.  This function is called (as an example) when a pop starts a fight with another pop, causing both to enter the Fighting state.

        public IFDelegation getDelegation();
        public bool hasDelegation();
        public void setDelegation(IFDelegation inDelegation); //note: this will also set up things correctly as far as the delegation's member variables go.  It shouldn't harm anything if you do so yourself, but it is redundant.  It will NOT, however, set the pop's popLocation to cInDelegation

        public IFCommunity getCommunity();
        public bool hasCommunity();
        public void setCommunity(IFCommunity inCommunity);//note: this will also set up things correctly as far as the community's member variables go.  It shouldn't harm anything if you do so yourself, but it is redundant

        public double getSoldierSkill();
        public void setSoldierSkill(double inSL);

        //note that these do not add or subtract any equipment from associated resource pools; that is the responsibility of the caller
        public bool hasEquipment(IFResource inResource);
        public void replaceEquipment(IFResource inResource, double inQuality);
        public void removeEquipment(IFResource inResource);
        public double getEquipmentQuality(IFResource inResource);
        public double getAverageEquipmentQuality();
    }
}
