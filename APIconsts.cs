using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces
{
    //this class is used for constants that relate to how modders/I enter game data
    static public class APIconsts
    {
        //general:
        public const string cNewlineTag = "[newline]";

        //moddable section:
        public const string cModdableWildcard = "*";

        public enum eModdableWildcardBehaviors
        {
             cDisabled
            ,cUseWildcardFirstAndRemove //the wildcard will blast what has been done before and make everything uniform.  Afterwards, other changes can remove from the wildcard.  Used for reference lists.
            ,cUseWildcardLast  //the wildcard will preserve everything that has been done before but override all the remaining.  Used for tuple-reference lists.
        }

        //moddable item spec:
        //all files are text files, UTF8-encoded.  Notepad can do that, as can notepad++
        //all files are well-formed XML files.  The root node's tag should be the name of the mod or its nearest equivalent
        //each immediate child node of the root node is a moddable object.  The immediate child node's tag should be the type of object to be created.
        //  in addition, the immediate child node must have a single attribute, called name, which is the (unique if you want to make a new one, not unique if you want to override properties of the old one) name of the moddable object.
        //
        //all moddable object types will have defaults.  The core data will specify these in full.  When a new moddable object is being created, its values are initialized to the defaults.
        //  The moddable object's values will be changed based on what is specified by the immediate child node's child nodes (for ease, calling those "variable nodes").  The tags of the each variable node must match a tag in the defaults.  Variable node tags which are repeated take the last listed value.
        //  If a moddable object is being overridden, only the variable nodes that are listed will be changed; everything else will stay as it was.
        //  There is (and can be) no default name.
        //
        //because this is xml, there is no necessary ordering to the child nodes of a moddable object.
        //
        //the variable nodes must have a single attribute to them.  This attribute, called type, will be one of:
        //  int, double, string, int_list, double_list, string_list, or specific other custom things that are described in detail in XML comments
        //  int, double, and string will just be basic values (XML treats all values as strings so really int and double will be converted from string to an int/double.  For doubles use 12.34 format and for ints use either 0xAB1D or 1234 format)
        //  lists will have some number of children (can be 0).  Children of lists will be the basic values above, and their tag names do not matter, and do not need to have any attribute.
        public const string cModdableEncyclopediaEntryLabel = "Encyclopedia_Entry";
        public const string cModdableDefaultAttributeName = "default";
        public const string cModdableNullReferenceName = "null";

        //if multiple possible spritesheets are available, they are randomly selected from, with the exception that spritesheets with this tag will be excluded (if there are spritesheets without this tag, that is):
        public const string cSpriteSheetExcludeTag = "do not choose randomly";

        //when there are variables held (or psuedo-variables, like custom consts), they are in one of four states.  List those states here:
        public const int cVariableKindCount = 4;
        public enum eVariableKind
        {
             cDoesNotExist = 0
            ,cInt = 1
            ,cString = 2
            ,cDouble = 3
        }
        static public readonly ReadOnlyCollection<string> cVariableKindNames = (new List<string>()
        {
             "DoesNotExist"
            ,"Int"
            ,"String"
            ,"Double"
            ,"List"
        }).AsReadOnly();
        //custom consts can hold a list directly while variableHandler cannot (ConnectedVariable can be ~kinda), so it needs its own enum:
        public enum eCustomConstKind
        {
             cDoesNotExist = 0
            ,cInt = 1
            ,cString = 2
            ,cDouble = 3
            ,cList = 4
        }

        //scripts may want to mark certain pops/communities/buildings as "do not delete" for a while.  The actual value in variableHandler does not matter, as long as the variable exists.
        //the delete C# code will check for cDoNotDeleteSOVariableNames[appropriate enum] + uid
        //for as long as this variable exists, the pop/community/building will not be deleted, even if it would be otherwise
        //the variable is a connected variable so that many sources can mark it
        public enum eSOsMarkableAsDoNotDelete
        {
             cPop = 0
            ,cCommunity = 1
            ,cBuilding = 2
            //,cDelegation = 3 //delegations are necessarily transient things which end, so they cannot be marked as "do not delete"
        }
        public static readonly ReadOnlyCollection<string> cDoNotDeleteSOVariableNames = (new List<string>()
        {
            "donotdelete_pop", //append UID to the end of this
            "donotdelete_community", //append UID to the end of this
            "donotdelete_building", //append UID to the end of this
        }.AsReadOnly());

        //menutext-specific things:
        //color tags will look like: [red]<-- start of tag. text goes between tags. end of tag-->[/red].  Default color is black.
        public const int cMenu_TextColorsNum = 12;
        public enum eMenu_TextColors
        {
            cBlack = 0
            ,cWhite = 1
            ,cGray = 2
            ,cRed = 3
            ,cBlue = 4
            ,cYellow = 5
            ,cGreen = 6
            ,cPurple = 7
            ,cOrange = 8
            ,cBrown = 9
            ,cCyan = 10
            ,cMagenta = 11
        }
        static public readonly ReadOnlyCollection<string> cMenu_TextColorNames = (new List<string>
        {
             "black"
            ,"white"
            ,"grey"
            ,"red"
            ,"blue"
            ,"yellow"
            ,"green"
            ,"purple"
            ,"orange"
            ,"brown"
            ,"cyan"
            ,"magenta"
        }).AsReadOnly();

        //consts for default of preload values:
        public const string cUnrollTagListPrependDefault = "TAG:";
        public const string cCustomConstListRemoveItemsIncludeDefault = "[REMOVE]";
        public const bool cClearAllDLLsDefault = false;
        public const bool cLoadDLLsDefault = true;
        public const bool cSaveDLLsDefault = true;
        public const bool cCompileDLLsAsReleaseDefault = true;
    }
}
