using Cities_of_Mosaic_Isle_PublicInterfaces.Helper;
using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFMilStrengthCalculation : IFMOID, IFNamedDebugObject
    {
        //an IFMilStrengthCalculation is a way that a foreign community's military capabilities are measured.  Specifically, it calculates how much military capability the foreign leader can bring to bear.
        //because different communities can have different ways of fighting, different pressures, and different leadership, mil strength can be calculated differently between communities.
        //each foreign AI will have one, and only one, IFMilStrengthCalculation.  Multiple foreign AI can share the same IFMilStrengthCalculation (that would imply their ways of fighting are similar).

        //GUARANTEE SECTION:
        //here we list the guarantees that a derived class must meet as a function of its existence as an IFMilStrengthCalculation:
        //A) This class represents a moddable object; therefore, by the time any script can access members of this class, the return values of all functions will only depend on the function's inputs.
        //  i) as a moddable object, this class has a tag list.  getTagList() will return a list of unique tags.  hasTag(string inTag) will return the same value as getTagList().Contains(inTag).  getTagList() will not return null, but the list may be empty.
        //B) if isPlayerOnly() is false, isPlayerAllowedRandomChoice() will also be false

        public enum ePlayerMilStrengthCalcQualities
        {
             cNone = 0x0
            ,cPlayerOnly = 0x1 //if this is true, this mil strength calculation cannot belong to any igForeignAI, and cannot be assigned to foreign communities.  If this is false, this mil strength calculation cannot be assigned to the local community.
            ,cAllowedRandomChoice = 0x2 //TODO (not really here): When a new player community is settled, the previously-player community is assigned a random econ strength calc from those that are possible for non-players.
        }

        public bool hasTag(string inTag);
        public ReadOnlyCollection<string> getTagList();

        public bool isPlayerOnly();
        public bool isPlayerAllowedRandomChoice();
    }
}
