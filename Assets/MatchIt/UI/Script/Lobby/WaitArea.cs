using MatchIt.Player.Script;
using TMPro;
using UnityEngine;
using MatchIt.Script.Event;

namespace MatchIt.UI.Script.Lobby
{
    public class WaitArea : MonoBehaviour
    {
        [SerializeField] private RectTransform waitArea;

        [SerializeField] private RectTransform playerOneRect;
        [SerializeField] private TextMeshProUGUI playerOneText;

        [SerializeField] private RectTransform playerTwoRect;
        [SerializeField] private TextMeshProUGUI playerTwoText;

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
                    playerOneText.text = PlayerManager.Instance.PlayerID;
                    break;
                case PlayEvent.OnSessionPaired:
                    DisplayPlayerTwo();
                    break;
            }
        }

        private void DisplayPlayerTwo()
        {
            string sessionToken = SaveData.GetItemString("sessionToken");
            SocMessage socResp = JsonUtility.FromJson<SocMessage>(sessionToken);
            playerTwoRect.gameObject.SetActive(true);
            playerTwoText.text = socResp.playerTwo;
        }
    }
}