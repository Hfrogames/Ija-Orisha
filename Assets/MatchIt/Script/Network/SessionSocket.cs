using UnityEngine;
using NativeWebSocket;
using MatchIt.Script.Event;

namespace MatchIt.Script.Network
{
    public class SessionSocket : GameSocket
    {
        public static SessionSocket Instance { get; private set; }

        private string _socketURL = "ws://localhost:3000/session";

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            Connect();
        }

        public void Connect()
        {
            OpenConn(_socketURL);
        }

        protected override void OnConnect()
        {
            EventPub.Emit(PlayEvent.OnSessionConnected);
        }

        protected override void OnDisconnect()
        {
            EventPub.Emit(PlayEvent.OnSessionDisconnected);
        }

        protected override void ManageSocResp(string socResponse)
        {
            switch (socResponse)
            {
                case "sessionConnected":
                    EventPub.Emit(PlayEvent.OnLobbyJoined);
                    break;
                case "sessionStart":
                    EventPub.Emit(PlayEvent.OnSessionStart);
                    break;
            }
        }

        public void Join()
        {
            SocMessage joinData = new SocMessage()
            {
                action = "join",
                playerID = "playerID",
                roomID = "4567",
            };
            SendWebSocketMessage(joinData);
        }
    }
}