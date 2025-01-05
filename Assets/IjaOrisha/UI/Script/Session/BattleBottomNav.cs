using DG.Tweening;
using IjaOrisha.Cards.Script;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace IjaOrisha.UI.Script.Session
{
    public class BattleBottomNav : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image formationTimeout;
        [SerializeField] private TextMeshProUGUI battleInfo;

        private void OnEnable()
        {
            EventPub.OnPlayEvent += OnPlayEvent;
            button.onClick.AddListener(OnClick);
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
                    button.enabled = true;
                    battleInfo.text = "Submit";
                    formationTimeout.DOFillAmount(1, 20)
                        .From()
                        .OnComplete(EmitFormationEnd);
                    break;
                case PlayEvent.OnFormationEnd:
                    formationTimeout.fillAmount = 0;
                    button.enabled = false;
                    battleInfo.text = "Processing";
                    break;
                case PlayEvent.OnBattleStart:
                    battleInfo.text = "Loading";
                    break;
                case PlayEvent.OnBattleWin:
                    // battleInfo.text = "Battle";
                    break;
                case PlayEvent.OnSessionEnd:
                    battleInfo.text = "Reload";
                    break;
            }
        }

        private void EmitFormationEnd()
        {
            if (EventSub.InFormation)
            {
                EventPub.Emit(PlayEvent.OnFormationEnd);
            }
        }

        private void OnClick()
        {
            EmitFormationEnd();
        }
    }
}