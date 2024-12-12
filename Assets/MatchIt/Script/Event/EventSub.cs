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
                // case EventPub.PlayEvent.isPassed:
                //     GameManager.Instance.SetGameState(GameManager.GameStates.Idle);
                //     break;
                // case EventPub.PlayEvent.isFailed:
                //     GameManager.Instance.SetGameState(GameManager.GameStates.Idle);
                //     break;
            }
        }

        public static void Initialize()
        {
        }
    }
}