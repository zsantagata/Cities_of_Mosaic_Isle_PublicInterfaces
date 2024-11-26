namespace Cities_of_Mosaic_Isle_PublicInterfaces.Helper
{
    public interface IFSaveableDifficulty
    {
        //GUARANTEE SECTION:
        //here we list the guarantees that a derived class must meet as a function of its existence as an IFSaveableDifficulty:
        //A) getCityEventFrequency() will return a value between IFModdableDifficulty's getDifficultyMin() and getDifficultyMax() inclusive
        //B) getTravelEventFrequency() will return a value between IFModdableDifficulty's getDifficultyMin() and getDifficultyMax() inclusive
        //C) getMilitaryTimeWalkSpeed() will return a value between IFModdableDifficulty's getDifficultyMin() and getDifficultyMax() inclusive
        //D) getDifficultyValueFromInternalName() will return a value between IFModdableDifficulty's getDifficultyMin() and getDifficultyMax() inclusive.  If the input name (capitalization ignored) does not exist in IFModdableDifficulty's getDifficultyInternalNames(), the return value will be 1.0d

        //these are taken care of in C# code and do not need to be checked by scripts, but are accessible anyway
        public double getCityEventFrequency();
        public double getTravelEventFrequency();
        public double getMilitaryTimeWalkSpeed();

        public double getDifficultyValueFromInternalName(string inDifficultyInternalName); //capitalization of the input is ignored

        //set functions are handled internally to the C# code, connected to the options menu
    }
}
