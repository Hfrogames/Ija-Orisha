using DG.Tweening;
using TMPro;
using UnityEngine;


namespace IjaOrisha
{
    public class BattlePoint : MonoBehaviour
    {
        [SerializeField] private RectTransform background;
        [SerializeField] private TextMeshProUGUI pointText;

        public int currentPoint { get; private set; }

        public Sequence SetPoint(int pointValue)
        {
            currentPoint = pointValue;
            return DOTween.Sequence()
                .JoinCallback(() =>
                {
                    transform.localScale = Vector3.one * 0.6f;
                    pointText.text = pointValue.ToString();
                    gameObject.SetActive(true);
                })
                .Append(background.DOScale(Vector3.one, 0.3f).From(Vector3.zero).SetEase(Ease.InOutBack))
                .Append(pointText.transform.DOScale(Vector3.one, 0.2f).From(Vector3.zero).SetEase(Ease.OutBack));
        }

        public Sequence UpdatePoint(int pointValue)
        {
            currentPoint = pointValue;
            return DOTween.Sequence()
                .Append(pointText.transform.DOScale(Vector3.one, 0.2f).From(Vector3.zero).SetEase(Ease.OutBack))
                .JoinCallback(() => { pointText.text = pointValue.ToString(); });
        }
    }
}