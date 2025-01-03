using IjaOrisha.Player.Script;
using TMPro;
using UnityEngine;

namespace IjaOrisha.UI.Script.Lobby
{
    public class PlayerID : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI iDText;

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
                case PlayEvent.OnLobbyConnected:
                    iDText.text = PlayerManager.Instance.PlayerID;
                    break;
            }
        }
    }
}