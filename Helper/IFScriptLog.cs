//this interface is not useful to modders but is what a script is able to call in order to do debug
namespace Cities_of_Mosaic_Isle_PublicInterfaces.Helper
{
    public interface IFScriptLog
    {
        public void addToDebugMessage(int inScriptInstance, string inToAddToDebugMessage);
    }
}
