
namespace IjaOrisha
{
    public class LobbySocket : GameSocket
    {
        public static LobbySocket Instance { get; private set; }

        private string _socketURL = "ws://localhost:3000/lobby";
        // private string _socketURL = "ws://match-it-env.eba-hf3mwhfn.eu-north-1.elasticbeanstalk.com/lobby";

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        public void Connect()
        {
            OpenConn(_socketURL);
        }

        protected override void OnConnect()
        {
            EventPub.Emit(PlayEvent.OnLobbyConnected);
        }

        protected override void OnDisconnect(string errroMessage)
        {
            EventPub.Emit(PlayEvent.OnLobbyDisconnected);
        }

        protected override void OnMessage(SocMessage socMessage)
        {
            string socResponse = socMessage.action;

            switch (socResponse)
            {
                case "lobbyJoined":
                    EventPub.Emit(PlayEvent.OnLobbyJoined);
                    break;
                case "sessionPaired":
                    BattlePlayer.SetPlayer(socMessage);
                    SaveData.SetItem("sessionToken", socMessage.Get());
                    EventPub.Emit(PlayEvent.OnSessionPaired, 2f);
                    break;
            }
        }

        public void Join()
        {
            SocMessage joinData = new SocMessage()
            {
                action = "join",
                playerID = PlayerManager.Instance.PlayerID
            };
            SendWebSocketMessage(joinData);
        }
    }
}