using IjaOrisha.Player.Script;
using UnityEngine;

namespace IjaOrisha.Script.Network
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
            Join();
        }

        protected override void OnDisconnect(string errorMessage)
        {
            EventPub.Emit(PlayEvent.OnSessionDisconnected);
        }

        protected override void OnMessage(SocMessage socMessage)
        {
            string socResponse = socMessage.action;

            switch (socResponse)
            {
                case "sessionJoined":
                    EventPub.Emit(PlayEvent.OnSessionJoined);
                    break;
                case "sessionStart":
                    EventPub.Emit(PlayEvent.OnSessionStart);
                    break;
                case "formationStart":
                case "formationEnd":
                case "battleData":
                case "sessionEnd":
                    EventPub.Emit(socMessage);
                    break;
            }
        }

        public void Join()
        {
            string joinDataString = SaveData.GetItemString("sessionToken");
            SocMessage joinData = JsonUtility.FromJson<SocMessage>(joinDataString);
            JoinData = new SocMessage()
            {
                action = "join",
                playerID = PlayerManager.Instance.PlayerID,
                roomID = joinData.roomID,
                playerOne = joinData.playerOne,
                playerTwo = joinData.playerTwo
            };
            SendWebSocketMessage(JoinData);
        }
    }
}