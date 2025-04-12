using Cities_of_Mosaic_Isle_PublicInterfaces.Helper;
using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFBuilding : IFUID, IFSpriteSheetOverrideable, IFNamedDebugObject
    {
        //for this SO class, establish consts and enums:
        public enum eBuildingWaterStatus
        {
             cOverwaterEntirely = 0
            ,cUnderwaterEntirely = 1
            ,cMixed = 2
        }

        public enum eBuildingDestructionReason
        {
             cLackOfMaintenanceOrOtherReason = 0
            ,cDeletion = 1
            ,cViolence = 2
        }

        public void changeDisplayNameTo(string inNewDisplayName);

        public IFBuildingKind getBuildingKind();
        public bool isHousing();
        public bool isWorkplace();
        public bool isMusteringPoint();

        public Int64 getMapLocationX();
        public Int64 getMapLocationY();
        public Tuple<Int64, Int64> getMapLocation();
        public Int64 getWidth(); //this will return the correct width for the rotation of the building
        public Int64 getHeight(); //this will return the correct height for the rotation of the building

        public Int64 getCurrentBuildingDurability();
        public void setCurrentBuildingDurability(Int64 inNewCBD, IFPop inPopCausingDamage = null); //won't restore a destroyed building
        public Int64 getMaxBuildingDurability();
        public void setMaxBuildingDurability(Int64 inNewMBD);

        public ReadOnlyCollection<Tuple<IFResource, double>> getConstructionResources();
        public void setConstructionResources(ReadOnlyCollection<Tuple<IFResource, double>> inResources);

        public Int64 getCalendarDayBuilt();
        public IFCalendar.calendarDate getCalendarDateBuilt();

        public bool isBuildingDestroyed();
        public void setBuildingDestroyed(bool inNewVal, eBuildingDestructionReason inReason, IFCommunity inCommunityResponsibleForDestruction = null);
        public bool getBuildingMarkedForDestruction(); //"marked for destruction" means the player has used the delete tool on the building
        public void setBuildingMarkedForDestruction(bool inNewVal); //"marked for destruction" means the player has used the delete tool on the building
        public void toggleBuildingMarkedForDestruction(); //"marked for destruction" means the player has used the delete tool on the building

        public bool isBuildingFullyBuilt();
        public void setBuildingFullyBuilt(); //this just calls "setPopDaysLeftToComplete(0)"
        public Int64 getPopDaysLeftToComplete();
        public void setPopDaysLeftToComplete(Int64 inPopDaysLeftToComplete);

        public List<bool> getAccessibleResources();
        public void setAccessibleResources(List<bool> inAccessibleResources);
        public bool isResourceAccessible(IFResource inResource);
        public bool isResourceAccessible(UInt32 inResource);
        public void setResourceAccessible(UInt32 inResource, bool inNowAccessible);

        public ReadOnlyDictionary<IFBuilding, double> getServiceQualities();
        public double calcServiceQualityForIndustry(UInt64 inIndustryMOID);
        public double calcServiceQualityForIndustry(IFIndustry inIndustry); //the difference between these functions and the above is that the above is raw values of individual buildings, while this is the total calculation of the service quality offered by the industry as a whole
        public void addServiceQuality(IFBuilding inBuilding, double inServiceQuality); //if the building is already stored in getServiceQualities, this overrides its service quality

        public double calcBuildingDurabilityMultFromConstructionResources();
        public double calcBuildingOutputMultFromConstructionResources();

        //these will return null if the building is not a workplace/not housing:
        public IFWorkplaceComponent getWorkplaceComponent();
        public IFHousingComponent getHousingComponent();
        public IFMusteringPointComponent getMusteringPointComponent();

        public IFBuilding.eBuildingWaterStatus getBuildingWaterStatus();
    }
}
