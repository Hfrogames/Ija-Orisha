using DG.Tweening;
using UnityEngine;

namespace IjaOrisha.Cards.Script.PlayerLoader
{
    public class PlayerTwoLoader : PlayerLoader
    {
        public override void DisplayCardData()
        {
            base.DisplayCardData();
            CardDataSq

                // Defence Card
                .AppendCallback(() =>
                {
                    defenceCardFlip.gameObject.SetActive(true);
                    defenceCardImg.sprite = _defenceCard?.CardSprite;
                })
                .Append(defenceCardFlip.transform.DOScale(1, 0.2f).From(0).SetEase(Ease.OutSine))

                // Attack Card
                .AppendCallback(() =>
                {
                    attackCardFlip.gameObject.SetActive(true);
                    attackCardImg.sprite = _attackCard?.CardSprite;
                })
                .Append(attackCardFlip.transform.DOScale(1, 0.2f).From(0).SetEase(Ease.OutSine))
                .Append(attackCardFlip.Flip())
                .Append(defenceCardFlip.Flip())

                // Optional: Add text animations for points
                .AppendCallback(() =>
                {
                    attackPoint.text = _attackCard?.AttackValue.ToString();
                    defencePoint.text = _defenceCard?.AttackValue.ToString();
                    attackPoint.transform.parent.localScale = Vector3.zero;
                    defencePoint.transform.parent.localScale = Vector3.zero;
                })
                .Join(attackPoint.transform.parent.DOScale(1, 0.2f).SetEase(Ease.OutSine))
                .Join(defencePoint.transform.parent.DOScale(1, 0.2f).SetEase(Ease.OutSine));
        }
    }
}