using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IjaOrisha
{
    public class WaitArea : MonoBehaviour
    {
        [SerializeField] private RectTransform lobby;

        [SerializeField] private TextMeshProUGUI playerOneText;

        [SerializeField] private RectTransform playerTwoRect;
        [SerializeField] private TextMeshProUGUI playerTwoText;

        [SerializeField] private Button battleButton;
        [SerializeField] private TextMeshProUGUI battleButtonText;
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
                    playerOneText.text = PlayerManager.Instance.PlayerID;
                    battleText.gameObject.SetActive(true);
                    break;
                case PlayEvent.OnSessionPaired:
                    DisplayGamePlayers();
                    break;
            }
        }

        private void DisplayGamePlayers()
        {
            battleText.gameObject.SetActive(false);

            playerTwoText.text = BattlePlayer.PlayerTwoID;
            playerTwoRect.gameObject.SetActive(true);

            battleButtonText.text = "Fight";
            battleButton.enabled = true;
            battleButton.gameObject.SetActive(true);
        }
    }
}