using DG.Tweening;
using MatchIt.Player.Script;
using MatchIt.Script.Event;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MatchIt.UI.Script
{
    public class BattleMessage : MonoBehaviour
    {
        [SerializeField] private Image formationCountDownImg;
        [SerializeField] private TextMeshProUGUI battleInfo;

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
                case PlayEvent.OnFormationStart:
                    battleInfo.text = "Formation";
                    formationCountDownImg.DOFillAmount(1, BattleManager.Instance.FormationTimeout).From();
                    break;
                case PlayEvent.OnFormationEnd:
                    formationCountDownImg.fillAmount = 0;
                    battleInfo.text = "Sending data";
                    break;
                case PlayEvent.OnBattleStart:
                    battleInfo.text = "Battle Started";
                    break;
                case PlayEvent.OnBattleWin:
                    battleInfo.text = "Battle";
                    break;
                case PlayEvent.OnSessionEnd:
                    battleInfo.text = "Reload";
                    break;
            }
        }
    }
}