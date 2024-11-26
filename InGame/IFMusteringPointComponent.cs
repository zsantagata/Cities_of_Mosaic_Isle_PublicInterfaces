using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFMusteringPointComponent
    {
        //a "mustering point component" is a component of a building.  Namely, it is a C# class which stores the member variables relating to, and performs the functions relating to,
        //  a building's nature as a mustering point.

        public const int cMusteringEquipmentCommandCount = 5;
        public enum eMusteringEquipmentCommand
        {
             cKeep = 0 //don't exchange equipment
            ,cRemove = 1 //remove all existing equipment and don't pick up any
            ,cEquip = 2 //equip all available equipment, including exchanging held equipment
            ,cBest = 3 //exchange any equipment (including nothing at all) that is worse than the quality held by the resource pool
            ,cUpgrade = 4 //exchange any existing equipment (NOT including nothing at all) that is worse than the quality held by the resource pool
        }
        public static readonly string[] cMusteringEquipmentCommandNames = //these are the translatedStrings names
        {
            "mustering_point_command_keep_name",
            "mustering_point_command_remove_name",
            "mustering_point_command_equip_name",
            "mustering_point_command_best_name",
            "mustering_point_command_upgrade_name"
        };
        public static readonly string[] cMusteringEquipmentDescriptions = //these are the translatedStrings names
        {
            "mustering_point_command_keep_description",
            "mustering_point_command_remove_description",
            "mustering_point_command_equip_description",
            "mustering_point_command_best_description",
            "mustering_point_command_upgrade_description"
        };

        public eMusteringEquipmentCommand getMusteringEquipmentCommand();
        public void setMusteringEquipmentCommand(eMusteringEquipmentCommand inMusteringEquipmentCommand);

        public ReadOnlyCollection<IFPop> getAssignedPops();
        public void removePopFromMusteringPointAssignment(IFPop inPop);
        public bool addPopToMusteringPointAssignment(IFPop inPop);
    }
}
