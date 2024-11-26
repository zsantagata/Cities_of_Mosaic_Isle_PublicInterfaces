namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFHistoryValueChart
    {
        public bool isIntValues();
        public bool isDoubleValues();

        public Int64 getIntValueAtCalendarDate(Int64 inCalendarDate, out bool outSuccess);
        public double getDoubleValueAtCalendarDate(Int64 inCalendarDate, out bool outSuccess);
        public Int64 getIntValueCalendarDaysAgo(Int64 inCalendarDaysAgo, out bool outSuccess);
        public double getDoubleValueCalendarDaysAgo(Int64 inCalendarDaysAgo, out bool outSuccess);
    }
}
