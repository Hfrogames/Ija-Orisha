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
            flip.ResetPose();
            transform.localScale = Vector3.one * .8f;
            cardLoader.transform.localScale = Vector3.one * 1.5f;
            cardLoader.transform.localPosition = Vector3.zero;

            if (!cardSo) return;

            cardLoader.SetCardSo(cardSo);
            cardLoader.HidePoint(hideDefence);
            _cardSo = cardSo;
        }

        public Sequence ShowCard()
        {
            return _cardSo ? flip.Flip(.1f) : flip.Flip(.1f, true);
        }

        public Sequence HideCard()
        {
            Sequence hideSq = DOTween.Sequence();
            hideSq.Append(transform.DOScale(Vector3.zero, .1f));

            return hideSq;
        }

        public void SoundFX()
        {
            if (!_cardSo || _cardSo.CardID == CardType.Spell) return;

            if (!_audioManager) _audioManager = AudioManager.Instance;
            _audioManager.PlaySound(_cardSo.AttackSFX);
        }
    }
}