using DG.Tweening;
using TMPro;
using UnityEngine;

namespace IjaOrisha
{
    public class BattleInfo : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textInfo;

        public Sequence Round()
        {
            string info = BattlePlayer.CurrentRound < BattlePlayer.MaxRound
                ? $"Round \n{BattlePlayer.CurrentRound}"
                : "Final";
            return TextInfo(info);
        }

        public Sequence Announce(string info)
        {
            return TextInfo(info);
        }

        private void SetText()
        {
            textInfo.gameObject.SetActive(false);
            textInfo.transform.localScale = Vector3.zero;
            textInfo.transform.localPosition = new Vector3(0, 300, 0);
        }

        private Sequence TextInfo(string info)
        {
            return DOTween.Sequence()
                    .OnStart(SetText)
                    .SetEase(Ease.OutBack)
                    .AppendCallback(() =>
                    {
                        textInfo.text = info;
                        textInfo.gameObject.SetActive(true);
                    })
                    .Append(textInfo.transform.DOScale(1, .5f))
                    .AppendInterval(2)
                    .Append(textInfo.transform.DOLocalMove(new Vector3(-1500, 300, 0), .2f))
                ;
        }
    }
}