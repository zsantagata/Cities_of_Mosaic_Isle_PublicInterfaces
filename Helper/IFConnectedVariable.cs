using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.Helper
{
    public interface IFConnectedVariable
    {
        //note to modders/users: declaring classes and creating new objects that derive from this interface is not supported.
        //There are back-end handlers that will fail to properly deal with such classes and objects.  My best guess is that they will cause an infinite loop and hang forever, but w/e happens it's on you.
        //Use already-existing IFConnectedVariables provided by the variable handler.  They'll have the appropriate "secret sauce" :)

        public enum eCombinationType
        {
             cAdd = 0
            ,cMult = 1
            ,cAND = 2
            ,cOR = 3
            ,cLIST = 4 //LIST is a special type.  The main INT will be the count of listed items underneath.
        }

        public string getInternalName(); //note that this is lowercase-only
        public string getDebugName(); //(just in case) this will return the same as getInternalName
        public string debug();

        public ReadOnlyCollection<IFConnectedVariable> getLowers();
        public Int64 getExpirationDate();
        public void setExpirationDate(Int64 inExpirationDate);
        public APIconsts.eVariableKind getVariableKind();

        public Int64 getVariableAsInt();
        public double getVariableAsDouble();
        public string getVariableAsString();

        public IFConnectedVariable.eCombinationType getCombinationType();

        //to connect variables, scripts must call the appropriate variableHandler function

        public bool setVariable(object inNewVariable);
    }
}
