using System.Collections.Generic;
using DG.Tweening;
using IjaOrisha.Script.LocalDB;
using UnityEngine;

namespace IjaOrisha.Cards.Script.CardControl
{
    public class DeckLoader : MonoBehaviour
    {
        [SerializeField] private LocalDB playerCardDB;
        [SerializeField] private Transform cardSlot;
        [SerializeField] private int cardPreload;
        [SerializeField] private float delayBetween = .2f;
        [SerializeField] private float moveDuration = .3f;


        private List<Card> _cards = new List<Card>();
        private List<Transform> _cardSlots = new List<Transform>();
        private int _hoveredIndex = 1;

        private void Start()
        {
            Reveal();
        }


        public void Reveal()
        {
            for (int i = 0; i < cardPreload; i++)
            {
                Transform slot = Instantiate(cardSlot, transform);
                Card cardComponent = slot.GetComponentInChildren<Card>();
                CardSO cardSo = playerCardDB.GetRandom();
                slot.transform.name = $"slot_{i}";
                cardComponent.SetCardSo(cardSo);
                cardComponent.transform.name = $"{cardSo.CardID}_{i}";
                _cards.Add(cardComponent);
                _cardSlots.Add(slot);

                Sequence fanSequence = DOTween.Sequence();
                fanSequence
                    .Join(cardComponent.transform.DOLocalMove(new Vector3(1000, 0, 0), .25f).From()
                        .SetEase(Ease.OutBack).SetDelay(i * delayBetween))
                    .Append(cardComponent.transform.DOLocalRotate(new Vector3(0, 0, -30), .25f).From()
                        .SetEase(Ease.OutBack));
            }
        }

        public void RemoveCard(Card selectedCard)
        {
            _cards.Remove(selectedCard);
            _cardSlots.Remove(selectedCard._cachedDropZone.transform);
            selectedCard._cachedDropZone.gameObject.SetActive(false);

            Destroy(selectedCard._cachedDropZone.gameObject);
        }

        public void AppendCard(Card selectedCard)
        {
            _cards.Add(selectedCard);
            Transform slot = RevealHiddenSlot();
            selectedCard._cachedDropZone = slot.GetComponent<DropZone>();
        }

        private Transform RevealHiddenSlot()
        {
            Transform slot = Instantiate(cardSlot, transform);
            Destroy(slot.transform.GetChild(0).gameObject);
            _cardSlots.Add(slot);
            return slot;
        }


        public void Rearrange(Card selectedCard, DropZone hoveredZone)
        {
            int hoveredIndex = hoveredZone.transform.GetSiblingIndex();
            int removedIndex = selectedCard._cachedDropZone.transform.GetSiblingIndex();
            if (hoveredIndex == _hoveredIndex || hoveredIndex == removedIndex) return;
            _hoveredIndex = hoveredIndex;

            // move card to new slots
            Card removedObject = _cards[removedIndex];
            _cards.RemoveAt(removedIndex);

            // Insert it at the hoveredIndex
            _cards.Insert(hoveredIndex, removedObject);
            TweenArrange(selectedCard, hoveredZone);
        }

        private void TweenArrange(Card selectedCard = null, DropZone hoveredZone = null)
        {
            // rearrange card according to list
            for (int i = 0; i < _cards.Count; i++)
            {
                if (selectedCard && hoveredZone)
                    if (_cards[i] == selectedCard)
                    {
                        _cards[i]._cachedDropZone = hoveredZone;
                        continue;
                    }

                var targetSlot = _cardSlots[i];
                var currentCard = _cards[i];

                // First tween to target world position
                currentCard.transform
                    .DOMove(targetSlot.position, moveDuration)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() =>
                    {
                        // Only change parent after movement is complete
                        currentCard.transform.SetParent(targetSlot);
                        currentCard.transform.localPosition = Vector3.zero;
                    });
            }
        }
    }
}