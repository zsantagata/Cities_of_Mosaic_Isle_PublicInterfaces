using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFHistoryEffect
    {
        //every impact of an effect has a strength.  The "major" impact is just cost/benefit, with cost negative and benefit positive.
        //other impact ("minor" impact) are whatever they wish, with negative meaning opposite of the word and positive meaning the word.  Minor impact cannot have a value of 0.

        public const int cMaxImpact = 100;
        public const int cMinImpact = -100;

        public int getCostBenefitImpact();
        public ReadOnlyDictionary<string, int> getOtherImpacts();

        //these two functions are the exact same, just duplicated for naming.  They return true if inTag is found within the minor impacts.  Not case specific.
        public bool hasTag(string inTag);
        public bool hasImpact(string inTag);
        //these two functions are the exact same, just duplicated for naming.  They return a value if inTag exists in the minor impacts, and return 0 otherwise.
        public int getTagStrength(string inTag);
        public int getImpactStrength(string inTag);
    }
}
