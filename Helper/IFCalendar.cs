namespace Cities_of_Mosaic_Isle_PublicInterfaces.Helper
{
    public interface IFCalendar
    {
        public struct calendarDate
        {
            public Int64 mYear;
            public Int64 mSeason;
            public Int64 mMonth;
            public Int64 mDay;
        };

        public const int cDayOffset = 1;

        public Int64 getCurrentDate();
        public Int64 getStartDate();

        //all the following functions are just convenient functions for others to call; they should return the same information as the above two functions
        public Int64 getCurrentYear();
        public Int64 getCurrentSeason();
        public Int64 getCurrentSeasonInt();
        public Int64 getCurrentMonth();
        public Int64 getCurrentMonthInt();
        public Int64 getCurrentDay();

        public Int64 getStartYear();
        public Int64 getStartSeason();
        public Int64 getStartSeasonInt();
        public Int64 getStartMonth();
        public Int64 getStartMonthInt();
        public Int64 getStartDay();

        //modify functions:
        public void incrementDay();
        public void advanceXDays(Int64 inDays);

        //text display function:
        public string getFormattedDate(Int64 inDate);
    }
}
