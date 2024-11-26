using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.Helper
{
    //an encyclopedia object is any object which, in addition to its other responsibilities, has an entry in the Mosaic Encyclopedia.
    public interface IFEncyclopediaObject
    {
        public Tuple<string, string, string, ReadOnlyCollection<string>> getEncyclopediaEntry();
    }
}
