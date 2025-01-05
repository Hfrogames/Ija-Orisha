using IjaOrisha.Cards.Script.CardFormation;
using IjaOrisha.Script.Network;
using UnityEngine;

namespace IjaOrisha.Cards.Script
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private FormationManager formationManager;
        [SerializeField] private SessionSocket sessionSocket;


        private SocMessage _battleData;

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
                    string joinDataString = SaveData.GetItemString("sessionToken");
                    _battleData = JsonUtility.FromJson<SocMessage>(joinDataString);
                    break;
                case PlayEvent.OnFormationStart:
                    Debug.Log("formation start");
                    formationManager.FormationStart();
                    break;
                case PlayEvent.OnFormationEnd:
                    SocMessage battleData = new SocMessage()
                    {
                        action = "getBattleData",
                        playerID = PlayerManager.Instance.PlayerOneID,
                        roomID = _battleData.roomID,
                        playerOneBD = formationManager.Pack()
                    };

                    sessionSocket.SendWebSocketMessage(battleData);
                    Debug.Log(JsonUtility.ToJson(formationManager.Pack()));
                    break;
                case PlayEvent.OnBattleData:
                    formationManager.FormationEnd();
                    break;
            }
        }
    }
}