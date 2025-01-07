using DG.Tweening;
using TMPro;
using UnityEngine;


namespace IjaOrisha
{
    public class LobbyNetwork : MonoBehaviour
    {
        [SerializeField] private RectTransform loaderRect;
        [SerializeField] private TextMeshProUGUI lobbyText;

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
                case PlayEvent.OnLobbyDisconnected:
                    loaderRect.gameObject.SetActive(true);
                    break;
                case PlayEvent.OnLobbyConnected:
                    lobbyText.text = "Connected to server";
                    DOVirtual.DelayedCall(3f, () =>
                    {
                        gameObject.SetActive(false);
                        EventPub.Emit(PlayEvent.OnStart);
                    });
                    break;
            }
        }
    }
}