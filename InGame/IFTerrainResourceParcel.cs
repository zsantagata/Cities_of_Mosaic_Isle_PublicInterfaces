namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFTerrainResourceParcel : IFTerrainDoodad
    {
        //IFTerrainResourceParcel is described in the comments of IFTerrainDoodad.cs
        //the only additional guarantee is:
        //getTerrainSource() will not return null

        public IFTerrainSource getTerrainSource();
    }
}
