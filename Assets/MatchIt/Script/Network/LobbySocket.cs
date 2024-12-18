using MatchIt.Script.Event;
using MatchIt.Script.Utils;
using UnityEngine;
using NativeWebSocket;

public class LobbySocket : MonoBehaviour
{
    public static LobbySocket Instance { get; private set; }

    private WebSocket _webSocket;

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
        OpenConn();
    }

    void Update()
    {
        DispatchMessage();
    }

    private void DispatchMessage()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        if (_webSocket != null)
            _webSocket.DispatchMessageQueue();
#endif
    }

    private async void OnApplicationQuit()
    {
        await _webSocket.Close();
    }


    public async void OpenConn()
    {
        _webSocket = new WebSocket("ws://localhost:3000/lobby");

        // _webSocket = new WebSocket("ws://match-it-env.eba-hf3mwhfn.eu-north-1.elasticbeanstalk.com");
        _webSocket.OnOpen += () => { EventPub.Emit(PlayEvent.OnLobbyConnected); };

        _webSocket.OnError += (e) => { EventPub.Emit(PlayEvent.OnLobbyDisconnected); };

        _webSocket.OnClose += (e) => { EventPub.Emit(PlayEvent.OnLobbyDisconnected); };

        _webSocket.OnMessage += (bytes) =>
        {
            var message = System.Text.Encoding.UTF8.GetString(bytes);

            Debug.Log(message);

            SocMessage socResponse = JsonUtility.FromJson<SocMessage>(message);

            ManageSocResp(socResponse.action);
        };

        await _webSocket.Connect();
    }

    private async void SendWebSocketMessage(SocMessage socMessage)
    {
        if (_webSocket.State == WebSocketState.Open)
        {
            string message = JsonUtility.ToJson(socMessage);
            // Sending plain text
            await _webSocket.SendText(message);
        }
    }

    private void ManageSocResp(string socResponse)
    {
        switch (socResponse)
        {
            case "lobbyJoined":
                EventPub.Emit(PlayEvent.OnLobbyJoined);
                break;
            case "sessionPaired":
                EventPub.Emit(PlayEvent.OnSessionPaired);
                break;
        }
    }

    public void Join()
    {
        SocMessage joinData = new SocMessage()
        {
            action = "join",
            playerID = "playerID"
        };
        SendWebSocketMessage(joinData);
    }
}

public class SocMessage
{
    public string action;
    public string roomID;
    public string playerID = PlayerID.GenerateRandomString(6);
    public string playerOne;
    public string playerTwo;
}