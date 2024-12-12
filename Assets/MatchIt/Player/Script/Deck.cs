using MatchIt.Script.Event;
using UnityEngine;
using UnityEngine.UI;

namespace MatchIt.Player.Script
{
    public class Deck : MonoBehaviour
    {
        [SerializeField] private Image icon;

        private DeckManager _deckManager;
        private CardSO _assignedCardSo;
        private bool _isPlayerOneMatched;
        private bool _isPlayerTwoMatched;

        private void OnEnable()
        {
            EventPub.OnPlayEvent += OnPlayEvent;
            EventPub.OnCardSelected += OnCardEvent;
        }

        private void OnDisable()
        {
            EventPub.OnPlayEvent -= OnPlayEvent;
            EventPub.OnCardSelected -= OnCardEvent;

            if (_assignedCardSo != null)
            {
                _deckManager.RefundCard(_assignedCardSo);
                _assignedCardSo = null;
            }

            _isPlayerOneMatched = _isPlayerTwoMatched = false;
        }

        public void SetCardSo(DeckManager deckManager)
        {
            _deckManager = deckManager;
            _assignedCardSo = deckManager.BorrowCard();
            icon.sprite = _assignedCardSo.sprite;
            gameObject.SetActive(true);
        }

        private void OnCardEvent(Card card)
        {
            if (_assignedCardSo.cardID == card.cardID)
            {
                if (card.playerID == Card.PlayerID.Player1 && !_isPlayerOneMatched)
                {
                    _isPlayerOneMatched = true;
                    EventPub.Emit(PlayEvent.OnCardOneMatch);
                }
                else if (card.playerID == Card.PlayerID.Player2 && !_isPlayerTwoMatched)
                {
                    _isPlayerTwoMatched = true;
                    EventPub.Emit(PlayEvent.OnCardTwoMatch);
                }
            }
        }

        private void OnPlayEvent(PlayEvent playEvent)
        {
            if (playEvent == PlayEvent.OnRoundReset)
            {
                gameObject.SetActive(false);
            }
        }
    }
}