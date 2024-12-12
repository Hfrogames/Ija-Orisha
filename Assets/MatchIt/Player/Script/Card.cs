using System;
using MatchIt.Script.Event;
using UnityEngine;
using UnityEngine.UI;

namespace MatchIt.Player.Script
{
    public class Card : MonoBehaviour
    {
        public enum PlayerID
        {
            Player1,
            Player2
        }

        [field: SerializeField] public PlayerID playerID { get; private set; }
        public CardID cardID { get; private set; }

        private bool isPlayer1 => playerID == PlayerID.Player1;
        private bool isPlayer2 => playerID == PlayerID.Player2;

        [SerializeField] private CardSO cardSo;
        [SerializeField] private Image iconSprite;
        [SerializeField] private GameObject[] playerBg;
        [SerializeField] private Button button;


        private void Awake()
        {
            button.onClick.AddListener(OnButtonClicked);
        }

        private void Start()
        {
            AssignCard();
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnButtonClicked);
        }

        private void AssignCard()
        {
            if (isPlayer1) playerBg[0].SetActive(true);
            else if (isPlayer2) playerBg[1].SetActive(true);

            iconSprite.sprite = cardSo.sprite;
            cardID = cardSo.cardID;
        }

        private void OnButtonClicked()
        {
            EventPub.Emit(this);
        }
    }
}