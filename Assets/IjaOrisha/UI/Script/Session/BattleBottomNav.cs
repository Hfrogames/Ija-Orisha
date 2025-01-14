using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IjaOrisha
{
    public class BattleBottomNav : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image formationTimeout;
        [SerializeField] private TextMeshProUGUI battleInfo;

        private Tween _formationFill;

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
                    _formationFill = formationTimeout.DOFillAmount(1, BattlePlayer.RoundTimeout)
                        .From();
                    DOVirtual.DelayedCall(BattlePlayer.RoundTimeout, EmitFormationEnd);
                    break;
                case PlayEvent.OnFormationEnd:
                    _formationFill.Complete();
                    formationTimeout.fillAmount = 0;
                    button.enabled = false;
                    battleInfo.text = "Processing";
                    break;
                case PlayEvent.OnSimulationStart:
                    battleInfo.text = "Loading";
                    break;
                case PlayEvent.OnSimulationEnd:
                    // battleInfo.text = "Battle";
                    break;
                case PlayEvent.OnSessionEnd:
                    battleInfo.text = "Reload";
                    break;
            }
        }

        private void OnClick()
        {
            EmitFormationEnd();
        }

        private void EmitFormationEnd()
        {
            if (EventSub.InFormation)
            {
                EventPub.Emit(PlayEvent.OnFormationEnd);
                EventPub.Emit(PlayEvent.OnFormationSubmit);
                button.enabled = false;
            }
        }
    }
}