using Cities_of_Mosaic_Isle_PublicInterfaces.Helper;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFMapTile
    {
        //there are three different sprite sheets which may be overridden for a tile:
        public const int cSpriteSheetsOfMapTileCount = 3;
        public enum eSpriteSheetsOfMapTile
        {
            cBaseParcel = 0,
            cResourceParcel = 1,
            cDoodad = 2
            //to override a building's spritesheet use the IFBuilding
        }

        public string getInternalName();

        public IFTerrainBaseParcel getBaseTerrain();
        public IFMapTileObject.eMapItemOrientation getBaseTerrainOrientation();
        public bool hasBaseTerrain();
        public bool isBaseTerrainRoot();
        public void setBaseTerrain(IFTerrainBaseParcel inBaseTerrain, bool inRoot, IFMapTileObject.eMapItemOrientation inOrientation);

        public IFTerrainResourceParcel getResourceParcel();
        public IFMapTileObject.eMapItemOrientation getResourceParcelOrientation();
        public bool hasResourceParcel();
        public bool isResourceParcelRoot();
        public void setResourceParcel(IFTerrainResourceParcel inResourceParcel, bool inRoot, IFMapTileObject.eMapItemOrientation inOrientation);

        public IFTerrainDoodad getDoodad();
        public IFMapTileObject.eMapItemOrientation getDoodadOrientation();
        public bool hasDoodad();
        public bool isDoodadRoot();
        public void setDoodad(IFTerrainDoodad inDoodad, bool inRoot, IFMapTileObject.eMapItemOrientation inOrientation);

        public IFBuilding getBuilding();
        public IFMapTileObject.eMapItemOrientation getBuildingOrientation();
        public bool hasBuilding();
        public bool isBuildingRoot();
        public void setBuilding(IFBuilding inBuilding, bool inRoot, IFMapTileObject.eMapItemOrientation inOrientation);


        public bool isPassable();
        public bool isUnderwater();
        public bool isBuildable();

        public double getDesolation();
        public void setDesolation(double inNewDesolation);

        public bool spriteSheetIsOverridden(IFMapTile.eSpriteSheetsOfMapTile inSSInQuestion);
        public string getOverrideSpriteSheetID(IFMapTile.eSpriteSheetsOfMapTile inSSInQuestion);
        public bool setOverrideSpriteSheetID(IFMapTile.eSpriteSheetsOfMapTile inSSInQuestion, string inOverrideSpriteSheetID);
        public void resetOverrideSpriteSheetID(IFMapTile.eSpriteSheetsOfMapTile inSSInQuestion);
    }
}
