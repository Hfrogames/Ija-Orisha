using DG.Tweening;
using UnityEngine;


public enum PlayEvent
{
    OnStart,
    OnLobbyConnected,
    OnLobbyDisconnected,
    OnLobbyJoined,
    OnSessionPaired,
    OnSessionConnected,
    OnSessionDisconnected,
    OnSessionJoined,
    OnSessionStart,
    OnSessionEnd, // session is over
    OnSessionWin, // player won the session
    OnSessionLose, // player lost the session
    OnSessionDraw, // both player draw
    OnRoundData, // Server event
    OnFormationStart,
    OnFormationSubmit,
    OnFormationEnd,
    OnBattleData, // Server event
    OnSimulationStart,
    OnSimulationEnd
}


public class EventPub
{
    public delegate void PlayEvents(PlayEvent playEvent);

    public static event PlayEvents OnPlayEvent;

    // ReSharper disable Unity.PerformanceAnalysis
    public static void Emit(PlayEvent playEvent, float delay = 0)
    {
        DOVirtual.DelayedCall(delay, () => OnPlayEvent?.Invoke(playEvent));
    }

    // Socket response
    public delegate void SocketMessage(SocMessage message);

    public static event SocketMessage OnSocketMessage;

    public static void Emit(SocMessage message)
    {
        OnSocketMessage?.Invoke(message);
    }
}