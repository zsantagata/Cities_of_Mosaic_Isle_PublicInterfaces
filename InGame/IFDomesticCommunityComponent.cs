namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFDomesticCommunityComponent
    {
        //a "domestic community component" is a component of a community.  Namely, it is a C# class which stores the member variables relating to, and performs the functions relating to,
        //  a community's nature as a domestic community.
        //here, "domestic" means a community that the player has controlled at least once in the past, and is either controlling now or could theoretically control again in the future.

        public IFMap getMap();

        public IFHappinessCollection getHappinessCollection();
        public IFIndustrySkills getIndustrySkills();

        public IFHistoryValueStorage getHistoryValueStorage();

        public double getPredictionStrength();
        public void setPredictionStrength(double inPredictionStrength);
    }
}
