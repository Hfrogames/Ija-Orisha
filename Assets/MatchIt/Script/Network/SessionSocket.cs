using MatchIt.Player.Script;
using UnityEngine;
using NativeWebSocket;
using MatchIt.Script.Event;

namespace MatchIt.Script.Network
{
    public class SessionSocket : GameSocket
    {
        public static SessionSocket Instance { get; private set; }

        // private string _socketURL = "ws://localhost:3000/session";

        private string _socketURL = "ws://match-it-env.eba-hf3mwhfn.eu-north-1.elasticbeanstalk.com/session";
        public SocMessage JoinData { get; private set; }

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
            // Connect();
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
                case "sessionJoined":
                    EventPub.Emit(PlayEvent.OnSessionJoined);
                    break;
                case "sessionStart":
                    EventPub.Emit(PlayEvent.OnFormationStart);
                    break;
            }
        }

        public void SetJoinData(SocMessage joinData)
        {
            JoinData = new SocMessage()
            {
                action = "join",
                playerID = PlayerManager.Instance.PlayerID,
                roomID = joinData.roomID,
                playerOne = joinData.playerOne,
                playerTwo = joinData.playerTwo
            };
        }

        public void Join()
        {
            SendWebSocketMessage(JoinData);
        }
    }
}