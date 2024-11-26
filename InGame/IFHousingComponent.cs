using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFHousingComponent
    {
        //a "housing component" is a component of a building.  Namely, it is a C# class which stores the member variables relating to, and performs the functions relating to,
        //  a building's nature as a house.

        public ReadOnlyCollection<IFPop> getHousedPops();
        public void removePopFromHousing(IFPop inPop); //this will properly arrange the IFPop's internal member variables as well
        public bool addPopToHousing(IFPop inPop); //this will properly arrange the IFPop's internal member variables as well

        public double getHousingQuality();
        public void setHousingQuality(double inHousingQuality); //limited by max and min quality (like a resource)
        public double calculateHousingQualityForPop(IFPop inPop);

        public double getBeautySurrounding();
        public void setBeautySurrounding(double inBeautySurrounding);
    }
}
