using Cities_of_Mosaic_Isle_PublicInterfaces.Helper;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFEventReport : IFUID, IFNamedDebugObject
    {
        public void setReportTextOverride(string inOverrideText);
        public void setResolutionTextOverride(string inOverrideText);

        public Int64 getDateEventReportGenerated();
    }
}
