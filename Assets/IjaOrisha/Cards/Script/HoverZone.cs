using DG.Tweening;
using UnityEngine;

namespace IjaOrisha
{
    public class HoverZone : MonoBehaviour
    {
        [SerializeField] private CardLoader zoneCard;

        private CardLoader _hoverCard;

        public void SetHover(CardLoader cardLoader)
        {
            _hoverCard = cardLoader;
            _hoverCard.SetCardSo(cardLoader.CardSo);
        }

        public void CopyCard()
        {
            if (!_hoverCard) return;
            zoneCard.gameObject.SetActive(false);
            _hoverCard.gameObject.SetActive(false);
            zoneCard.transform.position = _hoverCard.transform.position;

            DOTween.Sequence()
                .AppendCallback(() => zoneCard.gameObject.SetActive(true))
                .Append(zoneCard.transform.DOLocalMove(Vector3.zero, 0.5f))
                .Join(zoneCard.transform.DOScale(Vector3.one * 4f, 0.5f))
                .AppendInterval(.3f);
        }

        public void ReturnCopy()
        {
            if (!_hoverCard) return;
            DOTween.Sequence()
                .Append(zoneCard.transform.DOMove(_hoverCard.transform.position, 0.5f))
                .Join(zoneCard.transform.DOScale(Vector3.one, .5f))
                .AppendCallback(() =>
                {
                    zoneCard.gameObject.SetActive(false);
                    _hoverCard.gameObject.SetActive(true);
                    _hoverCard = null;
                });
        }
    }
}