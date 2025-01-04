using IjaOrisha.Player.Script;
using TMPro;
using UnityEngine;

namespace IjaOrisha.UI.Script.Lobby
{
    public class WaitArea : MonoBehaviour
    {
        [SerializeField] private RectTransform lobby;

        [SerializeField] private TextMeshProUGUI playerOneText;

        [SerializeField] private RectTransform playerTwoRect;
        [SerializeField] private TextMeshProUGUI playerTwoText;

        [SerializeField] private RectTransform battleButton;
        [SerializeField] private TextMeshProUGUI battleText;

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
                case PlayEvent.OnLobbyJoined:
                    lobby.gameObject.SetActive(true);
                    playerOneText.text = PlayerManager.Instance.PlayerOneID;
                    break;
                case PlayEvent.OnSessionPaired:
                    DisplayGamePlayers();
                    break;
            }
        }

        private void DisplayGamePlayers()
        {
            battleText.gameObject.SetActive(false);

            playerTwoText.text = PlayerManager.Instance.PlayerTwoID;
            playerTwoRect.gameObject.SetActive(true);

            battleButton.gameObject.SetActive(true);
        }
    }
}