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
            case PlayEvent.OnFormationStart:
                InFormation = true;
                break;
            case PlayEvent.OnFormationEnd:
                InFormation = false;
                break;
            case PlayEvent.OnBattleStart:
                InBattle = true;
                break;
            case PlayEvent.OnBattleWin:
                InBattle = false;
                break;
            case PlayEvent.OnLobbyConnected:
                IsLobbyConnected = true;
                break;
        }
    }

    public static bool InFormation;
    public static bool InBattle;
    public static bool IsLobbyConnected;

    public static void Initialize()
    {
        // this should init general eventsub
    }
}