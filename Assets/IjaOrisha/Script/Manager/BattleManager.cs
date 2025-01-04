using System.Collections;
using DG.Tweening;
using IjaOrisha.Cards.Script.CardControl;
using IjaOrisha.Cards.Script.PlayerLoader;
using IjaOrisha.Script.Network;
using UnityEngine;
using UnityEngine.Serialization;

namespace IjaOrisha.Player.Script
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance { get; private set; }
        public float FormationTimeout { get; private set; } = 10;

        [SerializeField] private SessionSocket sessionSocket;
        [SerializeField] private PackPlayerData packPlayerData;
        [SerializeField] private PlayerLoader playerOneLoader;
        [SerializeField] private PlayerLoader playerTwoLoader;
        [SerializeField] private DeckLoader deckLoader;

        private BattleData _playerOneData;
        private BattleData _playerTwoData;
        private SocMessage _joinData;
        private string _playerID;

        private Coroutine _formationCoroutine;
        private Coroutine _battleCoroutine;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            EventPub.OnPlayEvent += OnPlayEvent;
            EventPub.OnSocketMessage += OnSocketMessage;
        }

        private void OnDisable()
        {
            EventPub.OnPlayEvent -= OnPlayEvent;
            EventPub.OnSocketMessage -= OnSocketMessage;
        }


        private void OnPlayEvent(PlayEvent playEvent)
        {
            switch (playEvent)
            {
                case PlayEvent.OnSessionStart:
                    string joinDataString = SaveData.GetItemString("sessionToken");
                    _joinData = JsonUtility.FromJson<SocMessage>(joinDataString);
                    deckLoader.Reveal();
                    break;
            }
        }

        private void OnSocketMessage(SocMessage message)
        {
            if (message.action == "formationStart")
            {
                if (PlayerManager.Instance.PlayerOneID == _joinData.playerOne)
                {
                    playerOneLoader.DisplayPlayerHealth(message.playerOneBD.PlayerHealth);
                    playerTwoLoader.DisplayPlayerHealth(message.playerTwoBD.PlayerHealth);
                }
                else if (PlayerManager.Instance.PlayerOneID == _joinData.playerTwo)
                {
                    playerOneLoader.DisplayPlayerHealth(message.playerTwoBD.PlayerHealth);
                    playerTwoLoader.DisplayPlayerHealth(message.playerOneBD.PlayerHealth);
                }

                FormationTimeout = message.roundTimeout;
                _formationCoroutine ??= StartCoroutine(FormationStart());
            }

            if (message.action == "formationEnd")
            {
                FormationEnd();
            }

            if (message.action == "battleData")
            {
                if (PlayerManager.Instance.PlayerOneID == _joinData.playerOne)
                {
                    _playerOneData = message.playerOneBD;
                    _playerTwoData = message.playerTwoBD;
                }
                else if (PlayerManager.Instance.PlayerOneID == _joinData.playerTwo)
                {
                    _playerOneData = message.playerTwoBD;
                    _playerTwoData = message.playerOneBD;
                }

                playerOneLoader.SetCards(_playerOneData);
                playerTwoLoader.SetCards(_playerTwoData);

                EventPub.Emit(PlayEvent.OnBattleData);
                _battleCoroutine ??= StartCoroutine(BattleStart());
            }
            
            if (message.action == "battleEnd")
            {
                BattleEnd();
            }
        }

        private IEnumerator FormationStart()
        {
            EventPub.Emit(PlayEvent.OnFormationStart);
            yield return new WaitForSeconds(FormationTimeout);

            FormationEnd();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void FormationEnd()
        {
            if (_formationCoroutine == null) return;

            // stop formation coroutine
            StopCoroutine(_formationCoroutine);
            _formationCoroutine = null;

            EventPub.Emit(PlayEvent.OnFormationEnd);
            // send player one battle data to server
            SendBattleData();

            // for demo only
            // SetBattleData();
            // EventPub.Emit(PlayEvent.OnBattleData);
        }

        private void SendBattleData()
        {
            SocMessage battleData = new SocMessage()
            {
                action = "getBattleData",
                playerID = PlayerManager.Instance.PlayerOneID,
                roomID = _joinData.roomID,
                playerOneBD = packPlayerData.Pack()
            };

            sessionSocket.SendWebSocketMessage(battleData);
        }

        // ReSharper disable Unity.PerformanceAnalysis

        private IEnumerator BattleStart()
        {
            yield return new WaitForSeconds(2);
            EventPub.Emit(PlayEvent.OnBattleStart);

            playerTwoLoader.DisplayCardData();
            playerOneLoader.DisplayCardData();

            yield return new WaitForSeconds(3.8f);
            playerOneLoader.AttackDefence(_playerTwoData.DefensePoint);
            playerTwoLoader.DefenceAttacked(_playerOneData.AttackPoint);
            playerTwoLoader.AttackHealthPoint();

            yield return new WaitForSeconds(3.5f);
            playerTwoLoader.AttackDefence(_playerOneData.DefensePoint);
            playerOneLoader.DefenceAttacked(_playerTwoData.AttackPoint);
            playerOneLoader.AttackHealthPoint();

            // yield return new WaitForSeconds(2);
            BattleWin();
        }

        private void BattleWin()
        {
            EventPub.Emit(PlayEvent.OnBattleWin);

            // display round winner
            Debug.Log("done simulating");

            // stop formation coroutine
            StopCoroutine(_battleCoroutine);
            _battleCoroutine = null;
        }

        private void BattleEnd()
        {
            EventPub.Emit(PlayEvent.OnSessionEnd);
            // announce battle winner
        }
    }
}