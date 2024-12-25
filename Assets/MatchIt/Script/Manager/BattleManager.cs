using System;
using System.Collections;
using MatchIt.Script.Event;
using UnityEngine;

namespace MatchIt.Player.Script
{
    public class BattleManager : MonoBehaviour
    {
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

        private float _formationTimeout = 5;
        private Coroutine _formationCoroutine;
        private Coroutine _battleCoroutine;
        private bool _isBattleStarted = false;

        public PlayData PlayerOneData { get; private set; }
        public PlayData PlayerTwoData { get; private set; }

        private void Start()
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
        }

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
            yield return new WaitForSeconds(_formationTimeout);

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
            EventPub.Emit(PlayEvent.OnBattleData);
            _battleCoroutine = StartCoroutine(BattleStart());
        }

        private IEnumerator BattleStart()
        {
            _isBattleStarted = true;
            Debug.Log("Battle start");
            yield return new WaitForSeconds(2);
            // simulate battle
            EventPub.Emit(PlayEvent.OnBattleStart);
            Debug.Log("Battle started");

            // simulate battle end
            yield return new WaitForSeconds(2);
            BattleWin();
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
    }
}