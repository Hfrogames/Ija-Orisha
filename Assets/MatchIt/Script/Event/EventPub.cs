using MatchIt.Player.Script;
using UnityEngine;

namespace MatchIt.Script.Event
{
    public enum PlayEvent
    {
        OnPlay,
        OnPaused,
        OnRoundPassed,
        OnRoundFailed,
        OnCardOneMatch,
        OnCardTwoMatch,
        OnRoundReset,
        OnRoundStart,
        OnLobbyConnected,
        OnLobbyDisconnected,
        OnLobbyJoined,
        OnSessionPaired,
        OnSessionConnected,
        OnSessionDisconnected,
        OnSessionJoined,
        OnSessionStart,
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

        public static void Emit(PlayEvent playEvent)
        {
            OnPlayEvent?.Invoke(playEvent);
        }

        // Socket response
        public delegate void SocketMessage(SocMessage message);

        public static event SocketMessage OnSocketMessage;

        public static void Emit(SocMessage message)
        {
            OnSocketMessage?.Invoke(message);
        }
    }
}