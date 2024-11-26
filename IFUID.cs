namespace Cities_of_Mosaic_Isle_PublicInterfaces
{
    public interface IFUID
    {
        //all this interface indicates is that inheritors will provide a UID, and has a name
        public string getDisplayName();

        public UInt64 getUID();
    }
}
