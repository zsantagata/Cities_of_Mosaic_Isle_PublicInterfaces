using Cities_of_Mosaic_Isle_PublicInterfaces.Helper;
using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFEconStrengthCalculation : IFMOID, IFNamedDebugObject
    {
        //an IFEconStrengthCalculation is a way that a foreign community's economic capabilities are measured.  Specifically, it calculates how much economic capability the foreign leader can bring to bear.
        //because different communities can have different ways of living, different pressures, and different leadership, econ strength can be calculated differently between communities.
        //each foreign AI will have one, and only one, IFEconStrengthCalculation.  Multiple foreign AI can share the same IFEconStrengthCalculation (that would imply their ways of living are similar).

        //GUARANTEE SECTION:
        //here we list the guarantees that a derived class must meet as a function of its existence as an IFEconStrengthCalculation:
        //A) This class represents a moddable object; therefore, by the time any script can access members of this class, the return values of all functions will only depend on the function's inputs.
        //  i) as a moddable object, this class has a tag list.  getTagList() will return a list of unique tags.  hasTag(string inTag) will return the same value as getTagList().Contains(inTag).  getTagList() will not return null, but the list may be empty.
        //B) getEconomicCapitalKindInternalNames() will return a unique collection of strings, all lower case.  None of the values will be the empty string, but the collection may have no entries.
        //C) if getEconomicCapitalKindDisplayName() is provided an input which is a value in getEconomicCapitalKindInternalNames(), it will return a non-empty string.  Otherwise, it will return an empty string.
        //D) if inInputs for runWarDelegationAttackCapitalKindScript is size 0, or if the first value is not a string which exists in getEconomicCapitalKindInternalNames, it will not run a script and the return value will be null
        //E) if isPlayerOnly() is false, isPlayerAllowedRandomChoice() will also be false
        
        public enum ePlayerEconStrengthCalcQualities
        {
             cNone = 0x0
            ,cPlayerOnly = 0x1 //if this is true, this econ strength calculation cannot belong to any igForeignAI, and cannot be assigned to foreign communities.  If this is false, this econ strength calculation cannot be assigned to the local community.
            ,cAllowedRandomChoice = 0x2 //TODO (not really here): When a new player community is settled, the previously-player community is assigned a random econ strength calc from those that are possible for non-players.
        }

        public bool hasTag(string inTag);
        public ReadOnlyCollection<string> getTagList();

        //different communities can make different investment tools (buildings, actual tools, irrigation, etc.) that can be targeted by war delegations.  This is a list of what war delegations can target
        //note that these are the internal names, not the display names (they are different for translation purposes).  these internal names are all lower case.
        public ReadOnlyCollection<string> getEconomicCapitalKindInternalNames();
        public string getEconomicCapitalKindDisplayName(string inEconomicCapitalKindInternalName);

        public bool isPlayerOnly();
        public bool isPlayerAllowedRandomChoice();
    }
}
