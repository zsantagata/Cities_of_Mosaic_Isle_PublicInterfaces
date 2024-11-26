using Cities_of_Mosaic_Isle_PublicInterfaces.Helper;
using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFMap : IFUID, IFNamedDebugObject
    {
        public IFTerrainBiome getLandBiome();
        public IFTerrainBiome getWaterBiome();

        public IFCommunity getCommunity();

        public ReadOnlyCollection<ReadOnlyCollection<IFMapTile>> getMapTiles();
        public Tuple<Int64, Int64> getMapDimensions();
        public ReadOnlyCollection<IFMapTile> getMapTilesInBuildingRadius(IFBuilding inBuilding, double inRadius = -1.0d);
        public ReadOnlyCollection<IFMapTile> getMapTilesInRadius(double radius, double inX, double inY, double inWidth, double inHeight);

        public bool isBuildingOnThisMap(IFBuilding inBuilding);
        public ReadOnlyCollection<IFBuilding> getAllBuildingsOfKindOnMap(IFBuildingKind inBuildingKind);
        public ReadOnlyCollection<IFBuilding> getAllBuildingsOnMap();
        public Tuple<double, double> getRandomEdgeOfBuilding(IFBuilding inBuilding);

        public Tuple<double, double> findClosestMapEdgeAccessibleLocationTo(double inX, double inY, bool inLandBreathing, bool inWaterBreathing); //this function returns an x,y value which can access at least one map edge for the breathability provided (both-breathability is assumed if both bools are false).  Will return 0,0 if something goes wrong.

        public double getWaterRatio();
    }
}
