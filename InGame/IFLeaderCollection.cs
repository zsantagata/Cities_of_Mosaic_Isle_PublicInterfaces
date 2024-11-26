using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFLeaderCollection
    {
        //this class is a sub-component of an IFCommunity: this class holds which pops are leaders, and of what
        //for a domestic community, all parts are enabled.  For a foreign community, there are no industry leaders.
        //for a community which has neither domestic nor foreign components, a City Leader may exist (such as The Lady), or one may not

        //note that a City Leader may also be a Race Leader, but the City Leader and Race Leaders may not be Industry Leaders

        [Flags]
        public enum eLeaderFlags
        {
            cNone = 0,
            cCityLeader = 0x1,
            cRaceLeader = 0x2,
            cIndustryLeader = 0x4
        }

        public IFLeaderCollection.eLeaderFlags getLeaderFlagsForPop(IFPop inPop);
        public IFIndustry getIndustryLedByPop(IFPop inPop); //this will return null if the pop is not an industry leader

        public bool hasCityLeader();  //this returns (getCityLeader() != null)
        public IFPop getCityLeader(); //this may return null
        public void setCityLeader(IFPop inPop);

        public ReadOnlyCollection<IFPop> getRaceLeaders(); //note that this collection will not include 'null' but may be empty
        public bool hasRaceLeader(IFRace inRace);  //this returns (getRaceLeader(inRace) != null)
        public IFPop getRaceLeader(IFRace inRace); //this may return null
        public void setRaceLeader(IFPop inPop, IFRace inRace = null); //if inPop is null, we are clearing the race leader of inRace (and there is no race leader).  Otherwise inPop's race is used, and inRace is ignored.

        public ReadOnlyCollection<IFPop> getIndustryLeaders(); //note that this collection will not include 'null', but may be empty
        public bool hasIndustryLeader(IFIndustry inIndustry);  //this returns (getIndustryLeader(inIndustry) != null)
        public IFPop getIndustryLeader(IFIndustry inIndustry); //this may return null
        public void setIndustryLeader(IFPop inPop, IFIndustry inIndustry); //if inPop is null, we are clearing the industry leader of inIndustry (and there is no industry leader).  inIndustry should not be null.
    }
}
