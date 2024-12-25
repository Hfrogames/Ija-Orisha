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

        // Card event
        // public delegate void CardSelected(Card clickedCard);
        //
        // public static event CardSelected OnCardSelected;
        //
        // public static void Emit(Card clickedCard)
        // {
        //     OnCardSelected?.Invoke(clickedCard);
        // }
    }
}