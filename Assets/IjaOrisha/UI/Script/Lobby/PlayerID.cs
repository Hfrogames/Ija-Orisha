using IjaOrisha.Player.Script;
using TMPro;
using UnityEngine;

namespace IjaOrisha.UI.Script.Lobby
{
    public class PlayerID : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playerOneID;
        [SerializeField] private RectTransform playerTwoRect;
        [SerializeField] private TextMeshProUGUI playerTwoID;

        private void OnEnable()
        {
            EventPub.OnPlayEvent += OnPlayEvent;

            playerOneID.text = PlayerManager.Instance.PlayerOneID;
        }

        private void OnDisable()
        {
            EventPub.OnPlayEvent -= OnPlayEvent;
        }

        private void OnPlayEvent(PlayEvent playEvent)
        {
            switch (playEvent)
            {
                case PlayEvent.OnSessionPaired:
                    Debug.Log("display player data");
                    playerTwoRect.gameObject.SetActive(true);
                    playerTwoID.text = PlayerManager.Instance.PlayerTwoID;
                    break;
            }
        }
    }
}