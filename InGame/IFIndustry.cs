using Cities_of_Mosaic_Isle_PublicInterfaces.Helper;
using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFIndustry : IFMOID, IFEncyclopediaObject, IFNamedDebugObject
    {
        //an in-game industry is: a category of production of goods or services.
        //each industry is mutually exclusive with every other industry, and the sum of all industries is the sum of all goods and services created
        //all buildings which are staffed must belong to an industry
        //this class represents the abstract industries themselves

        //GUARANTEE SECTION:
        //here we list the guarantees that a derived class must meet as a function of its existence as an IFIndustry:
        //A) This class represents a moddable object; therefore, by the time any script can access members of this class, the return values of all functions will only depend on the function's inputs.
        //  i) as a moddable object, this class has a tag list.  getTagList() will return a list of unique tags.  hasTag(string inTag) will return the same value as getTagList().Contains(inTag).  getTagList() will not return null, but the list may be empty.
        //B) getIndustryQualities() will return an Int64 that is a logical OR of the values in eIndustryQualities that are true for this industry.
        //  i) isDistribution() and isConstruction() will not both return true.

        public const Int64 cIndustryQualitiesMask = 0x7;
        [Flags]
        public enum eIndustryQualities
        {
             cNoSkillImprovement = 0x1 //this is implemented by C# code in leader collection and industry skills
            ,cDistribution = 0x2
            ,cConstruction = 0x4
        }

        public bool hasTag(string inTag);
        public ReadOnlyCollection<string> getTagList();

        public Int64 getIndustryQualities();
        public bool isNoSkillImprovement();
        public bool isDistribution();
        public bool isConstruction();
    }
}
