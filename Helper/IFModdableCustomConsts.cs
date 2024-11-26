using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.Helper
{
    public interface IFModdableCustomConsts
    {
        //GUARANTEE SECTION:
        //here we list the guarantees that a derived class must meet as a function of its existence as an IFModdableCustomConsts:
        //A) This class represents a moddable object; therefore, by the time any script can access members of this class, the return values of all functions will only depend on the function's inputs.
        //B) if getIntConst is called but the const does not exist, return value is 0L
        //C) if getStringConst is called but the const does not exist, return value is string.empty
        //D) if getDoubleConst is called but the const does not exist, return value is 0.0d
        //E) if getListConst is called but the const does not exist, return value is an empty collection
        //F) all functions will treat 'inName' independent of capitalization (internally all variables/consts are held as lowercase, and inName will be converted to all lowercase)


        public APIconsts.eCustomConstKind getConstKind(string inName);
        public Int64 getIntConst(string inName, out bool outSuccess);
        public string getStringConst(string inName, out bool outSuccess);
        public double getDoubleConst(string inName, out bool outSuccess);
        public ReadOnlyCollection<string> getListConst(string inName, out bool outSuccess);
    }
}
