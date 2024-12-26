using System.Collections;
using MatchIt.Script.Event;
using UnityEngine;
using UnityEngine.Serialization;

namespace MatchIt.Player.Script
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance { get; private set; }
        public float FormationTimeout { get; private set; } = 2;

        [SerializeField] private PackPlayerData packPlayerData;
        [SerializeField] private PlayerLoader playerOneLoader;
        [SerializeField] private PlayerLoader playerTwoLoader;

        private PlayData _playerOneData;
        private PlayData _playerTwoData;

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
        }

        private void OnDisable()
        {
            EventPub.OnPlayEvent -= OnPlayEvent;
        }


        private void OnPlayEvent(PlayEvent playEvent)
        {
            switch (playEvent)
            {
                case PlayEvent.OnSessionStart:
                    _formationCoroutine ??= StartCoroutine(FormationStart());
                    break;
                case PlayEvent.OnBattleData:
                    _battleCoroutine ??= StartCoroutine(BattleStart());
                    break;
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
            Debug.Log("Formation data sent");

            // for demo only
            SetBattleData();
            EventPub.Emit(PlayEvent.OnBattleData);
        }

        private void SetBattleData()
        {
            // _playerOneData = packPlayerData.Pack();
            _playerOneData = new PlayData() // demo
            {
                AttackCard = "sango",
                DefenseCard = "ogun",
                AttackSpell = "double",
                DefenseSpell = "divide",
                AttackPoint = 6,
                DefensePoint = 8,
                PlayerHealth = 50
            };
            _playerTwoData = new PlayData() // demo
            {
                AttackCard = "ogun",
                DefenseCard = "osun",
                AttackSpell = "double",
                DefenseSpell = "divide",
                AttackPoint = 10,
                DefensePoint = 10,
                PlayerHealth = 50
            };

            playerOneLoader.SetCards(_playerOneData);
            playerTwoLoader.SetCards(_playerTwoData);
        }

        // ReSharper disable Unity.PerformanceAnalysis

        private IEnumerator BattleStart()
        {
            yield return new WaitForSeconds(2);
            EventPub.Emit(PlayEvent.OnBattleStart);

            playerTwoLoader.DisplayCardData();
            playerTwoLoader.ApplyCardSpell();

            playerOneLoader.DisplayCardData();
            playerOneLoader.ApplyCardSpell();

            // yield return new WaitForSeconds(4);
            playerOneLoader.AttackDefence();
            playerTwoLoader.DefenceAttacked();
            playerTwoLoader.AttackHealthPoint();

            // yield return new WaitForSeconds(4);
            playerTwoLoader.AttackDefence();
            playerOneLoader.DefenceAttacked();
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
            // announce battle winner
        }
    }
}