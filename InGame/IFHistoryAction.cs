using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFHistoryAction
    {
        public string getActionText();

        //these adjectives serve as tags
        public string getMajorAdjective();
        public ReadOnlyCollection<string> getMinorAdjectives(); //this will not return null, but may return an empty collection

        //these two functions are the exact same, just duplicated for naming.  They return true if inTag is found within the minor adjectives or is the major adjective.  Not case specific.
        public bool hasTag(string inTag);
        public bool hasAdjective(string inTag);
    }
}
