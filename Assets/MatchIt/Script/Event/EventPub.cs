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
        OnSessionStart
    }
    
    

    public class EventPub
    {
        public delegate void PlayEvents(PlayEvent playEvent);

        public static event PlayEvents OnPlayEvent;

        public static void Emit(PlayEvent playEvent)
        {
            OnPlayEvent?.Invoke(playEvent);
        }

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