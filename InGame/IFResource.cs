using Cities_of_Mosaic_Isle_PublicInterfaces.Helper;
using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFResource : IFMOID, IFEncyclopediaObject, IFNamedDebugObject
    {
        //an in-game resource is: a type of usable item, (usually) stored and consumed by the community.  They are created through buildings which work the land or transform other resources,
        // or by trade; they are consumed by trade, pops directly, or as fuel to create other resources.
        //for ease of use, we implement "intermediate" resources: resources that the community does not store or consume directly, but instead breakdown into other resources immediately or act as sources
        // for production.

        //GUARANTEE SECTION:
        //here we list the guarantees that a derived class must meet as a function of its existence as an IFResource:
        //A) This class represents a moddable object; therefore, by the time any script can access members of this class, the return values of all functions will only depend on the function's inputs.
        //  i) as a moddable object, this class has a tag list.  getTagList() will return a list of unique tags.  hasTag(string inTag) will return the same value as getTagList().Contains(inTag).  getTagList() will not return null, but the list may be empty.
        //B) getResourceQualities() will return an Int64 that is a logical OR of the values in eResourceQualities that are true for this resource.
        //  i) the bool accessors will return the same value as calling getResourceQualities() and checking if the bit is 1.
        //C) getProteinRatio() will return a non-negative number.
        //D) getDecaySpeed() will return a non-infinite, non-NaN number.  If isImmediateBreakdown() would return true, getDecaySpeed() will return 0.0d
        //E) getDesolationFactor() will return a non-infinite, non-NaN number.
        //F) this resource will not be included in getBreakdownResources(), nor will loops exist between resources.
        //G) getBreakdownResources() will only have positive, non-infinite, non-NaN numbers.
        //H) getBreakdownResources() will not return a null object if there are no breakdown resources, but instead will return a list with no elements.
        //J) a single resource will appear at most once in getBreakdownResources()
        //K) getProductionSpeed() will return a non-negative, non-infinite, non-NaN number

        //for this MO class, establish consts and enums:
        public const Int64 cResourceQualityMask = 0x7FF;
        public enum eResourceQualities //there is hardcoded C# which uses these, and therefore these should stay like this instead of as tags.  Tags should be for scripts only (ideally); enums for C# code.
        {
            cNone = 0x0,
            cFood = 0x1,
            cHStasis = 0x2,
            cWoundHeal = 0x4,
            cDiseaseHeal = 0x8,
            cDrug = 0x10,
            cMilEquip = 0x20,
            cOnlyOne = 0x40,
            cImmediateBreakdown = 0x80,
            cTrade = 0x100,
            cNotInResourcePool = 0x200,
            cEnableManualBreakdown = 0x400, //this is manual breakdown; won't need to do for immediate breakdown
        }

        public bool hasTag(string inTag);
        public ReadOnlyCollection<string> getTagList();

        //accessors for qualities:
        public Int64 getResourceQualities();
        public bool isFood();
        public bool isHStasis();
        public bool isWoundHeal();
        public bool isDiseaseHeal();
        public bool isDrug();
        public bool isMilEquip();
        public bool isOnlyOne();
        public bool isImmediateBreakdown();
        public bool isTrade();
        public bool isNotInResourcePool();
        public bool isEnableManualBreakdown();

        public Int64 getProteinRatio();
        public double getDecaySpeed(); //this is proportion of the whole quality range
        public ReadOnlyCollection<Tuple<IFResource, double>> getBreakdownResources();

        public double getDesolationFactor();

        public double getProductionSpeed();
    }
}
