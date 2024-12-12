using System;
using UnityEngine;
using UnityEngine.UI;

namespace MatchIt.Player.Script
{
    public class Card : MonoBehaviour
    {
        private enum PlayerID
        {
            None,
            Player1,
            Player2
        }

        [SerializeField] private PlayerID playerID;

        private bool isNone => playerID == PlayerID.None;
        private bool isPlayer1 => playerID == PlayerID.Player1;
        private bool isPlayer2 => playerID == PlayerID.Player2;

        [SerializeField] private CardSO cardSo;
        [SerializeField] private Image iconSprite;
        [SerializeField] private GameObject[] playerBg;

        private CardID _cardID;

        private void Start()
        {
            AssignCard();
        }

        private void AssignCard()
        {
            if (isPlayer1) playerBg[0].SetActive(true);
            else if (isPlayer2) playerBg[1].SetActive(true);

            iconSprite.sprite = cardSo.sprite;
            _cardID = cardSo.cardID;
        }
    }
}