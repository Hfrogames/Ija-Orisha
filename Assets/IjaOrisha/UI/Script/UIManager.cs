using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IjaOrisha.UI.Script
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Image formationCountDownImg;

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
                case PlayEvent.OnSessionStart:
                    formationCountDownImg.DOFillAmount(0, 2);
                    break;
                case PlayEvent.OnFormationEnd:
                    break;
            }
        }
    }
}