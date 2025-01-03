using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VInspector;

namespace IjaOrisha.Cards.Script.CardControl
{
    public class DropZone : MonoBehaviour
    {
        public bool IsLocked { get; private set; } = false;
        public Card DroppedCard { get; private set; }

        [SerializeField] public DropZones dropZones;

        private bool IsAttack => dropZones == DropZones.Attack;
        private bool IsDefence => dropZones == DropZones.Defence;
        private bool IsSpell => dropZones == DropZones.Spell;
        private bool IsDeck => dropZones == DropZones.Deck;


        [SerializeField] private DeckLoader deckLoader;

        [HideIf(nameof(IsDeck)), SerializeField]
        private CardType cardType;

        [HideIf(nameof(IsDeck)), SerializeField]
        private RectTransform dropBorder;

        [HideIf(nameof(IsDeck)), SerializeField]
        public RectTransform zoneBackG;


        private void OnEnable()
        {
            EventPub.OnPlayEvent += OnPlayEvent;
        }

        private void OnDisable()
        {
            EventPub.OnPlayEvent -= OnPlayEvent;
        }

        private void Start()
        {
            // if (!IsDeck) // test only
            IsLocked = false;

            if (!deckLoader) deckLoader = GetComponentInParent<DeckLoader>();
        }

        private void OnPlayEvent(PlayEvent playEvent)
        {
            switch (playEvent)
            {
                case PlayEvent.OnFormationStart:
                case PlayEvent.OnBattleStart:
                    if (DroppedCard)
                    {
                        DroppedCard.gameObject.SetActive(false);
                        DroppedCard = null;
                    }

                    transform.parent.localScale = Vector3.one;
                    IsLocked = false;
                    break;
                case PlayEvent.OnFormationEnd:
                    if (DroppedCard)
                        DroppedCard.isLocked = true;
                    IsLocked = true;
                    break;
            }
        }

        public void OnHover(Card item = null)
        {
            ArrangeDeck(item);

            if (!IsDeck && item.CardID == cardType && !DroppedCard)
                dropBorder.gameObject.SetActive(true);
        }

        public void OnHoverOut()
        {
            if (IsDeck) return;
            dropBorder.gameObject.SetActive(false);
        }

        public bool CanDrop(Card item)
        {
            if (IsDeck) return true;

            if (item.CardID != cardType || DroppedCard) return false;

            if (item.CardID == cardType) DroppedCard = item;

            if (DroppedCard)
                zoneBackG.gameObject.SetActive(false);

            return true;
        }

        public void OnDrop(Card card)
        {
            card.transform.localScale = Vector3.one;
            SetCardPos(card);
            // SetSpellPos(card);
            SetDeckParent(card);
        }

        public void OnRemove(Card item)
        {
            if (item.CardID == cardType) DroppedCard = null;

            if (!IsDeck)
            {
                deckLoader.AppendCard(item);

                if (!DroppedCard)
                    zoneBackG.gameObject.SetActive(true);
            }
        }


        private void SetCardPos(Card card)
        {
            if (IsDeck || card.CardID != cardType) return;

            deckLoader.RemoveCard(card);

            card._cachedDropZone = this;

            card.transform.SetParent(transform);
            card.ResetItem();
        }

        // private void SetSpellPos(Card card)
        // {
        //     if (IsDeck || card.CardID == CardType.Card) return;
        //
        //     deckLoader.RemoveCard(card);
        //
        //     card._cachedDropZone = this;
        //     card.transform.SetParent(transform);
        // }

        private void SetDeckParent(Card card)
        {
            if (!IsDeck) return;
            card.ResetItem();
        }

        private void ArrangeDeck(Card item)
        {
            if (!IsDeck) return;
            deckLoader.Rearrange(item, this);
        }
    }

    public enum DropZones
    {
        Attack,
        Defence,
        Spell,
        Deck
    }
}