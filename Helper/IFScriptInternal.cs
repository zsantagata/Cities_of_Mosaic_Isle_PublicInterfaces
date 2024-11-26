//this interface is not useful to modders but is what a script is, and must be public and in this namespace
namespace Cities_of_Mosaic_Isle_PublicInterfaces.Helper
{
    public interface IFScriptInternals
    {
        List<object> exec_script(int inScriptInstance, params object[] inArguments);
    }
}
