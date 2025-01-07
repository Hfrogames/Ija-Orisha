using System;
using System.Collections.Generic;
using UnityEngine;

namespace IjaOrisha
{
    public class LocalDB : MonoBehaviour
    {
        [SerializeField] private LocDB locDB;

        private bool IsCardSo => locDB == LocDB.CardSO;

        [SerializeField] private List<CardSO> cardSo;

        public int Count => cardSo.Count;

        public CardSO GetCard(string cardName)
        {
            if (!IsCardSo || cardSo == null || string.IsNullOrEmpty(cardName))
            {
                return null;
            }

            foreach (var card in cardSo)
            {
                if (card != null && card.name.Equals(cardName, StringComparison.OrdinalIgnoreCase))
                {
                    return card;
                }
            }

            return null;
        }

        public CardSO GetRandom()
        {
            if (!IsCardSo || cardSo == null || cardSo.Count == 0)
            {
                return null;
            }

            int randomIndex = UnityEngine.Random.Range(0, cardSo.Count);
            CardSO card = cardSo[randomIndex];
            cardSo.Remove(card);

            return card;
        }
    }


    public enum LocDB
    {
        CardSO
    }
}