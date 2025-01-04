using DG.Tweening;
using IjaOrisha.Script.Network;
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
    OnSessionEnd,
    OnRoundData, // Server event
    OnFormationStart,
    OnFormationEnd,
    OnBattleData, // Server event
    OnBattleStart,
    OnBattleWin
}


public class EventPub
{
    public delegate void PlayEvents(PlayEvent playEvent);

    public static event PlayEvents OnPlayEvent;

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