using Cities_of_Mosaic_Isle_PublicInterfaces.Helper;
using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFTerrainDoodad : IFMOID, IFMapTileObject, IFNamedDebugObject
    {
        //an in-game doodad is: a type of terrain object that lays on the map above the base terrain
        //note that IFTerrainResourceParcel inherits from this interface.  ResourceParcels are special doodads in that they provide resources (by virtue of having an associated IFTerrainSource).
        //IFTerrainDoodads which are NOT IFTerrainResourceParcels are simply objects which block buildings, block passage entirely, or just look cool.

        //also note that the IFTerrainDoodad guarantees are pretty much the same as the IFTerrainBaseParcel guarantees

        //GUARANTEE SECTION:
        //here we list the guarantees that a derived class must meet as a function of its existence as an IFTerrainBaseParcel:
        //A) This class represents a moddable object; therefore, by the time any script can access members of this class, the return values of all functions will only depend on the function's inputs.
        //  i) as a moddable object, this class has a tag list.  getTagList() will return a list of unique tags.  hasTag(string inTag) will return the same value as getTagList().Contains(inTag).  getTagList() will not return null, but the list may be empty.
        //B) getDoodadQualities() will return an Int64 that is a logical OR of the values in eDoodadQualities that are true for this doodad.
        //  i) the bool accessors will return the same value as calling getDoodadQualities() and checking if the bit is 1.
        //  ii) an unpassable doodad will always be unbuildable as well
        //C) getBiomes will not return null, but may return an empty list
        //D) isBiomeEnabledForDoodad will return the same value as getBiomes().Contains(inBiome)

        //for this MO class, establish consts and enums:
        public const int cDoodadQualitiesMask = 0xF;
        public enum eDoodadQualities
        {
             cNone = 0x0
            ,cBuildable = 0x1
            ,cPassable = 0x2
            ,cUnderwaterEnable = 0x4
            ,cOverwaterEnable = 0x8
        }

        public bool hasTag(string inTag);
        public ReadOnlyCollection<string> getTagList();

        public Int64 getDoodadQualities();
        public bool isBuildable();
        public bool isPassable();
        public bool isUnderwaterEnable();
        public bool isOverwaterEnable();

        public ReadOnlyCollection<IFTerrainBiome> getBiomes();
        public bool isBiomeEnabledForDoodad(IFTerrainBiome inBiome);
    }
}
