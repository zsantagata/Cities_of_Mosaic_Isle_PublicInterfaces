namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFHappinessCause
    {
        //All happiness items have a name, description, current value, start date, and bool that indicates "permanent" (and a hidden-to-modder bool that indicates "base").
        //Non-base happiness items are independent of one another, but when displayed, if they share a name they are displayed together (with their descriptions independent if they are not the same, and descriptions together if they are -- but current quantity is always displayed combined for the same name, even though it's not stored that way).

        public string getDisplayName();
        public string getDescription();

        public double getCurrentValue();
        public void setCurrentValue(double inValue);
        public Int64 getStartDate();

        public bool isPermanent(); //this flag is used to indicate that the happiness cause should not be fed into the script that decreases happiness quantities
        public void setPermanent(bool inPermanent);
    }
}
