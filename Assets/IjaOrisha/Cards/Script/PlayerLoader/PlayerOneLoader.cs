using DG.Tweening;
using UnityEngine;

namespace IjaOrisha.Cards.Script.PlayerLoader
{
    public class PlayerOneLoader : PlayerLoader
    {
        public override void DisplayCardData()
        {
            base.DisplayCardData();

            CardDataSq
                // Attack Spell
                // .AppendCallback(() =>
                // {
                //     attackSpellFlip.gameObject.SetActive(true);
                //     attackSpellImg.sprite = _attackSpell?.CardSprite;
                // })
                // .Join(attackSpellFlip.transform.DOScale(1, 0.2f).From(0).SetEase(Ease.OutSine))

                // Attack Card
                .AppendCallback(() =>
                {
                    attackCardFlip.gameObject.SetActive(true);
                    attackCardImg.sprite = _attackCard?.CardSprite;
                })
                .Append(attackCardFlip.transform.DOScale(1, 0.2f).From(0).SetEase(Ease.OutSine))

                // Defence Card
                .AppendCallback(() =>
                {
                    defenceCardFlip.gameObject.SetActive(true);
                    defenceCardImg.sprite = _defenceCard?.CardSprite;
                })
                .Append(defenceCardFlip.transform.DOScale(1, 0.2f).From(0).SetEase(Ease.OutSine))

                // Defence Spell
                // .AppendCallback(() =>
                // {
                //     defenceSpellFlip.gameObject.SetActive(true);
                //     defenceSpellImg.sprite = _defenceSpell?.CardSprite;
                // })
                // .Join(defenceSpellFlip.transform.DOScale(1, 0.2f).From(0).SetEase(Ease.OutSine))
                .Append(defenceCardFlip.Flip())
                .Append(attackCardFlip.Flip())

                // .Append(defenceSpellFlip.Flip())
                // .Append(attackSpellFlip.Flip())

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