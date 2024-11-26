using Cities_of_Mosaic_Isle_PublicInterfaces.Helper;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFIndustrySkills : IFUID, IFNamedDebugObject
    {
        //this class holds the industry skill of leaders and general pops in a domestic community.  It also holds the buildings that Industry Leaders are assigned to direct.
        //It does not, however, hold any record of who Industry Leaders may be.
        //this class also holds cached (calculated at midnight) values for the average military skill by race, and the average community military skill.
        //this class is not 1:1 with a community like IFLeaderCollection because it does not need to exist for foreign-only communities, and therefore needs to be an IFUID

        public double getLeaderSkill(IFIndustry inIndustry);
        public void setLeaderSkill(double inNewLeaderLevel, IFIndustry inIndustry);
        public double getAverageSkill(IFIndustry inIndustry);
        public void setAverageSkill(double inNewAvgLevel, IFIndustry inIndustry);
        public double getRaceMilitarySkill(IFRace inRace);
        public double getCommunityMilitarySkill();

        public IFBuilding getBuildingBeingDirected(IFIndustry inIndustry);
        public void setBuildingBeingDirected(IFBuilding inBuilding, IFIndustry inIndustry); //industry needs to be provided since inBuilding can be null. Also inIndustry must match inBuilding
    }
}
