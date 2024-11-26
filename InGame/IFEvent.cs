using Cities_of_Mosaic_Isle_PublicInterfaces.Helper;
using System.Collections.ObjectModel;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.InGame
{
    public interface IFEvent : IFMOID, IFNamedDebugObject
    {
        //an in-game event is: a happenstance, based possibly on conditions, that occurs within the player's sight or that changes the status of things not visible to the player.
        //(It is possible to create an event that is "invisible" to the player and changes things which are visible to the player, but IMO this is bad game design and I do not do it.)
        //  in-game events can either happen to delegations or the player community.
        //  of those that happen to delegations, there are three possibilities: pre-arrival events, arrival events (only immediately before or after handling arrival), or post-arrival events.
        //    an event can be both a pre-arrival and post-arrival event, but not either and also an arrival event
        //  of those that happen to the player community, there are many possibilities:
        //            (note that                                                       informatory (require and can have no player action besides acknowledgement)
        //             these are                                                       time limited "quests" (do not require input but player can take action to succeed/fail, player can also choose to "turn in" early or ignore, time limited)
        //             all just                                                        "offers" (do not require input but player must accept to enjoy its effects, time can be limited or unlimited)
        //             examples)                                                       immediate choices (require player action before time continues)
        //                                                                             others as can be imagined.
        //    the distinction between these possibilites is determined entirely by scripting, save for a bool that labels "must resolve before time continues":
        //      an informatory event performs its script, reports to the player, and its resolution "choice" does nothing but move the event report to the dismissal screen
        //      a "quest" performs a script, and that script enables a certain other informatory event and forces its occurrence time to a certain value/range/MTTH.  That informatory event is the "check failure" event, meaning its script will check if the player has completed the task, and if so provide rewards, and otherwise penalties.  Then, it will disable itself.
        //        The quest usually offers a button to resolve the quest early if the player has successfully completed it early.  This button will disable the "check failure" event because it should perform a similar function (no double-dipping).
        //        The quest may offer a button to reject the quest.  This button will also disable the "check failure" event because it should perform a similar function (no double penalty).
        //      an "offer" reports to the player and offers buttons that perform scripts when chosen.  The "hidden" or "available" scripts for these buttons may be time-limited.
        //      an "immediate choice" will offer at least one viable choice to the player, and evaluate a script based on the button chosen.
        //        Note that hard code will check if an event does both: A) offering no viable choices and B) stopping time.  If this is the case, the game cannot be guaranteed to continue.
        //        Therefore, we prevent this problem.  An event that is flagged as "stopping time" and is evaluated as offering no viable choices has a hard-coded button added to it as a choice:
        //        "We don't have time to deal with this!"  All the button will do is advance the event report to the dismissal screen so gameplay can continue.
        //        That means you, the modder (or me), need to be aware that if your button conditions do not cover all possibilities, the player could skip your event entirely.

        //last note: events have these scripts: "script that gates this event", "script that occurs before event is presented", "scripts which gates a button appearing (per button)" and "scripts which occur when a choice is selected (per button)".
        //by convention: "script that gates this event" should not change game state, because it can be called without the event happening.  This script calculates the answer to the question "can this event happen".
        //               "script that occurs before this event is presented" can change game state, and always happens if-and-when the event happens.  This script represents "the event occurring".
        //               "script which gates a button appearing (per button)" should not change game state, because it is re-evaluated each time the game state changes or the player enters the Event View.  (There's some caching actually but it makes no difference.)  This script calculates the answer to the question "can the player respond to the event this way".
        //               "scripts which occur when a choice is selected (per button)" can and probably should change game state, and happens if and only if the player makes the associated choice.  This script represents "the consequences of the player's choice".

        //GUARANTEE SECTION:
        //here we list the guarantees that a derived class must meet as a function of its existence as an IFEvent:
        //A) This class represents a moddable object; therefore, by the time any script can access members of this class, the return values of all functions will only depend on the function's inputs.
        //  i) as a moddable object, this class has a tag list.  getTagList() will return a list of unique tags.  hasTag(string inTag) will return the same value as getTagList().Contains(inTag).  getTagList() will not return null, but the list may be empty.
        //B) one and only one of "is*Event" will return true; the others will return false.
        //  i) getTypeOfEvent()'s return value will match which one of "is*Event" is true.
        //  ii) isMTTHType() will return isNormalEvent() || isPrePostArrivalEvent().  isChanceType() will return !isMTTHType().
        //C) getSimultaneousWithOccurrenceNumber() will return an Int64 that is a logical OR of the values in eSimultaneousWithOccurrence that are true for this event.
        //  i) the bool accessors will return the same value as calling getSimultaneousWithOccurrenceNumber() and checking if the bit is 1.
        //  ii) if isSimultaneousEvent() would return false, then getSimultaneousWithOccurrenceNumber() will return 0 and the bool accessors will all return false.
        //  iii) if isSimultaneousEvent() would return true, then getSimultaneousWithOccurrenceNumber() will not return 0
        //D) getDefaultMTTH() will return a positive number.
        //E) getDefaultWeight() will return a number greater than 0.0d
        //F) if isInvisible() would return true, getPotentialButtonCount() will return zero.  Otherwise, getPotentialButtonCount() will return a positive number.
        //G) if isInvisible() would return true, isStopTimeAndForceEvaluate() will return false.
        //H) isInvisible() will return false if getTypeOfEvent() is Arrival
        //I) if getTypeOfEvent() would return cArrival, getDelegationType() must not return IFDelegation.eDelegationType.cNone
        //J) if getTypeOfEvent() would return cNormal or cSimultaneous, getDelegationType() must return IFDelegation.eDelegationType.cNone
        //K) if isNormalEvent() would return false, evokeEventImmediately() does nothing

        //for this MO class, establish consts and enums:
        public const int cNumTypesOfEvents = 4;
        public enum eTypeOfEvent
        {
             cNormal = 0 //a community event of any kind
            ,cArrival = 1 //a player-sourced-delegation arrival event (not post- or pre-)
            ,cPostPreArrivalEvent = 2 //a player-sourced-delegation event (script to enable will have access to a call that indicates post/pre status)
            ,cSimultaneous = 3 //an event that occurs once at an unchangeable time (see below occurrences)
        }

        public const int cSimultaneousWithOccurrenceMask = 0x7FFF;
        public const int cSimultaneousWithOccurrenceMaskHighestBit = 0x4000;
        public const int cSimultaneousDelegationStartMask = 0x3E0;
        public const int cSimultaneousDelegationEndMask = 0x7C00;
        [Flags]
        public enum eSimultaneousWithOccurrence
        {
            //the occurrences here are times when things happen and it can't just be calculated during regular overnights; the events in question are specifically related to immediate occurrences
            //when an occurrence occurs, only one event is chosen relating to that occurrence.
             cNone = 0x0
            ,cPlayerDelegationArriveHome = 0x1 //right after a player delegation arrives home -- meaning all other events of the delegation are resolved and (therefore) its full information is known
            ,cTheLadyAppears = 0x2 //when The Lady herself appears on the local map
            ,cRebelsAppear = 0x4 //when there are now rebels on the local map
            ,cOtherEnemiesAppear = 0x8 //when there are now generic or foreign enemies on the local map
            ,cWanderersArrive = 0x10 //when Wanderers show up, they have some sort of circumstance, and that itself is an event which handles itself.  However, another event could be conditioned on the fact and time that Wanderers do show up, and this tag indicates those *secondary* events.

            //I choose to split out the delegation kinds here for the purposes of performance; if "ForeignDelegation" were one big thing it would hit a lot of "not associated with this foreign delegation type" events before a good event triggered
            //NOTE TO SELF: all these are for the appropriate IFDelegation.eDelegationType
            //when a foreign delegation of the given type shows up, they may have some sort of circumstance beyond their basics.  The *fact* that such a delegation is sent, and what their circumstance is, should be disconnected -- this is just the second
            ,cForeignDelegation_TradeStart = 0x20
            ,cForeignDelegation_EmigrationStart = 0x40
            ,cForeignDelegation_WarStart = 0x80
            ,cForeignDelegation_RaidingStart = 0x100
            ,cForeignDelegation_DiplomacyStart = 0x200
            ,cForeignDelegation_TradeEnd = 0x400
            ,cForeignDelegation_EmigrationEnd = 0x800
            ,cForeignDelegation_WarEnd = 0x1000
            ,cForeignDelegation_RaidingEnd = 0x2000
            ,cForeignDelegation_DiplomacyEnd = 0x4000
        }
        //each of these should be triggered at the correct time, under the correct conditions.  But what are those conditions/times?
        //the foreign delegation simultaneous triggers will require some sort of list of foreign delegations that are present on the map -- this should be accessible (read only) by scripts.  For all events relating to delegations, an accessor of "which delegation triggered this event" should be available to scripts somehow.
        //cPlayerDelegationArriveHome: when (just before) the pops in a non-lost delegation are placed back on the map
        //cTheLadyAppears: when (just after) The Lady appears on the map
        //cRebelsAppear: when (just after) military time is entered, if there are rebels on the map
        //cOtherEnemiesAppear: when (just after) military time is entered, if there are generic or foreign enemies on the map
        //cWanderersArrive: when (just after) any count of Wanderers are on the map and there were 0 on the map one tick ago
        //cForeignDelegation*Start: when (just after) a foreign delegation's pops are placed on the map (see the function in InGameStageLogic that does both at once)
        //cForeignDelegation*End: Whenever during peacetime there are no longer any of the foreign delegation's pops alive on the map.

        public bool hasTag(string inTag);
        public ReadOnlyCollection<string> getTagList();

        public IFEvent.eTypeOfEvent getTypeOfEvent();
        public bool isNormalEvent();
        public bool isArrivalEvent();
        public bool isPrePostArrivalEvent();
        public bool isSimultaneousEvent();
        public bool isMTTHType(); //normal or pre/post arrival event
        public bool isOccurrenceType(); //arrival event or simultaneous event

        public Int64 getSimultaneousWithOccurrenceNumber();
        public bool isSimultaneousPlayerDelegationArriveHome();
        public bool isSimultaneousTheLadyAppears();
        public bool isSimultaneousRebelsAppear();
        public bool isSimultaneousOtherEnemiesAppear();
        public bool isSimultaneousWanderersArrive();
        public bool isSimultaneousForeignDelegation_TradeStart();
        public bool isSimultaneousForeignDelegation_EmigrationStart();
        public bool isSimultaneousForeignDelegation_WarStart();
        public bool isSimultaneousForeignDelegation_DiplomacyStart();
        public bool isSimultaneousForeignDelegation_RaidingStart();
        public bool isSimultaneousForeignDelegation_TradeEnd();
        public bool isSimultaneousForeignDelegation_EmigrationEnd();
        public bool isSimultaneousForeignDelegation_WarEnd();
        public bool isSimultaneousForeignDelegation_DiplomacyEnd();
        public bool isSimultaneousForeignDelegation_RaidingEnd();

        public bool isDefaultEnabled();
        public Int64 getDefaultMTTH();
        public double getDefaultWeight();

        public IFDelegation.eDelegationType getDelegationType(); //this must be non-zero for arrival events.  This *can* be non-zero for postPreArrivalEvents.  If it is nonzero, then it is specific to a delegation of that type.

        public bool isInvisible(); //an invisible event does not generate an event report
        public bool isStopTimeAndForceEvaluate(); //when an event report for this event is in the Event View, time cannot continue
        public bool canHappenWhileReportPending(); //if this is true, this event can occur again while there is an event report for this event in the Event View.  If this is false, this event cannot occur again while there is an event report for this event that has not yet been dismissed.
        public int getPotentialButtonCount();

        //note that this is not the "usual" way that events are triggered, and is for scripts only.  Calling this on a normal event causes the event to occur immediately, i.e. before the script calling this even returns.
        public void evokeEventImmediately(bool inBypassGateScript, bool inBypassEnableStatusBool);
    }
}
