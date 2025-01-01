using System;
using UnityEngine;

namespace MatchIt.Script.LocalDB
{
    public class LocalDB : MonoBehaviour
    {
        [SerializeField] private LocDB locDB;

        private bool IsCardSo => locDB == LocDB.CardSO;

        [SerializeField] private CardSO[] cardSo;

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
            if (!IsCardSo || cardSo == null || cardSo.Length == 0)
            {
                return null;
            }

            int randomIndex = UnityEngine.Random.Range(0, cardSo.Length);
            return cardSo[randomIndex];
        }
    }
}

public enum LocDB
{
    CardSO
}