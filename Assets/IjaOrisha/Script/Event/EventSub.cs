using IjaOrisha;
using UnityEngine;

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
                InSimulation = true;
                break;
            case PlayEvent.OnSimulationEnd:
                InSimulation = false;
                BattlePlayer.FindWinner();
                break;
            case PlayEvent.OnSessionEnd:
                IsSessionEnd = true;
                break;
        }
    }

    public static bool InFormation;
    public static bool IsSessionEnd;
    public static bool InSimulation;

    public static void Initialize()
    {
        // this should init general eventsub
        InFormation = IsSessionEnd = InSimulation = false;
    }
}