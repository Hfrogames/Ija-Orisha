using System.Collections.Generic;
using MatchIt.Script.Event;
using UnityEngine;

namespace MatchIt.Player.Script
{
    public class DeckManager : MonoBehaviour
    {
        [field: SerializeField] public List<CardSO> cardSoDB { get; private set; }
        [SerializeField] private RoundManager roundManager;
        [SerializeField] private Deck[] cardDecks;

        private int _amountToSpawn;
        private int _cachedBorrowed = -1;

        private void OnEnable()
        {
            EventPub.OnPlayEvent += OnPlayEvent;
        }

        private void OnDisable()
        {
            EventPub.OnPlayEvent -= OnPlayEvent;
        }

        public CardSO BorrowCard()
        {
            if (cardSoDB.Count == 0)
                return null;

            int index;
            do
            {
                index = Random.Range(0, cardSoDB.Count);
            } while (index == _cachedBorrowed && cardSoDB.Count > 1);

            _cachedBorrowed = index;
            CardSO card = cardSoDB[index];
            cardSoDB.RemoveAt(index);
            return card;
        }

        public void RefundCard(CardSO cardSo)
        {
            if (!cardSoDB.Contains(cardSo))
                cardSoDB.Add(cardSo);

            _cachedBorrowed = -1;
        }

        private void DisplayDeck()
        {
            _amountToSpawn = Random.Range(1, cardDecks.Length);
            roundManager.SetRoundWinScore(_amountToSpawn);
            for (int i = 0; i < _amountToSpawn; i++)
            {
                cardDecks[i].SetCardSo(this);
            }
        }

        private void OnPlayEvent(PlayEvent playEvent)
        {
            if (playEvent == PlayEvent.OnRoundStart)
            {
                DisplayDeck();
            }
        }
    }
}