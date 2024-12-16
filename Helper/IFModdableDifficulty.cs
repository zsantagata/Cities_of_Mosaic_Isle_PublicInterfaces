using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.Helper
{
    public interface IFModdableDifficulty
    {
        //GUARANTEE SECTION:
        //here we list the guarantees that a derived class must meet as a function of its existence as an IFModdableDifficulty:
        //A) getDifficultyMin() will return a non-negative value less than 1.0d
        //B) getDifficultyMax() will return a value greater than 1.0d
        //C) getDifficultyInternalNames() will return a collection with unique elements, and can return an empty collection but will not return null
        //D) getDifficultyPresetNamesAndValues() will return a non-empty collection.  The strings in this collection will be unique, lowercase, and will not be empty.  The doubles in this collection will be between getDifficultyMin() and getDifficultyMax() inclusive.  The collection will be ordered by the doubles (increasing).
        //E) getDifficultyPresetMilTimeMultFactor() will return a positive value

        public double getDifficultyMin();
        public double getDifficultyMax();

        public ReadOnlyCollection<string> getDifficultyInternalNames(); //all lower case
        public ReadOnlyCollection<Tuple<string, double>> getDifficultyPresetNamesAndValues();
        public double getDifficultyPresetMilTimeMultFactor();
    }
}
