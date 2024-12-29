using System;
using MatchIt.Script.Event;
using UnityEngine;
using NativeWebSocket;

public class GameSocket : MonoBehaviour
{
    private WebSocket _webSocket;

    private void Update()
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


    protected async void OpenConn(string socketURL)
    {
        _webSocket = new WebSocket(socketURL);
        _webSocket.OnOpen += () => OnConnect();

        _webSocket.OnError += (OnDisconnect);

        _webSocket.OnClose += (OnDisconnect);

        _webSocket.OnMessage += ByteToSocResponse;

        await _webSocket.Connect();
    }

    private void ByteToSocResponse(byte[] bytes)
    {
        var message = System.Text.Encoding.UTF8.GetString(bytes);

        Debug.Log(message);

        SocMessage socResponse = JsonUtility.FromJson<SocMessage>(message);

        OnMessage(socResponse);
    }

    public async void CloseConn()
    {
        if (_webSocket != null && _webSocket.State == WebSocketState.Open)
        {
            await _webSocket.Close();
        }
    }

    public async void SendWebSocketMessage(SocMessage socMessage)
    {
        if (_webSocket.State == WebSocketState.Open)
        {
            string message = JsonUtility.ToJson(socMessage);
            // Sending plain text
            await _webSocket.SendText(message);
        }
    }

    protected virtual void OnConnect()
    {
        EventPub.Emit(PlayEvent.OnLobbyConnected);
    }

    protected virtual void OnDisconnect(string errorMessage)
    {
        EventPub.Emit(PlayEvent.OnLobbyDisconnected);
    }

    protected virtual void OnDisconnect(WebSocketCloseCode errorMessage)
    {
        EventPub.Emit(PlayEvent.OnLobbyDisconnected);
    }

    protected virtual void OnMessage(SocMessage socResponse)
    {
    }
}

[Serializable]
public class SocMessage
{
    public string action;
    public string roomID;
    public string playerID;
    public string playerOne;
    public string playerTwo;
    public BattleData playerOneBD;
    public BattleData playerTwoBD;
    public int roundTimeout;
    public int currentRound;
    public int totalRounds;

    public string Get()
    {
        return JsonUtility.ToJson(this);
    }
}
