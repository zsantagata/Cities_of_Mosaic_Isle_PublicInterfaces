using Cities_of_Mosaic_Isle_PublicInterfaces.Helper;
using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFRace : IFMOID, IFEncyclopediaObject, IFNamedDebugObject
    {
        //an in-game race is: an attribute of a pop, which all pops have, which can impact
        //  how they work, live, fight, negotiate, appear, etc.  Races in each city will have values associated with them,
        //  but those will be stored in a pool of info elsewhere.
        //in terms of lore, race is a physical attribute only.  Culture/behavior is not associated with race explicitly.
        //  the values held by IFRace come only from the physical structure of the race.

        //GUARANTEE SECTION:
        //here we list the guarantees that a derived class must meet as a function of its existence as an IFRace:
        //A) This class represents a moddable object; therefore, by the time any script can access members of this class, the return values of all functions will only depend on the function's inputs.
        //  i) as a moddable object, this class has a tag list.  getTagList() will return a list of unique tags.  hasTag(string inTag) will return the same value as getTagList().Contains(inTag).  getTagList() will not return null, but the list may be empty.
        //B) getSpecialAttributes() will return an Int64 that is a logical OR of the values in eSpecialAttributes that are true for this race.
        //  i) the bool accessors will return the same value as calling getSpecialAttributes() and checking if the bit is 1.
        //C) getIndustryProductivity will return a positive, non-infinite, non-NaN number.
        //D) getIndustryQualities() will return an Int64 that is a logical OR of the value in eProductivityModifiers that are true for this race, for the input industry.
        //  i) the bool accessors will return the same value as calling getIndustryQualities() for this industry and checking if the bit is 1.
        //E) getResourcesToGenerateWhileHoused will not return null, but may be an empty list.  An IFResource will appear at most once in the list.
        //F) isResourceGeneratedWhileHoused(IFResource inResource) returns the same value as getResourcesToGenerateWhileHoused().Contains(inResource).
        //G) getResourcesToGenerateWhileWorking will not return null, but may be an empty list.  An IFResource will appear at most once in the list.
        //H) isResourceGeneratedWhileWorking(IFResource inResource) returns the same value as getResourcesToGenerateWhileWorking().Contains(inResource).
        //G) getFreeCombatResources will not return null, but may be an empty list.  An IFResource will appear at most once in the list.
        //H) isResourceFreeInCombat(IFResource inResource) returns the same value as getFreeCombatResources().Contains(inResource).
        //I) getSpecificResourceModifier() will return an Int64 that is a logical OR of the values in eConsumptionSpecificResourceModifiers that are true for this race, for the input resource.
        //  i) the bool accessors will return the same value as calling getSpecificResourceModifier() for this resource and checking if the bit is 1.
        //J) getBuildingsToTreatAsHousing will not return null, but may be an empty list.  An IFBuildingKind will appear at most once in the list.
        //K) isBuildingTreatedAsHousing (IFBuildingKind inBuildingKind) returns the same value as getBuildingsToTreatAsHousing().Contains(inBuildingKind).
        //L) getChanceToBirthOtherRace() will return a value between 0.0d and 1.0d, inclusive.
        //M) get*PregnancyDays() will return a non-negative value.
        //  i) getVisiblePregnancyDays() will return a value greater than or equal to getIncapablePregnancyDays().
        //N) get*NameList() will not return null, and will not be an empty list.  Entries in the list might repeat.
        //O) get*Speed() will return a positive, non-infinite, non-NaN number.
        //P) getSSIDs() will not return null, and will not be an empty list.  Entries in the list will not repeat, and will indicate valid sprite sheets.


        //for this MO class, establish consts and enums:
        public const int cProductivityModifiersMask = 0x3FFFF;
        [Flags]
        public enum eProductivityModifiers //this is per industry
        {
             cNone = 0x0
            ,cCannotWorkInIndustry = 0x1
            ,cOverwaterNerf = 0x2
            ,cOverwaterBuff = 0x4
            ,cUnderwaterBuff = 0x8
            ,cUnderwaterNerf = 0x10
            ,cQuantityBuff = 0x20
            ,cQualityBuff = 0x40
            ,cQuantityNerf = 0x80
            ,cQualityNerf = 0x100
            ,cMoreDanger = 0x200
            ,cSameRaceBuff = 0x400
            ,cSameRaceNerf = 0x800
            ,cDifferentRaceBuff = 0x1000
            ,cDifferentRaceNerf = 0x2000
            ,cDislikeIndustry = 0x4000 //decreases worker happiness
            ,cLikeIndustry = 0x8000 //increases worker happiness
            ,cLessDanger = 0x10000
            ,cNoDanger = 0x20000
        }
        //mutually exclusive eProductivityModifiers:
        //cCannotWorkInIndustry and anything else
        //cOverwaterNerf and cOverwaterBuff
        //cUnderwaterBuff and cUnderwaterNerf
        //cQuantityBuff and cQuantityNerf
        //cQualityBuff and cQualityNerf
        //cMoreDanger and cLessDanger and cNoDanger (only one can be true at most)
        //cSameRaceBuff and cSameRaceNerf
        //cDifferentRaceBuff and cDifferentRaceNerf
        //cDislikeIndustry and cLikeIndustry

        public const int cConsumptionSpecificResourceModifiersMask = 0x1F;
        [Flags]
        public enum eConsumptionSpecificResourceModifiers //per resource
        {
             cNone = 0x0
            ,cMoreHealthFromConsuming = 0x1
            ,cMoreHappinessFromConsuming = 0x2
            ,cNoHealthFromConsuming = 0x4
            ,cNoHappinessFromConsuming = 0x8
            ,cDoNotConsume = 0x10
        }
        //mutually exclusive eConsumptionSpecificResourceModifiers:
        //cMoreHealthFromConsuming and cNoHealthFromConsuming
        //cMoreHappinessFromConsuming and cNoHappinessFromConsuming
        //cDoNotConsume and anything else

        public const int cSpecialAttributesMask = 0xFDF;
        public enum eSpecialAttributes //the commented out ones should be tags; the rest are fine as hard-coded
        {
             cNone = 0x0
            ,cIsUnisex = 0x1 //if this is true, instead of having two sexes (male and female) of this race, there is only one sex of this race.  That sex (unisex/male/female/unisex) is determined by CanMotherChildren and CanFatherChildren
            ,cIgnoreLocalLandSpeed = 0x2 //if this is true, pops will not care about the biome land speed of the local community when moving
            ,cIgnoreLocalWaterSpeed = 0x4 //if this is true, pops will not care about the biome land speed of the local community when moving
            ,cIgnoreWorldLandSpeed = 0x8 //if this is true, pops will not care about the biome land speed of the world map when moving
            ,cIgnoreWorldWaterSpeed = 0x10 //if this is true, pops will not care about the biome land speed of the world map when moving
                //0x20 unused
            ,cCanBreatheUnderwater = 0x40
            ,cCanBreatheOverwater = 0x80
            ,cHiddenFromPlayerSight = 0x100 //use this for secret races
            ,cCanMotherChildren = 0x200 //"can the females/unisex of this race give birth to children?"
            ,cCanFatherChildren = 0x400 //"can the males/unisex of this race father children?"
            ,cCrossBreedPossible = 0x800 //"can a member of this race and another race create children if their sexes otherwise allow it?" -- only one of the parents needs this special attribute
        }

        public bool hasTag(string inTag);
        public ReadOnlyCollection<string> getTagList();

        public string getSingleNoun();
        public string getPluralNoun();
        public string getCollectiveNoun();
        public string getAdjective();

        public Int64 getSpecialAttributes();
        public bool isUnisex();
        public bool isIgnoreLocalLandSpeed();
        public bool isIgnoreLocalWaterSpeed();
        public bool isIgnoreWorldLandSpeed();
        public bool isIgnoreWorldWaterSpeed();
        public bool isCanBreatheUnderwater();
        public bool isCanBreatheOverwater();
        public bool isHiddenFromPlayerSight();
        public bool isCanMotherChildren();
        public bool isCanFatherChildren();
        public bool isCrossBreedPossible();

        public double getIndustryProductivity(IFIndustry inIndustry);
        public Int64 getIndustryQualities(IFIndustry inIndustry);

        public bool isIndustryCannotWorkIn(IFIndustry inIndustry);
        public bool isIndustryOverwaterNerf(IFIndustry inIndustry);
        public bool isIndustryOverwaterBuff(IFIndustry inIndustry);
        public bool isIndustryUnderwaterNerf(IFIndustry inIndustry);
        public bool isIndustryUnderwaterBuff(IFIndustry inIndustry);
        public bool isIndustryQuantityBuff(IFIndustry inIndustry);
        public bool isIndustryQualityBuff(IFIndustry inIndustry);
        public bool isIndustryQuantityNerf(IFIndustry inIndustry);
        public bool isIndustryQualityNerf(IFIndustry inIndustry);
        public bool isIndustryMoreDanger(IFIndustry inIndustry);
        public bool isIndustryNoDanger(IFIndustry inIndustry);
        public bool isIndustryLessDanger(IFIndustry inIndustry);
        public bool isIndustrySameRaceBuff(IFIndustry inIndustry);
        public bool isIndustrySameRaceNerf(IFIndustry inIndustry);
        public bool isIndustryDifferentRaceBuff(IFIndustry inIndustry);
        public bool isIndustryDifferentRaceNerf(IFIndustry inIndustry);
        public bool isIndustryDisliked(IFIndustry inIndustry);
        public bool isIndustryLiked(IFIndustry inIndustry);

        public ReadOnlyCollection<Tuple<IFResource, double>> getResourcesToGenerateWhileHoused();
        public bool isResourceGeneratedWhileHoused(IFResource inResource);

        public Int64 getSpecificResourceModifier(IFResource inResource);
        public bool isResourceMoreHealthFromConsuming(IFResource inResource); //TODO this could apply to food, ill heal, wound heal, or hstasis resources
        public bool isResourceMoreHappinessFromConsuming(IFResource inResource); //let this stay as it is
        public bool isResourceNoHealthFromConsuming(IFResource inResource);
        public bool isResourceNoHappinessFromConsuming(IFResource inResource);
        public bool isResourceNotConsumedByThisRace(IFResource inResource);
        //TODO a flag for "treat this resource as food/ill heal/wound heal/hstasis/drug"

        public ReadOnlyCollection<IFBuildingKind> getBuildingsToTreatAsHousing();
        public bool isBuildingTreatedAsHousing(IFBuildingKind inBuildingKind);

        public Int64 getHappinessBaseShift();
        public string getHappinessBaseShiftText();
        public string getHappinessIndividualCombinationText();
        public string getHappinessEmploymentText();
        public string getHappinessKinText();
        public string getHappinessMatesText();
        public string getHappinessJealousyText();
        public string getHappinessIndustryLeaderCountText();
        public string getHappinessIllnessInCommunityText();

        public Int64 getLifespanYearsAverage();

        public double getChanceToBirthOtherRace();
        public Int64 getTotalPregnancyDays();
        public Int64 getIncapablePregnancyDays();
        public Int64 getVisiblePregnancyDays();

        public ReadOnlyCollection<string> getSSIDs();

        //these are multiplicative factors on top of gameConsts.cPopVelocity and gameConsts.cDelegationVelocityBase
        public double getLandSpeed(); //note that pops who cannot breathe overland will not path onto it, or travel in delegations across it, but this must still be positive for pops who *somehow* find themselves on the local map in inappropriate terrain
        public double getWaterSpeed(); //note that pops who cannot breathe underwater will not path onto it, or travel in delegations across it, but this must still be positive for pops who *somehow* find themselves on the local map in inappropriate terrain
    }
}
