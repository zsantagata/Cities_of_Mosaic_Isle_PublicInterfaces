using Cities_of_Mosaic_Isle_PublicInterfaces.Helper;
using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFBuildingKind : IFMOID, IFEncyclopediaObject, IFMapTileObject, IFNamedDebugObject
    {
        //an IFBuildingKind is: an attribute of a building that determines its function, its shape, and its appearance

        //GUARANTEE SECTION:
        //here we list the guarantees that a derived class must meet as a function of its existence as an IFBuildingKind:
        //A) This class represents a moddable object; therefore, by the time any script can access members of this class, the return values of all functions will only depend on the function's inputs.
        //  i) as a moddable object, this class has a tag list.  getTagList() will return a list of unique tags.  hasTag(string inTag) will return the same value as getTagList().Contains(inTag).  getTagList() will not return null, but the list may be empty.
        //B) getBuildingKindQualities() will return an Int64 that is a logical OR of the values in eBuildingKindQualities that are true for this buildingKind.
        //  i) the bool accessors will return the same value as calling getBuildingKindQualities() and checking if the bit is 1.
        //  ii) all conditions as per the comment under "eBuildingKindQualities" declaration must be true.
        //  iii) if isUnderwaterEnable would return false, isUnderwaterCoastalOnly will return false.  if isOverwaterEnable would return false, isOverwaterCoastalOnly will return false.
        //  iv) if isServiceWorkplace would return false, then isForceBestQualityService will return false
        //C) getMusteringType() will return an Int64 that is a logical OR of the values in eMusteringType that are true for this buildingKind.
        //  i) the bool accessors will return the same value as calling getMusteringType() and checking if the bit is 1.
        //D) getHeight() and getWidth() will return positive integers
        //E) getPopDaysToBuild() will return a positive integer
        //F) getBaseDurability() will return a positive integer
        //G) getAllResourceCombinationsToBuild() will not return null, but may be an empty list.  An IFResource will appear at most once in each inner list.  The values will be non-negative.  Only one inner list at most will have all values sum to 0.
        //I) getDurabilityMultForConstructionResource() will return a positive, non-infinite, non-NaN number.
        //J) getBeautySynergyBuildings() will not return null, but may be an empty list.  An IFBuildingKind will appear at most once in the list.
        //K) hasBeautySynergyWith(IFBuildingKind inBuildingKind) will return the same value as getBeautySynergyBuildings().Contains(inBuildingKind)
        //L) if isHousing() is true, getHousingCapacity() will return a positive integer
        //M) getHousingQualityFactor() will return a non-infinite, non-NaN number.
        //N) getResourcesRestrictedByHousing() will not return null, but may be an empty list.  An IFResource will appear at most once in the list.
        //O) isResourceRestrictedByHousing(IFResource inResource) will return the same value as getResourcesRestrictedByHousing().Contains(inResource)
        //P) if isWorkplace() is true, getMaxWorkers() will return a positive integer
        //Q) if isWorkplace() is true, getIndustry() will not return null.
        //R) getRadius() will return a non-negative value (note: depending on the building, getRadius() may not even be used)
        //S) getDesolationRateInRadius() will return a non-infinite, non-NaN number.  Note that this is workplace-specific.
        //T) getResourceInputsBigTuple() will not return null, but may be an empty list.  Each IFResource will be the first element of the tuple at most one time.
        //U) getInvalidOutputsForInputResource, getOtherRequiredResourcesForInputResource, and getExclusiveResourcesForInputResource will not return null, but may be an empty list.  An IFResource will appear within a single return list at most one time.  (But otherwise that one IFResource could be in every return list -- there are no cross-restrictions.)
        //  i) The return list of getExclusiveResourcesForInputResource will not contain the input resource.
        //  ii) The return list of getInvalidOutputsForInputResource will not include output resources that are not in getOutputResources()
        //V) isPossibleResourceInput, getInvalidOutputsForInputResource, getOtherRequiredResourcesForInputResource, and getExclusiveResourcesForInputResource return the same values as searching getResourceInputsBigTuple for the list element whose (first element of the tuple) is equal to the input resource.
        //W) getRequiredResourceInputs() will not return null, but may be an empty list.  An IFResource will appear at most once in the list.
        //X) isResourceInputRequired(IFResource inResource) will return the same value as getRequiredResourceInputs().Contains(inResource)
        //Y) getOutputResources() will not return null, but may be an empty list.  An IFResource will appear at most once in the list.  One (and only one) of the elements of the list may be null (this indicates script output).
        //Z) isOutputResource(IFResource inResource) will return the same value as getOutputResources().Contains(inResource)
        //A2) if isWorkplace() is true, getOutputResourceQuanDailyFactor() will return a positive, non-infinite, non-NaN number.
        //B2) if isWorkplace() is true, getInputResourceQuanDailyFactor() will return a positive, non-infinite, non-NaN number.
        //C2) getBeautyRadius() will return a non-negative number
        //D2) getSSIDs() will not return null, and will not be an empty list.  Entries in the list will not repeat, and will indicate valid sprite sheets.
        //E2) getInputWeightForInputResource() will return a non-negative number (but it might be 0)
        //F2) canOutputDirectToScript() will return true if the buildingkind is a workplace and getOutputDirectToScriptName() would return a non-empty string
        //G2) getOutputDirectToScriptName() will return an empty string if the buildingkind is not a workplace


        //for this MO class, establish consts and enums:
        public const int cBuildingKindQualitiesMask = 0x3FFFF;
        public const int cBuildingKindNonWorkplaceMask = 0x1FFF;
        [Flags]
        public enum eBuildingKindQualities
        {
             cNone = 0x0
            ,cUnderwaterEnable = 0x1
            ,cOverwaterEnable = 0x2
            ,cPassable = 0x4
            //0x8 //TODO unused
            ,cIsRoad = 0x10 //hardcoded, not sure how to do it otherwise.  If a building is a road, it forms part of the road network.  Roads make overland travel quicker.
            ,cIsBridge = 0x20 //harcoded, not sure how to do it otherwise.  Pops that can only breathe overwater can travel on bridges even if the bridge is built on water.  Pop velocity along bridges is the higher of their two velocities.
            ,cIsCanal = 0x40 //harcoded, not sure how to do it otherwise.  Pops that can only breathe underwater can travel on canals even if the canal is built on land.  Pop velocity along canals is the higher of their two velocities.
            ,cOnlyOne = 0x80
            ,cPlayerMayEnterText = 0x100
            ,cPlayerMayChangeName = 0x200
            ,cDisplaysNoMenu = 0x400
            ,cUnderwaterOnlyCoastal = 0x800 //this being true will restrict this building to being built alongside a coast if built underwater
            ,cOverwaterOnlyCoastal = 0x1000 //this being true will restrict this building to being built alongside a coast if built overwater

            ,cUsesLandResources = 0x2000 //see above
            ,cChoosesOutputResource = 0x4000 //this makes a goods building (from land or not) have a selectable output and provide full quantity of that resource, instead of splitting quantity across all possible output resources
            ,cServiceWorkplace = 0x8000 //this makes a building a service workplace, meaning it does not output goods but instead a service (that often impacts other buildings, houses specifically, but could be fed to script)
            ,cCanUseNoInputs = 0x10000 //this makes a workplace building able to use no inputs
            ,cForceBestQualityService = 0x20000 //this forces a service building to provide the best quality service, sacrificing all quantity.  Should be used for those buildingKinds where quantity does not matter.  Hides qual/quan dial.
        }
        //conditions:
        //at least one of "underwater enable" and "overwater enable" must be true
        //if underwaterOnlyCoastal is true, then underwaterEnable must be true
        //if overwaterOnlyCoastal is true, then overwaterEnable must be true
        //mutually exclusive eBuildingKindQualities (meaning only at most one of the flags within these pairs can be true):
        //cPlayerMayEnterText and cDisplaysNoMenu
        //cPlayerMayChangeName and cDisplaysNoMenu
        //cUsesLandResources and cForceBestQualityService
        //cChoosesOutputResource and cForceBestQualityService
        //all workplace qualities must be false if the buildingKind is not a workplace
        //if a buildingKind is a workplace and has no inputs in getResourceInputsBigTuple, then cCanUseNoInputs must be true
        //if a buildingKind's industry is not a service industry, cForceBestQualityService is effectively false
        //if a buildingKind is a workplace and has no outputs in getOutputResources(), then cChoosesOutputResource must be false (the building can only be a service building or output to script)
        //if cChoosesOutputResource is false, cCanOutputDirectToScript will be made false

        public const int cMusteringTypeCount = 3;
        public enum eMusteringType
        {
             cDisabled = 0
            ,cDuringRealBattle = 1 //whenever there is a real battle, all pops who have this building as a mustering point will be told to muster here (when battle goes high or when assignment of this building to a pop goes high is when the command is given, TODO)
            ,cAlways = 2 //all pops assigned this building as a mustering point will always be told to muster here
        }

        //only one of these can be chosen for a buildingkind
        public const int cRadiusVisualFeedbackFlagsMask = 0x7F;
        public enum eRadiusVisualFeedbackFlags
        {
             cNone = 0x0
            ,cAllOtherBuildings = 0x1
            ,cWorkplaces = 0x2
            ,cHousing = 0x4
            ,cMusteringPoints = 0x8
            ,cDistribution = 0x10
            ,cAppropriateResourceParcels = 0x20
            ,cCustomScript = 0x40
        }


        public bool hasTag(string inTag);
        public ReadOnlyCollection<string> getTagList();

        public Int64 getBuildingKindQualities();
        public bool isUnderwaterEnable();
        public bool isOverwaterEnable();
        public bool isPassable();
        public bool isRoad();
        public bool isBridge();
        public bool isCanal();
        public bool isOnlyOne();
        public bool isPlayerMayEnterText();
        public bool isPlayerMayChangeName();
        public bool isDisplaysNoMenu();
        public bool isUnderwaterCoastalOnly();
        public bool isOverwaterCoastalOnly();
        public bool isUsesLandResources();
        public bool isChoosesOutputResource();
        public bool isCanUseNoInputs();
        public bool isForceBestQualityService();
        public bool isServiceWorkplace();

        public ReadOnlyCollection<string> getSSIDs();

        public Int64 getPopDaysToBuild();
        public Int64 getBaseDurability();

        public ReadOnlyCollection<ReadOnlyCollection<Tuple<IFResource, double>>> getAllResourceCombinationsToBuild();
        public double getOutputMultForConstructionResource(IFResource inResource);
        public double getDurabilityMultForConstructionResource(IFResource inResource);

        public Int64 getBeautyValue();
        public double getBeautyRadius();
        public ReadOnlyCollection<IFBuildingKind> getBeautySynergyBuildings();
        public bool hasBeautySynergyWith(IFBuildingKind inBuildingKind);

        public eRadiusVisualFeedbackFlags getRadiusVisualFeedbackFlag();

        //housing accessors:
        public bool isHousing();
        public Int64 getHousingCapacity();
        public double getHousingQualityFactor();
        public double getHousingQualityShiftForRace(IFRace inRace);
        public ReadOnlyCollection<IFResource> getResourcesRestrictedByHousing();
        public bool isResourceRestrictedByHousing(IFResource inResource);

        //workplace accessors:
        public bool isWorkplace();
        public Int64 getMaxWorkers();
        public bool isHasDailyScript();
        public bool isHasWorkplaceScript();
        public IFIndustry getIndustry();
        public double getRadius();
        public double getDesolationRateInRadius();
        public bool canOutputDirectToScript();
        public string getOutputDirectToScriptName();

        public ReadOnlyCollection<Tuple<IFResource, ReadOnlyCollection<IFResource>, ReadOnlyCollection<IFResource>, ReadOnlyCollection<IFResource>, double, double>> getResourceInputsBigTuple();
        public bool isPossibleResourceInput(IFResource inResource);
        public ReadOnlyCollection<IFResource> getInvalidOutputsForInputResource(IFResource inResource);
        public ReadOnlyCollection<IFResource> getOtherRequiredResourcesForInputResource(IFResource inResource);
        public ReadOnlyCollection<IFResource> getExclusiveResourcesForInputResource(IFResource inResource);
        public double getInputWeightForInputResource(IFResource inResource);
        public double getFitnessFactorForInputResource(IFResource inResource);

        public ReadOnlyCollection<IFResource> getRequiredResourceInputs();
        public bool isResourceInputRequired(IFResource inResource);

        public ReadOnlyCollection<IFResource> getOutputResources();
        public bool isOutputResource(IFResource inResource);

        public ReadOnlyCollection<string> getCustomButtonInternalNames();

        public Int64 getMusteringType();
        public bool isMusteringPoint();
        public bool isMusteringTypeDuringRealBattle();
        public bool isMusteringTypeAlways();

        public double getOutputResourceQuanDailyFactor();
        public double getInputResourceQuanDailyFactor();
    }
}
