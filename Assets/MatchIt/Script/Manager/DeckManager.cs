using System.Collections.Generic;
using MatchIt.Script.Event;
using UnityEngine;

namespace MatchIt.Player.Script
{
    public class DeckManager : MonoBehaviour
    {
        [field: SerializeField] public List<CardSO> cardSoDB { get; private set; }
        [SerializeField] private RoundManager roundManager;

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

        public void RefundCard(CardSO cardSo)
        {
           
        }

        private void DisplayDeck()
        {
            
        }

        private void OnPlayEvent(PlayEvent playEvent)
        {
           
        }
    }
}