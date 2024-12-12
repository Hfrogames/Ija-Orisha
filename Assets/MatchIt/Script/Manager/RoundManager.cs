using System;
using MatchIt.Script.Event;
using MatchIt.UI.Script;
using UnityEngine;
using UnityEngine.Serialization;

namespace MatchIt.Player.Script
{
    public class RoundManager : MonoBehaviour
    {
        public static RoundManager Instance { get; private set; }

        public int currentRound { get; private set; } = 1;
        public int maxRound { get; private set; } = 3;

        [SerializeField] private UIManager uiManager;

        private int _playerOneScore = 0;
        private int _playerTwoScore = 0;
        private int _roundWinScore = 1;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void OnEnable()
        {
            EventPub.OnPlayEvent += OnPlayEvent;
        }

        private void OnDisable()
        {
            EventPub.OnPlayEvent -= OnPlayEvent;
        }

        public void SetRoundWinScore(int score)
        {
            _roundWinScore = score;
        }


        private void OnPlayEvent(PlayEvent playEvent)
        {
            switch (playEvent)
            {
                case PlayEvent.OnCardOneMatch:
                    _playerOneScore++;
                    CheckRoundWinner();
                    break;
                case PlayEvent.OnCardTwoMatch:
                    _playerTwoScore++;
                    CheckRoundWinner();
                    break;
                case PlayEvent.OnRoundReset:
                    ResetRound();
                    uiManager.HideWinner();
                    break;
            }
        }

        private void CheckRoundWinner()
        {
            if (_playerOneScore >= _roundWinScore)
            {
                // Debug.Log("Player one won");
                uiManager.DisplayWinner("Player One");
            }
            else if (_playerTwoScore >= _roundWinScore)
            {
                uiManager.DisplayWinner("Player Two");
                // Debug.Log("Player two won");
            }
        }

        public void ResetRound()
        {
            _playerOneScore = 0;
            _playerTwoScore = 0;
        }
    }
}