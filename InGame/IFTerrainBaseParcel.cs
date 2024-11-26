using Cities_of_Mosaic_Isle_PublicInterfaces.Helper;
using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFTerrainBaseParcel : IFMOID, IFMapTileObject, IFNamedDebugObject
    {
        //this class represents an NxM cell of base terrain.  A base parcel is necessarily biome-specific.
        //the dimensions of a base parcel cell have no gameplay effect, but different cells are used to tile the map, which hopefully provides visual variation.

        //GUARANTEE SECTION:
        //here we list the guarantees that a derived class must meet as a function of its existence as an IFTerrainBaseParcel:
        //A) This class represents a moddable object; therefore, by the time any script can access members of this class, the return values of all functions will only depend on the function's inputs.
        //  i) as a moddable object, this class has a tag list.  getTagList() will return a list of unique tags.  hasTag(string inTag) will return the same value as getTagList().Contains(inTag).  getTagList() will not return null, but the list may be empty.
        //B) getBaseParcelQualities() will return an Int64 that is a logical OR of the values in eBaseParcelQualities that are true for this base parcel.
        //  i) the bool accessors will return the same value as calling getResourceQualities() and checking if the bit is 1.
        //C) getBiome() will not return null

        //for this MO class, establish consts and enums:
        public const int cBaseParcelQualitiesMask = 0x7;
        public enum eBaseParcelQualities
        {
             cNone = 0x0
            ,cBuildable = 0x1
            ,cPassable = 0x2
            ,cUnderwater = 0x4
        }

        public bool hasTag(string inTag);
        public ReadOnlyCollection<string> getTagList();

        public Int64 getBaseParcelQualities();
        public bool isBuildable(); //while I'm not including any non-buildable base terrain, who knows, maybe a modder will find a use for the concept
        public bool isPassable(); //while I'm not including any non-passable base terrain, who knows, maybe a modder will find a use for the concept
        public bool isUnderwater();

        public IFTerrainBiome getBiome();
    }
}
