using IjaOrisha.Script.Network;
using TMPro;
using UnityEngine;

namespace IjaOrisha.UI.Script.Lobby
{
    public class WaitArea : MonoBehaviour
    {
        [SerializeField] private RectTransform waitArea;

        [SerializeField] private RectTransform playerOneRect;
        [SerializeField] private TextMeshProUGUI playerOneText;

        [SerializeField] private RectTransform playerTwoRect;
        [SerializeField] private TextMeshProUGUI playerTwoText;

        [SerializeField] private TextMeshProUGUI vs;
        [SerializeField] private RectTransform battleButton;

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
                    waitArea.gameObject.SetActive(true);
                    break;
                case PlayEvent.OnSessionPaired:
                    DisplayGamePlayers();
                    break;
            }
        }

        private void DisplayGamePlayers()
        {
            string sessionToken = SaveData.GetItemString("sessionToken");
            SocMessage socResp = JsonUtility.FromJson<SocMessage>(sessionToken);

            playerOneRect.gameObject.SetActive(true);
            playerOneText.text = socResp.playerOne;

            vs.text = "vs";

            playerTwoRect.gameObject.SetActive(true);
            playerTwoText.text = socResp.playerTwo;

            battleButton.gameObject.SetActive(true);
        }
    }
}