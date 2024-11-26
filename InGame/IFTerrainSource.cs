using Cities_of_Mosaic_Isle_PublicInterfaces;
using Cities_of_Mosaic_Isle_PublicInterfaces.Helper;
using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFTerrainSource : IFMOID, IFEncyclopediaObject, IFNamedDebugObject
    {
        //an in-game terrain source is: a quality of a map tile on the map where groups of resources are allowed to be extracted
        //this class holds information; it does not actually perform any work

        //GUARANTEE SECTION:
        //here we list the guarantees that a derived class must meet as a function of its existence as an IFIndustry:
        //A) This class represents a moddable object; therefore, by the time any script can access members of this class, the return values of all functions will only depend on the function's inputs.
        //  i) as a moddable object, this class has a tag list.  getTagList() will return a list of unique tags.  hasTag(string inTag) will return the same value as getTagList().Contains(inTag).  getTagList() will not return null, but the list may be empty.
        //B) getAllowedResources* will never return null, but the return list may be empty.  A single list will not contain an IFResource more than once.
        //  i) isAllowedResource*(inResource) will return the same value as getAllowedResources*().Contains(inResource)
        //C) getDesolationRecoveryPerDay will never return an infinite or NaN.  It may return negative, positive, or zero.

        //for this MO class, establish consts and enums:
        public const int cNumDistributionTypes = 2;
        public enum eDistributionType
        {
             cValueFractalBlobby = 0
            ,cSimplexFractalStringy = 1
        }

        public bool hasTag(string inTag);
        public ReadOnlyCollection<string> getTagList();

        public ReadOnlyCollection<IFResource> getAllowedResourcesLand();
        public bool isAllowedResourceLand(IFResource inResource);
        public ReadOnlyCollection<IFResource> getAllowedResourcesWater();
        public bool isAllowedResourceWater(IFResource inResource);

        public eDistributionType getDistributionType();
        public double getDesolationRecoveryPerDay();
    }
}
