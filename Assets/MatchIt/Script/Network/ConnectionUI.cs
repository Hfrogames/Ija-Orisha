using MatchIt.Player.Script;
using MatchIt.Script.Event;
using UnityEngine;

namespace MatchIt.Script.Network
{
    public class ConnectionUI : MonoBehaviour
    {
        [SerializeField] private DemoPanel playerID;
        [SerializeField] private DemoPanel lobbySocket;
        [SerializeField] private DemoPanel joinLobby;
        [SerializeField] private DemoPanel joinGameSession;
        [SerializeField] private DemoPanel sessionSocket;
        [SerializeField] private DemoPanel matchStarted;

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
        }

        private void OnPlayEvent(PlayEvent playEvent)
        {
            switch (playEvent)
            {
                case PlayEvent.OnLobbyConnected:
                    lobbySocket.Success("connected");
                    joinLobby.gameObject.SetActive(true);
                    joinLobby.Normal("join");
                    break;
                case PlayEvent.OnLobbyDisconnected:
                    lobbySocket.Fail("closed");
                    break;
                case PlayEvent.OnLobbyJoined:
                    joinLobby.Success("joined");
                    break;
                case PlayEvent.OnSessionPaired:
                    joinGameSession.gameObject.SetActive(true);
                    joinGameSession.Normal("join");
                    SessionSocket.Instance.Connect();
                    LobbySocket.Instance.CloseConn();
                    joinLobby.Normal("closed");
                    break;
                case PlayEvent.OnSessionConnected:
                    sessionSocket.gameObject.SetActive(true);
                    sessionSocket.Success("connected");
                    break;
                case PlayEvent.OnSessionDisconnected:
                    sessionSocket.Success("offline");
                    break;
                case PlayEvent.OnSessionJoined:
                    joinGameSession.Success("joined");
                    break;
                case PlayEvent.OnFormationStart:
                    matchStarted.gameObject.SetActive(true);
                    SocMessage socData = SessionSocket.Instance.JoinData;
                    string matchDetails = $"{socData.playerOne} vs {socData.playerTwo}";
                    matchStarted.Success(matchDetails);
                    break;
            }
        }
    }
}