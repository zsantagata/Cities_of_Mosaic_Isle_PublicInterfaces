using Cities_of_Mosaic_Isle_PublicInterfaces.Helper;
using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    //an in-game biome is: the type of land upon which a community has settled.  It is chosen either rigidly by the scenario, or by the player via the use of a map in sandbox mode.

    //GUARANTEE SECTION:
    //here we list the guarantees that a derived class must meet as a function of its existence as an IFIndustry:
    //A) This class represents a moddable object; therefore, by the time any script can access members of this class, the return values of all functions will only depend on the function's inputs.
    //  i) as a moddable object, this class has a tag list.  getTagList() will return a list of unique tags.  hasTag(string inTag) will return the same value as getTagList().Contains(inTag).  getTagList() will not return null, but the list may be empty.
    //B) getTerrainSourceProbability will return a value between 0.0d and 1.0d inclusive.
    //C) getBiomeQualityDecayStrength will return a non-negative number
    //D) getLocalMoveSpeedFactor and getWorldMapMoveSpeedFactor will each return a positive number

    public interface IFTerrainBiome : IFMOID, IFEncyclopediaObject, IFNamedDebugObject
    {
        //for this MO class, establish consts and enums:
        //this is not used directly within IFTerrainBiome but in random map gen
        public const UInt16 cTerrainSourceRichnessCount = 10;
        public enum eTerrainSourceRichness
        {
            cNone = 0x0,
            cSoloTiles = 0x1,
            cBitsAndDots = 0x2,
            cThinDregs = 0x3,
            cSmallPocket = 0x4,
            cPocket = 0x5, //around here and less rich than this, the names are less like a guarantee of this for a map and more like an average
            cHealthy = 0x6,
            cMultipleOrBigPockets = 0x7,
            cManyPockets = 0x8,
            cShouldCoverTheMap = 0x9 //"cover" means roughly 50% of the map
        }

        public bool hasTag(string inTag);
        public ReadOnlyCollection<string> getTagList();

        public double getBiomeQualityDecayStrength();

        public double getLocalMoveSpeedFactor();
        public double getWorldMapMoveSpeedFactor();

        public double getWaterAvailability(); //note that this is independent from isWater
        public bool isWater();

        public IFTerrainBiome.eTerrainSourceRichness getTerrainSourceRichness(IFTerrainSource inTerrainSource);
        public double getTerrainSourceProbability(IFTerrainSource inTerrainSource);
    }
}
