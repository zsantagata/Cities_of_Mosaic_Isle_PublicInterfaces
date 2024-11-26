using Cities_of_Mosaic_Isle_PublicInterfaces.Helper;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFResourcePool : IFUID, IFNamedDebugObject
    {
        public double getResourceQuantity(IFResource inResource);
        public double getResourceQuality(IFResource inResource);

        //for the following functions, if in*Building/in*Pop is non-null, this change of resources will be included in distribution calculations (building only) and resource view display (both).
        //If it is null, this change of resources will not be included.
        public void addResourcesOfQuality(UInt32 inResourceMOID, double inQuantityAdded, double inQualityOfAdded, IFBuilding inSourceBuilding = null, IFPop inProducingPop = null);
        public void addResourcesOfQuality(IFResource inResource, double inQuantityAdded, double inQualityOfAdded, IFBuilding inSourceBuilding = null, IFPop inProducingPop = null);
        public void subtractResource(UInt64 inMOID, double inQuantitySubtracted, IFBuilding inBuilding = null, IFPop inConsumingPop = null);
        public void subtractResource(IFResource inResource, double inQuantitySubtracted, IFBuilding inBuilding = null, IFPop inConsumingPop = null);
        //the following two are just a different name for the above functions because I always mix up if it's plural or not
        public void subtractResources(UInt64 inMOID, double inQuantitySubtracted, IFBuilding inBuilding = null, IFPop inConsumingPop = null);
        public void subtractResources(IFResource inResource, double inQuantitySubtracted, IFBuilding inBuilding = null, IFPop inConsumingPop = null);

        public void changeResourceQuality(UInt32 inResourceID, double inQualityModifyValue);
        public void changeResourceQuality(IFResource inResourceID, double inQualityModifyValue);

        public void drainOtherResourcePoolIntoThisOne(IFResourcePool inResourcePool);
        public void drainOtherResourcePoolIntoThisOne(UInt64 inResourcePoolID);
    }
}
