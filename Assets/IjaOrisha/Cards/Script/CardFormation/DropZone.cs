using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VInspector;

namespace IjaOrisha.Cards.Script.CardFormation
{
    public class DropZone : MonoBehaviour
    {
        public bool IsLocked { get; private set; } = false;
        public CardLoader DroppedCard { get; private set; }

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
            IsLocked = false;
            if (!deckLoader) deckLoader = GetComponentInParent<DeckLoader>();
        }

        private void OnPlayEvent(PlayEvent playEvent)
        {
            switch (playEvent)
            {
                case PlayEvent.OnFormationEnd:
                    if (DroppedCard)
                        DroppedCard.isLocked = true;
                    IsLocked = true;
                    break;
            }
        }
        
        public void Clear()
        {
            if (DroppedCard)
                Destroy(DroppedCard.gameObject);
            DroppedCard = null;
            IsLocked = false;
            zoneBackG.gameObject.SetActive(true);
        }

        public void OnHover(CardLoader item = null)
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

        public bool CanDrop(CardLoader item)
        {
            if (IsDeck) return true;

            if (item.CardID != cardType || DroppedCard) return false;

            if (item.CardID == cardType) DroppedCard = item;

            if (DroppedCard)
                zoneBackG.gameObject.SetActive(false);

            return true;
        }

        public void OnDrop(CardLoader cardLoader)
        {
            cardLoader.transform.localScale = Vector3.one;
            SetCardPos(cardLoader);
            SetDeckParent(cardLoader);
        }

        public void OnRemove(CardLoader item)
        {
            if (item.CardID == cardType) DroppedCard = null;

            if (!IsDeck)
            {
                deckLoader.AppendCard(item);

                if (!DroppedCard)
                    zoneBackG.gameObject.SetActive(true);
            }
        }

        private void SetCardPos(CardLoader cardLoader)
        {
            if (IsDeck || cardLoader.CardID != cardType) return;

            deckLoader.RemoveCard(cardLoader);

            cardLoader.Release(this);

            cardLoader.transform.SetParent(transform);
            cardLoader.ResetItem();
        }

        private void SetDeckParent(CardLoader cardLoader)
        {
            if (!IsDeck) return;
            cardLoader.ResetItem();
        }

        private void ArrangeDeck(CardLoader item)
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