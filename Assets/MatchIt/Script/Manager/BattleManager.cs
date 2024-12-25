using System.Collections;
using MatchIt.Script.Event;
using UnityEngine;

namespace MatchIt.Player.Script
{
    public class BattleManager : MonoBehaviour
    {
        public PackPlayerData demoPlayerOneData; // demo only

        [SerializeField] private PlayerLoader playerOneLoader;
        [SerializeField] private PlayerLoader playerTwoLoader;
        
        public static BattleManager Instance { get; private set; }

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
        }

        private void OnDisable()
        {
            EventPub.OnPlayEvent -= OnPlayEvent;
        }

        public float formationTimeout { get; private set; } = 10;
        private Coroutine _formationCoroutine;
        private Coroutine _battleCoroutine;
        private bool _isBattleStarted = false;

        public PlayData PlayerOneData { get; private set; }
        public PlayData PlayerTwoData { get; private set; }


        public void SetPlayerOneData(PlayData playData)
        {
            PlayerOneData = playData;
        }

        public void SetPlayerTwoData(PlayData playData)
        {
            PlayerTwoData = playData;
        }

        private void OnPlayEvent(PlayEvent playEvent)
        {
            switch (playEvent)
            {
                case PlayEvent.OnFormationStart:
                    _formationCoroutine = StartCoroutine(FormationStart());
                    break;
                case PlayEvent.OnBattleData:
                    if (!_isBattleStarted)
                        _battleCoroutine = StartCoroutine(BattleStart());
                    break;
            }
        }

        private IEnumerator FormationStart()
        {
            Debug.Log("formation start");
            yield return new WaitForSeconds(formationTimeout);

            FormationEnd();
        }

        private void FormationEnd()
        {
            // end formation coroutine
            if (_formationCoroutine == null) return;
            {
                StopCoroutine(_formationCoroutine);
                _formationCoroutine = null;
            }

            EventPub.Emit(PlayEvent.OnFormationEnd);
            // send battle data to server
            Debug.Log("Formation data sent");

            // for demo only
            SetBattleData();
            EventPub.Emit(PlayEvent.OnBattleData);
            _battleCoroutine = StartCoroutine(BattleStart());
        }

        private void SetBattleData()
        {
            PlayerTwoData = new PlayData() // demo
            {
                AttackCard = "ogun",
                DefenseCard = "osun",
                AttackSpell = "double",
                DefenseSpell = "divide",
                AttackPoint = 10,
                DefensePoint = 10,
                PlayerHealth = 50
            };
            
            playerOneLoader.SetCards(PlayerOneData);
            playerTwoLoader.SetCards(PlayerTwoData);
        }

        private IEnumerator BattleStart()
        {
            _isBattleStarted = true;
            yield return new WaitForSeconds(2);
            // simulate battle
            SimulateBattle();
        }

        private void BattleWin()
        {
            // display player score
            EventPub.Emit(PlayEvent.OnBattleWin);
            Debug.Log("done simulating");
            _isBattleStarted = false;
        }

        private void BattleEnd()
        {
            // announce battle winner
        }

        private void SimulateBattle()
        {
            EventPub.Emit(PlayEvent.OnBattleStart);

            // display card point
            // apply spell
            // attack player two defence
            // attack player two health
            // attack player one defence
            // attack player one health
            Debug.Log(JsonUtility.ToJson(PlayerOneData));
            Debug.Log(JsonUtility.ToJson(PlayerTwoData));

            BattleWin();
        }
    }
}