using MatchIt.Script.Event;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MatchIt.Script.Network
{
    public class ConnectionUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI lobbySocket;
        [SerializeField] private Image lobbySocketIcon;

        [SerializeField] private TextMeshProUGUI joinLobby;

        [SerializeField] private Transform joinGameSession;

        [SerializeField] private TextMeshProUGUI sessionSocket;
        [SerializeField] private Image sessionSocketIcon;

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
            lobbySocket.text = "offline";
            lobbySocket.color = lobbySocketIcon.color = Color.red;
        }

        private void OnPlayEvent(PlayEvent playEvent)
        {
            switch (playEvent)
            {
                case PlayEvent.OnLobbyConnected:
                    lobbySocket.text = "online";
                    lobbySocket.color = lobbySocketIcon.color = Color.green;
                    break;
                case PlayEvent.OnLobbyDisconnected:
                    lobbySocket.text = "offline";
                    lobbySocket.color = lobbySocketIcon.color = Color.red;
                    break;
                case PlayEvent.OnLobbyJoined:
                    joinLobby.color = lobbySocketIcon.color = Color.green;
                    break;
                case PlayEvent.OnSessionPaired:
                    joinGameSession.gameObject.SetActive(true);
                    break;
                case PlayEvent.OnSessionConnected:
                    sessionSocket.text = "online";
                    sessionSocket.color = sessionSocketIcon.color = Color.green;
                    sessionSocketIcon.gameObject.SetActive(true);
                    sessionSocket.gameObject.SetActive(true);
                    break;
                case PlayEvent.OnSessionDisconnected:
                    sessionSocket.text = "offline";
                    sessionSocket.color = sessionSocketIcon.color = Color.red;
                    break;
            }
        }
    }
}