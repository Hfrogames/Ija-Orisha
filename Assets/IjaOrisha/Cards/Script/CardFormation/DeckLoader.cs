using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


namespace IjaOrisha
{
    public class DeckLoader : MonoBehaviour
    {
        [SerializeField] private LocalDB orishaCardDB;
        [SerializeField] private LocalDB spellCardDB;
        [SerializeField] private Transform cardSlot;
        [SerializeField] private float delayBetween = .2f;
        [SerializeField] private float moveDuration = .3f;


        private List<CardLoader> _cards = new List<CardLoader>();
        private List<Transform> _cardSlots = new List<Transform>();

        private int _orishaCardCount = 0;
        private int _spellCardCount = 0;

        private int _maxOrishaCard = 4;
        private int _maxSpellCard = 2;

        private int _hoveredIndex = 1;

        public void Reveal()
        {
            if (_cards.Count < 6)
            {
                int neededCard = _maxOrishaCard - _orishaCardCount;
                int neededSpell = _maxSpellCard - _spellCardCount;

                Debug.Log(neededCard + " " + neededSpell);

                SpawnCard(CardType.Card, neededCard);
                SpawnCard(CardType.Spell, neededSpell);
            }
        }

        private void SpawnCard(CardType cardType, int amount)
        {
            LocalDB cardDB = cardType == CardType.Card ? orishaCardDB : spellCardDB;
            if (cardDB.Count == 0 || amount <= 0) return;
            if (amount > cardDB.Count) amount = cardDB.Count;

            for (int i = 0; i < amount; i++)
            {
                CardSO cardSo = cardDB.GetRandom();
                Transform slot = Instantiate(cardSlot, transform);
                CardLoader cardLoaderComponent = slot.GetComponentInChildren<CardLoader>();
                cardLoaderComponent.SetCardSo(cardSo);
                slot.transform.name = $"slot_{i}";
                cardLoaderComponent.transform.name = $"{cardSo.CardID}_{i}";

                _cards.Add(cardLoaderComponent);
                _cardSlots.Add(slot);

                UpdateCardCount(cardSo, true);

                Sequence fanSequence = DOTween.Sequence();
                fanSequence
                    .Join(cardLoaderComponent.transform.DOLocalMove(new Vector3(1000, 0, 0), .25f).From()
                        .SetEase(Ease.OutBack).SetDelay(i * delayBetween))
                    .Append(cardLoaderComponent.transform.DOLocalRotate(new Vector3(0, 0, -30), .25f).From()
                        .SetEase(Ease.OutBack));
            }
        }

        private void UpdateCardCount(CardSO cardToCount, bool isAdding)
        {
            int modifier = isAdding ? 1 : -1;

            if (cardToCount.CardID == CardType.Card)
            {
                _orishaCardCount += modifier;
            }
            else
            {
                _spellCardCount += modifier;
            }
        }

        public void RemoveCard(CardLoader selectedCardLoader)
        {
            _cards.Remove(selectedCardLoader);
            _cardSlots.Remove(selectedCardLoader.CachedDropZone.transform);
            selectedCardLoader.CachedDropZone.gameObject.SetActive(false);
            UpdateCardCount(selectedCardLoader.CardSo, false);
            Destroy(selectedCardLoader.CachedDropZone.gameObject);
        }

        public void AppendCard(CardLoader selectedCardLoader)
        {
            _cards.Add(selectedCardLoader);
            Transform slot = RevealHiddenSlot();
            UpdateCardCount(selectedCardLoader.CardSo, true);
            selectedCardLoader.Release(slot.GetComponent<DropZone>());
        }

        private Transform RevealHiddenSlot()
        {
            Transform slot = Instantiate(cardSlot, transform);
            Destroy(slot.transform.GetChild(0).gameObject);
            _cardSlots.Add(slot);
            return slot;
        }

        public void Rearrange(CardLoader selectedCardLoader, DropZone hoveredZone)
        {
            int hoveredIndex = hoveredZone.transform.GetSiblingIndex();
            int removedIndex = selectedCardLoader.CachedDropZone.transform.GetSiblingIndex();
            if (hoveredIndex == _hoveredIndex || hoveredIndex == removedIndex) return;
            _hoveredIndex = hoveredIndex;

            // move card to new slots
            CardLoader removedObject = _cards[removedIndex];
            _cards.RemoveAt(removedIndex);

            // Insert it at the hoveredIndex
            _cards.Insert(hoveredIndex, removedObject);
            TweenArrange(selectedCardLoader, hoveredZone);
        }

        private void TweenArrange(CardLoader selectedCardLoader = null, DropZone hoveredZone = null)
        {
            // rearrange card according to list
            for (int i = 0; i < _cards.Count; i++)
            {
                if (selectedCardLoader && hoveredZone)
                    if (_cards[i] == selectedCardLoader)
                    {
                        _cards[i].Release(hoveredZone);
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