using DG.Tweening;

namespace MatchIt.Player.Script
{
    public class PlayerTwoLoader : PlayerLoader
    {
        public override void DisplayCardData()
        {
            Sequence displaySq = DOTween.Sequence();


            displaySq
                // Defence Spell
                .AppendCallback(() =>
                {
                    defenceSpellFlip.gameObject.SetActive(true);
                    defenceSpellImg.sprite = _defenceSpell?.CardSprite;
                })
                .Join(defenceSpellFlip.transform.DOScale(1, 0.2f).From(0).SetEase(Ease.OutSine))

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

                // Attack Spell
                .AppendCallback(() =>
                {
                    attackSpellFlip.gameObject.SetActive(true);
                    attackSpellImg.sprite = _attackSpell?.CardSprite;
                })
                .Join(attackSpellFlip.transform.DOScale(1, 0.2f).From(0).SetEase(Ease.OutSine))
                .Append(attackSpellFlip.Flip())
                .Append(attackCardFlip.Flip())
                .Append(defenceCardFlip.Flip())
                .Append(defenceSpellFlip.Flip())

                // Optional: Add text animations for points
                .Join(attackPoint.DOText(_attackCard?.AttackValue.ToString(), 0.5f))
                .Join(defencePoint.DOText(_defenceCard?.AttackValue.ToString(), 0.5f));

            // Optional: Add sequence completion callback if needed
            displaySq.OnComplete(() =>
            {
                // Any code to run after all animations are complete
            });
        }
    }
}