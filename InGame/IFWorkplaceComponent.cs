using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFWorkplaceComponent
    {
        //a "workplace component" is a component of a building.  Namely, it is a C# class which stores the member variables relating to, and performs the functions relating to,
        //  a building's nature as a workplace.

        //consts and enums:
        public const string cWorkplaceInputOnDescriptionTranslatedStringName = "workplace_state_on_text";
        public const string cWorkplaceInputOffDescriptionTranslatedStringName = "workplace_state_off_text";

        public bool isWorkplaceOn(); //this is determined by workplace scripts (predictive or not). note to self this cannot be cached because we don't know what scripts will do

        public ReadOnlyCollection<IFResource> getInputsChosen(); //will not return null but may return an empty collection
        public ReadOnlyCollection<IFResource> getAdditionalInputsNotFromChosen(); //some buildingKinds have resources they simply must consume to do work.  some input resource selections have additional resources that must be consumed.  This is the list of all of those for the currently chosen inputs

        //IFIndustrySkills holds the list of buildings led by the leader; it is not stored here

        public UInt16 getWorkerLimit();
        public void setWorkerLimit(UInt16 inNewWorkerLimit); //capped by IFBuildingKind max workers.  Does nothing if trying to set below current worker count.

        public IFResource getOutputResourceChosen();
        public void setOutputResourceChosen(IFResource inResourceChosen); //only does something if inResourceChosen is a valid output resource for this workplace.  If the building has a script output, using inResourceChosen = null chooses the script output.

        public double getResourceQualityDesiredFromQualQuanDial();

        public void toggleWorkplaceInputChosen(IFResource inResource);

        public ReadOnlyCollection<IFPop> getWorkerPops();
        public void removePopFromWorkers(IFPop inPop); //this will properly arrange the IFPop's internal member variables as well
        public void addPopToWorkers(IFPop inPop); //this will properly arrange the IFPop's internal member variables as well
        public void fireAllWorkerPops();
    }
}
