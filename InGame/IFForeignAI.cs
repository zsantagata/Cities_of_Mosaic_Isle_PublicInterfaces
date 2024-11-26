using Cities_of_Mosaic_Isle_PublicInterfaces.Helper;
using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFForeignAI : IFMOID, IFNamedDebugObject
    {
        //an IFForeignAI is a way that a foreign community's leader makes decisions.  These decisions not only determine econ strength and mil strength, but can influence:
        //  if and when foreign-sourced delegations are sent, the goal and resource pool and pop count of foreign-sourced delegations, arrival events of player-sourced delegations sent to this community, and possibly while-traveling player-sourced delegations
        //because different leaders can make decisions in different ways, 
        //each foreign AI will have one, and only one, IFMilStrengthCalculation.  Multiple foreign AI can share the same IFMilStrengthCalculation (that would imply their ways of fighting are similar).

        //GUARANTEE SECTION:
        //here we list the guarantees that a derived class must meet as a function of its existence as an IFMilStrengthCalculation:
        //A) This class represents a moddable object; therefore, by the time any script can access members of this class, the return values of all functions will only depend on the function's inputs.
        //  i) as a moddable object, this class has a tag list.  getTagList() will return a list of unique tags.  hasTag(string inTag) will return the same value as getTagList().Contains(inTag).  getTagList() will not return null, but the list may be empty.
        //B) getWeight() will return a non-negative value
        //C) getDefaultEconStrengthCalculation() will not be null
        //D) getDefaultMilStrengthCalculation() will not be null

        public bool hasTag(string inTag);
        public ReadOnlyCollection<string> getTagList();

        public double getWeight();
        public bool getGeneralEnable(); //if this is false, then a foreign community can only have this IFForeignAI directly assigned, rather than randomly chosen

        //these two are constant for a foreign AI (but multiple foreign AIs can share them):
        public IFEconStrengthCalculation getDefaultEconStrengthCalculation();
        public IFMilStrengthCalculation getDefaultMilStrengthCalculation();
    }
}
