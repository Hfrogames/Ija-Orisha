using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using MatchIt.Script.LocalDB;

namespace MatchIt.Player.Script.CardControl
{
    public class DeckLoader : MonoBehaviour
    {
        [SerializeField] private LocalDB playerCardDB;
        [SerializeField] private Transform cardSlot;
        [SerializeField] private int cardPreload;

        [SerializeField] private List<Card> cards;
        [SerializeField] private List<Transform> cardSlots;
        [SerializeField] private float delayBetweenCards = 0.5f; // Delay in seconds

        private void Start()
        {
            cards = new List<Card>();
            cardSlots = new List<Transform>();
            Reveal();
        }

        public float delayBetween = .2f;

        public void Reveal()
        {
            for (int i = 0; i < cardPreload; i++)
            {
                Transform slot = Instantiate(cardSlot, transform);
                Card cardComponent = slot.GetComponentInChildren<Card>();
                CardSO cardSO = playerCardDB.GetRandom();
                slot.transform.name = $"slot_{i}";
                cardComponent.SetCardSo(cardSO);
                cardComponent.transform.name = $"{cardSO.CardID}_{i}";
                cards.Add(cardComponent);
                cardSlots.Add(slot);

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
            cards.Remove(selectedCard);
            cardSlots.Remove(selectedCard._cachedDropZone.transform);
            selectedCard._cachedDropZone.gameObject.SetActive(false);

            Destroy(selectedCard._cachedDropZone.gameObject);
        }

        public void AppendCard(Card selectedCard)
        {
            cards.Add(selectedCard);
            Transform slot = RevealHiddenSlot();
            selectedCard._cachedDropZone = slot.GetComponent<DropZone>();
        }

        private Transform RevealHiddenSlot()
        {
            Transform slot = Instantiate(cardSlot, transform);
            Destroy(slot.transform.GetChild(0).gameObject);
            cardSlots.Add(slot);
            return slot;
        }

        private int _hoveredIndex = 1;
        public float moveDuration = .3f;

        public void Rearrange(Card selectedCard, DropZone hoveredZone)
        {
            int hoveredIndex = hoveredZone.transform.GetSiblingIndex();
            int removedIndex = selectedCard._cachedDropZone.transform.GetSiblingIndex();
            if (hoveredIndex == _hoveredIndex || hoveredIndex == removedIndex) return;
            _hoveredIndex = hoveredIndex;

            // move card to new slots
            Card removedObject = cards[removedIndex];
            cards.RemoveAt(removedIndex);

            // Insert it at the hoveredIndex
            cards.Insert(hoveredIndex, removedObject);
            TweenArrange(selectedCard, hoveredZone);
        }

        private void TweenArrange(Card selectedCard = null, DropZone hoveredZone = null)
        {
            // rearrange card according to list
            for (int i = 0; i < cards.Count; i++)
            {
                if (selectedCard && hoveredZone)
                    if (cards[i] == selectedCard)
                    {
                        cards[i]._cachedDropZone = hoveredZone;
                        continue;
                    }

                var targetSlot = cardSlots[i];
                var currentCard = cards[i];

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