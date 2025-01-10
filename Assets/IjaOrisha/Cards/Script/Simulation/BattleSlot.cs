using DG.Tweening;
using UnityEngine;

namespace IjaOrisha
{
    public class BattleSlot : MonoBehaviour
    {
        public CardFlip flip;
        public CardLoader cardLoader;
        [SerializeField] private bool isCardSo;

        public void LoadCard(CardSO cardSo, bool hideDefence)
        {
            flip.ResetPose();
            transform.localScale = Vector3.one * .8f;
            cardLoader.transform.localScale = Vector3.one * 1.5f;
            cardLoader.transform.localPosition = Vector3.zero;

            if (!cardSo)
            {
                isCardSo = false;
                return;
            }

            cardLoader.SetCardSo(cardSo);
            cardLoader.HidePoint(hideDefence);
            isCardSo = true;
        }

        public Sequence ShowCard()
        {
            return isCardSo ? flip.Flip(.1f) : flip.Flip(.1f, true);
        }

        public Sequence HideCard()
        {
            Sequence hideSq = DOTween.Sequence();
            hideSq.Append(transform.DOScale(Vector3.zero, .1f));

            return hideSq;
        }
    }
}