using DG.Tweening;
using UnityEngine;
using TMPro;

namespace IjaOrisha.UI.Script.Session
{
    public class SessionNetwork : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI statusText;
        [SerializeField] private GameObject sessionPanel;

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
            }
        }
    }
}