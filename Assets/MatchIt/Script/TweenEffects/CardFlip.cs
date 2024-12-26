using DG.Tweening;
using UnityEngine;

namespace MatchIt.Script.TweenEffects
{
    public class CardFlip : MonoBehaviour
    {
        [SerializeField] private RectTransform front;
        [SerializeField] private RectTransform back;

        public Sequence Flip()
        {
            Sequence flipSequence = DOTween.Sequence();

            flipSequence
                .Join(back.DORotate(new Vector3(0, 90, 0), 0.25f).SetEase(Ease.InSine))
                .AppendCallback(() =>
                {
                    back.gameObject.SetActive(false);
                    front.gameObject.SetActive(true);
                })
                .Append(front.DORotate(new Vector3(0, 180, 0), 0.25f).SetEase(Ease.OutSine));

            return flipSequence;
        }
    }
}