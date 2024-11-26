namespace Cities_of_Mosaic_Isle_PublicInterfaces
{
    public interface IFMOID
    {
        //all this interface indicates is that inheritors will provide a MOID, and has a name.
        //the names will not change once assigned.  The MOID can change during mod loading, but will not change after moddable object references are resolved.  (So any user scripts won't see the MOID change.)
        //the reason we have an internal name, and a display name, is this: the display name is what is presented to the player in the UI, and therefore should be translatable.
        //The internal name is what is used by internal C# code/debug messages/modders to indicate a specific moddable object (when a MOID won't do).
        public string getInternalName();
        public string getDisplayName();

        public UInt32 getMOID();
    }
}
