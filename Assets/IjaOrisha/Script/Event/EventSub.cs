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
            case PlayEvent.OnSimulationStart:
                InBattle = true;
                break;
            case PlayEvent.OnSimulationEnd:
                InBattle = false;
                break;
            case PlayEvent.OnSessionEnd:
                IsSessionEnd = true;
                break;
        }
    }

    public static bool InFormation;
    public static bool IsSessionEnd;
    public static bool InBattle;

    public static void Initialize()
    {
        // this should init general eventsub
        InFormation = IsSessionEnd = InBattle = false;
    }
}