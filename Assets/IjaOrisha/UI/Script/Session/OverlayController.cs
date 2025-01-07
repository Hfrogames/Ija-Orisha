using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace IjaOrisha
{
    public class OverlayController : MonoBehaviour
    {
        [SerializeField] private Image overlayImg;

        [SerializeField] private GameObject sessionPanel;
        [SerializeField] private TextMeshProUGUI sessionInfo;

        [SerializeField] private GameObject successPanel;
        [SerializeField] private GameObject drawPanel;
        [SerializeField] private GameObject failedPanel;


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
                case PlayEvent.OnSessionConnected:
                    Debug.Log("connected please wait");
                    break;
                case PlayEvent.OnSessionJoined:
                    Debug.Log("waiting for opponent...");
                    break;
                case PlayEvent.OnSessionStart:
                    Debug.Log("Battle Ready...");
                    break;
                case PlayEvent.OnSessionWin:
                    overlayImg.enabled = true;
                    successPanel.SetActive(true);
                    break;
                case PlayEvent.OnSessionLose:
                    overlayImg.enabled = true;
                    failedPanel.SetActive(true);
                    break;
                case PlayEvent.OnSessionDraw:
                    overlayImg.enabled = true;
                    drawPanel.SetActive(true);
                    break;
            }
        }
    }
}