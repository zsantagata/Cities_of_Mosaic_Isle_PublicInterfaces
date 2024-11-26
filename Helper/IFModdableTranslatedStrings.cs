namespace Cities_of_Mosaic_Isle_PublicInterfaces.Helper
{
    public interface IFModdableTranslatedStrings
    {
        //GUARANTEE SECTION:
        //here we list the guarantees that a derived class must meet as a function of its existence as an IFModdableCustomConsts:
        //A) This class represents a moddable object; therefore, by the time any script can access members of this class, the return values of all functions will only depend on the function's inputs.
        //B) the input of getDisplayStringOfName will be treated as case insensitive
        //C) if the input of getDisplayStringOfName does not relate to anything in the translation file, the same value as getMissingDisplayString will be returned

        public string getDisplayStringOfName(string inName);
        public string getMissingDisplayString();
    }
}