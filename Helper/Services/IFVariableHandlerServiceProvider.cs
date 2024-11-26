namespace Cities_of_Mosaic_Isle_PublicInterfaces.Helper.Services
{
    public interface IFVariableHandlerServiceProvider
    {
        //note to self regarding calendar date and expiration date:
        //right before midnight processing, the calendar date is incremented, THEN variables are cleared iff their expiration date is LESS THAN the current date.
        //in other words: if today is N, and the expiration date is N, then the variable will NOT be present when the next midnight processing begins.
        //in other words: midnight processing is the start of a new day

        public void cleanUpVariablesUpToDate(Int64 inCurrentDate); //note: connected variables with lowers remaining will not be cleaned up if any of those lowers have expiration dates later than inCurrentDate

        //connectedVariable functions:
        public bool connectedVariableExists(string inVariableName);
        public IFConnectedVariable getConnectedVariable(string inVariableName); //this can return null which is why the above function exists
        public Int64 getConnectedInt(string inVariableName, out bool outSuccess);
        public double getConnectedDouble(string inVariableName, out bool outSuccess);
        public string getConnectedString(string inVariableName, out bool outSuccess);
        public bool setConnectedVariableValue(string inVariableName, object inToSet, Int64 inNewExpirationDate = -1);
        public bool connectVariables(string inLowerVariable, string inUpperVariable);
        public bool connectVariables(IFConnectedVariable inLowerVariable, string inUpperVariable);
        public bool connectVariables(string inLowerVariable, IFConnectedVariable inUpperVariable);
        public bool connectVariables(IFConnectedVariable inLowerVariable, IFConnectedVariable inUpperVariable);
        public void clearConnectedVariable(string inVariableName);
        public void clearConnectedVariable(IFConnectedVariable inVariable);
        public bool addConnectedInt(string inVariableName, Int64 inStartValue, out IFConnectedVariable outResult, Int64 inExpirationDate = -1, IFConnectedVariable.eCombinationType inCombinationType = IFConnectedVariable.eCombinationType.cAdd);
        public bool addConnectedDouble(string inVariableName, double inStartValue, out IFConnectedVariable outResult, Int64 inExpirationDate = -1, IFConnectedVariable.eCombinationType inCombinationType = IFConnectedVariable.eCombinationType.cAdd);
        public bool addConnectedString(string inVariableName, string inStartValue, out IFConnectedVariable outResult, Int64 inExpirationDate = -1, IFConnectedVariable.eCombinationType inCombinationType = IFConnectedVariable.eCombinationType.cAdd);

        //normal variable functions:
        public void storeIntVariable(Int64 inClearTime, string inVariableName, Int64 inVariable);
        public void storeIntVariable(Int64 inClearTime, string inVariableName, UInt64 inVariable);
        public void storeStringVariable(Int64 inClearTime, string inVariableName, string inVariable);
        public void storeDoubleVariable(Int64 inClearTime, string inVariableName, double inVariable);
        public Int64 getInt(string inVariableName, out bool outSuccess); //returns 0L if not successful
        public string getString(string inVariableName, out bool outSuccess); //returns "" if not successful
        public double getDouble(string inVariableName, out bool outSuccess); //returns 0.0d if not successful
        public Int64 getExpirationDateForVariable(string inVariableName, out bool outSuccess);
        public bool clearVariableImmediately(string inVariableName); //returns true if successful, false otherwise
    }
}
