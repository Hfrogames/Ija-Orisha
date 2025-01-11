using UnityEngine;

namespace IjaOrisha
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private SessionSocket sessionSocket;
        [SerializeField] private FormationManager formationManager;
        [SerializeField] private SimulationManager simulationManager;

        private SocMessage _battleData;

        private void OnEnable()
        {
            EventPub.OnPlayEvent += OnPlayEvent;
        }

        private void OnDisable()
        {
            EventPub.OnPlayEvent -= OnPlayEvent;
        }

        private void Start()
        {
            BattlePlayer.LoadDummy();
        }

        private void OnPlayEvent(PlayEvent playEvent)
        {
            switch (playEvent)
            {
                case PlayEvent.OnSessionStart:
                    string joinDataString = SaveData.GetItemString("sessionToken");
                    _battleData = JsonUtility.FromJson<SocMessage>(joinDataString);
                    break;
                case PlayEvent.OnFormationStart:
                    formationManager.FormationStart();
                    simulationManager.SimulationEnd();
                    break;
                case PlayEvent.OnFormationEnd:
                    SocMessage battleData = new SocMessage()
                    {
                        action = "getBattleData",
                        playerID = PlayerManager.Instance.PlayerID,
                        roomID = _battleData.roomID,
                        playerOneBD = formationManager.Pack()
                    };

                    sessionSocket.SendWebSocketMessage(battleData);
                    break;
                case PlayEvent.OnBattleData:
                    formationManager.FormationEnd();
                    simulationManager.LoadSimulationData();
                    break;
                case PlayEvent.OnSimulationStart:
                    simulationManager
                        .SimulationStart();
                    break;
                case PlayEvent.OnSessionEnd:
                case PlayEvent.OnSimulationEnd:
                    BattlePlayer.FindWinner();
                    break;
            }
        }
    }
}