namespace Cities_of_Mosaic_Isle_PublicInterfaces.Helper
{
    //an IFMapTileObject is any object which has integer width and height values relating to a portion of a community map.
    //while many IFMapTileObjects may have X and Y positions, not all do (for example, an igTerrainResourceParcel doesn't have an X and Y; it represents a pattern (parcel) of land sources which are shaped
    //in a common way, but this pattern can appear multiple times per map)
    public interface IFMapTileObject
    {
        public enum eMapItemOrientation //these numerical values need to stay the same for some calculations to work
        {
             cOriginal = 0  //things whose textures point north as "up" will only have original or FlipHoriz orientation
            ,cFlipHoriz = 1 //like 90 but not actually a rotation
            ,cFlipBoth = 2 //180
            ,cFlipVert = 3 //like 270 but not actually a rotation
        }
        //GUARANTEE SECTION:
        //here we list the guarantees that a derived class must meet as a function of its existence as an IFTerrainBaseParcel:
        //A) getHeight() and getWidth() will return positive integers
        //B) getDimensions() will return a Tuple whose Item1 is equal to getWidth(), and whose Item2 is equal to getHeight()
        //C) getLongSide() will return MAX(getWidth(), getHeight())
        //D) getShortSide() will return MIN(getWidth(), getHeight())

        public Int64 getWidth();
        public Int64 getHeight();

        public Tuple<Int64, Int64> getDimensions();
        public Int64 getLongSide();
        public Int64 getShortSide();
    }
}
