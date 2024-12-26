using MatchIt.Player.Script;
using MatchIt.Script.Event;
using MatchIt.Script.Network;

public class LobbySocket : GameSocket
{
    public static LobbySocket Instance { get; private set; }

    private string _socketURL = "ws://localhost:3000/lobby";
    // private string _socketURL = "ws://match-it-env.eba-hf3mwhfn.eu-north-1.elasticbeanstalk.com/session";

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
        EventPub.Emit(PlayEvent.OnLobbyConnected);
    }

    protected override void OnDisconnect(string errroMessage)
    {
        EventPub.Emit(PlayEvent.OnLobbyDisconnected);
    }

    protected override void ManageSocResp(string socResponse)
    {
        switch (socResponse)
        {
            case "lobbyJoined":
                EventPub.Emit(PlayEvent.OnLobbyJoined);
                break;
            case "sessionPaired":
                SaveData.SetItem("sessionToken",socResponse);
                EventPub.Emit(PlayEvent.OnSessionPaired);
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

public class SocMessage
{
    public string action;
    public string roomID;
    public string playerID;
    public string playerOne;
    public string playerTwo;
}