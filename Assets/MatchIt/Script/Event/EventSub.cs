namespace MatchIt.Script.Event
{
    public class EventSub
    {
        static EventSub()
        {
            EventPub.OnPlayEvent += OnPlayEvent;
        }

        private static void OnPlayEvent(PlayEvent playEvent)
        {
            switch (playEvent)
            {
                case PlayEvent.OnSessionStart:
                    IsFormation = true;
                    break;
                case PlayEvent.OnFormationEnd:
                    IsFormation = false;
                    break;
                case PlayEvent.OnBattleStart:
                    InBattle = true;
                    break;
                case PlayEvent.OnBattleWin:
                    InBattle = false;
                    break;
            }
        }

        public static bool IsFormation;
        public static bool InBattle;

        public static void Initialize()
        {
            // this should init general eventsub
        }
    }
}