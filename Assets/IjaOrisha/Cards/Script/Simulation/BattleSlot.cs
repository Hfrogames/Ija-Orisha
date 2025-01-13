using DG.Tweening;
using UnityEngine;

namespace IjaOrisha
{
    public class BattleSlot : MonoBehaviour
    {
        public CardFlip flip;
        public CardLoader cardLoader;

        private AudioManager _audioManager;

        private CardSO _cardSo;

        public void LoadCard(CardSO cardSo, bool hideDefence)
        {
            _cardSo = null;
            flip.ResetPose();
            transform.localScale = Vector3.one * .8f;
            cardLoader.transform.localScale = Vector3.one * 1.5f;
            cardLoader.transform.localPosition = Vector3.zero;

            if (!cardSo) return;

            _cardSo = cardSo;
            cardLoader.SetCardSo(_cardSo);
            cardLoader.HidePoint(hideDefence);
        }

        public Sequence ShowCard()
        {
            return _cardSo ? flip.Flip(.1f) : flip.Flip(.1f, true);
        }

        public Tween HideCard()
        {
            return transform.DOScale(Vector3.zero, .1f)
                .OnComplete(() => { _cardSo = null; });
        }

        public void PlaySoundFX(SoundFX effect)
        {
            if (!_cardSo || _cardSo.CardID == CardType.Spell) return;

            if (!_audioManager) _audioManager = AudioManager.Instance;

            switch (effect)
            {
                case SoundFX.Attack:
                    _audioManager.PlaySound(_cardSo.AttackSFX);
                    break;
                case SoundFX.Defence:
                    _audioManager.PlaySound(_cardSo.DefenceSFX);
                    break;
                case SoundFX.Spell:
                    _audioManager.PlaySound(_cardSo.SpellSFX);
                    break;
            }
        }
    }

    public enum SoundFX
    {
        Attack,
        Defence,
        Spell
    }
}