using DG.Tweening;
using IjaOrisha.Cards.Script.CardFormation;
using UnityEngine;

namespace IjaOrisha
{
    public class BattleSlot : MonoBehaviour
    {
        [SerializeField] private CardLoader cardLoader;
        [SerializeField] private CardFlip flip;
        [SerializeField] private bool isCardSo;

        public void LoadCard(CardSO cardSo, bool hideDefence)
        {
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