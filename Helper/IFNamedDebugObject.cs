namespace Cities_of_Mosaic_Isle_PublicInterfaces.Helper
{
    public interface IFNamedDebugObject
    {
        //GUARANTEE SECTION:
        //here we list the guarantees that a derived class must meet as a function of its existence as an IFDebugObject:
        //A) a derived must return a non-null value for the function below
        //B) a derived must throw no exceptions and cause no errors to be reported as a result of the function below
        public string getDebugName();
    }
}
