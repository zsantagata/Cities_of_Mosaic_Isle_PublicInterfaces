namespace Cities_of_Mosaic_Isle_PublicInterfaces.Helper
{
    public interface IFModdableCustomScripts
    {
        //GUARANTEE SECTION:
        //here we list the guarantees that a derived class must meet as a function of its existence as an IFModdableCustomScripts:
        //A) doesCustomScriptExistWithName will check if a script exists with the name provided and return true if and only if so.  Capitalization is ignored.
        //  i) script names will be unique as a result of ModdableCustomScripts-specific loading from file; script names are made lowercase.  An advisory/warning message will be sent if two scripts have the same name, and the latter will be used.
        //B) if runCustomScript is called with an inName that would cause doesCustomScriptExistWithName to return false, then:
        //  i) runCustomScript does not call any scripts
        //  ii) runCustomScript returns false
        //  iii) outOutputs is an empty List
        //C) otherwise:
        //  i) runCustomScript will call the unique script with the correct name exactly once.  inInputs will be provided to the called script as is, except that if inInputs is null, an empty List<object> will be used instead.
        //  ii) if any part of the custom script throws an exception, runCustomScript will report an error, and return false.  outOutputs will be an empty list.
        //  iii) if the custom script completes without an exception, runCustomScript will return true, and outOutputs will be the output of the script.
        //D) runCustomScriptCheckTypes is a wrapper for runCustomScript.  It returns true if, and only if, all of the following are true:
        //  i) runCustomScript returns true
        //  ii) inTypesOfOutputsExpected contains only members of type Type //typeof(string) is an example of a Type
        //  iii) outOutputs is the same size as inTypesOfOutputsExpected
        //  iv) the types of objects in outOutputs are in the same order, and of the same type, as indicated by inTypesOfOutputsExpected
        //    a) if an element of outOutputs is null, that is only considered valid if the type is nullable, and inIsNullOkay is true.
        //E) after a custom script is successfully called, if there is any debug string from the script to log, it will be logged before returning from runCustomScript.
        //  i) helpful note: that means if you have a script Custom_Script_Top which calls a Custom_Script_Middle which calls a Custom_Script_Bottom, the debug string from Custom_Script_Bottom will be logged first, then the debug string from Custom_Script_Middle, then the debug string from Custom_Script_Top.
        //F) does*MidnightScriptExistWithName will check if a script exists with the name provided and return true if and only if so.  Capitalization is ignored.
        //  i) script names will be unique as a result of ModdableCustomScripts-specific loading from file; script names are made lowercase.  An advisory/warning message will be sent if two scripts have the same name, and the latter will be used.

        public bool doesCustomScriptExistWithName(string inName);
        //early and late midnight scripts will be evoked by the midnight processor at the correct stage.  If something needs to be evoked by other scripts, it should be a custom script.  If something needs to be both, make it a custom script and have a midnight script that is a passthrough
        //the order of execution of midnight scripts cannot be guaranteed (other than that all of them will be executed at the correct stage in midnight processor).  If order needs to be guaranteed or maintained, a modder needs to change up their scripts/combine them
        public bool doesEarlyMidnightScriptExistWithName(string inName);
        public bool doesLateMidnightScriptExistWithName(string inName);

        public bool runCustomScript(string inName, List<object> inInputs, out List<object> outOutputs);
        public bool runCustomScriptCheckTypes(string inName, List<object> inInputs, out List<object> outOutputs, bool inIsNullOkay, params object[] inTypesOfOutputsExpected);
    }
}
