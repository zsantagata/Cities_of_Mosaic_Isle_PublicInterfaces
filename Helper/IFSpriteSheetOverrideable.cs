namespace Cities_of_Mosaic_Isle_PublicInterfaces.Helper
{
    public interface IFSpriteSheetOverrideable
    {
        public bool spriteSheetIsOverridden();
        public string getOverrideSpriteSheetID(); //if the sprite sheet is not overridden, this will return an empty string
        public bool setOverrideSpriteSheetID(string inOverrideSpriteSheetID); //returns true if the sprite sheet exists for this in-game type, false otherwise.
        public void resetOverrideSpriteSheetID();
    }
}
