using MatchIt.Player.Script;
using MatchIt.Player.Script.CardControl;
using MatchIt.Player.Script.SO;
using MatchIt.Script.Event;
using UnityEngine;
using UnityEngine.UI;
using VInspector;


public class DropZone : MonoBehaviour
{
    public bool IsLocked { get; private set; } = false;
    public Card DroppedCard { get; private set; }
    public Card DroppedSpell { get; private set; }

    [SerializeField] public DropZones dropZones;


    private bool IsAttack => dropZones == DropZones.Attack;
    private bool IsDefence => dropZones == DropZones.Defence;
    private bool IsDeck => dropZones == DropZones.Deck;

    [SerializeField] private DeckLoader deckLoader;

    [HideIf(nameof(IsDeck)), SerializeField]
    private Image dropBorder;

    [HideIf(nameof(IsDeck)), SerializeField]
    public RectTransform emptyBorder;


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
        if (!IsDeck)
            IsLocked = true;

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

                if (DroppedSpell)
                {
                    DroppedSpell.gameObject.SetActive(false);
                    DroppedSpell = null;
                }

                transform.parent.localScale = Vector3.one;

                IsLocked = false;
                break;
            case PlayEvent.OnFormationEnd:
                if (DroppedCard)
                    DroppedCard.isLocked = true;
                if (DroppedSpell)
                    DroppedSpell.isLocked = true;
                IsLocked = true;
                break;
        }
    }

    public void OnHover(GameObject item = null)
    {
        ArrangeDeck(item);

        if (!IsDeck)
            dropBorder.color = new Color32(29, 255, 0, 100);
    }

    public void OnHoverOut()
    {
        if (IsDeck) return;
        dropBorder.color = new Color32(29, 255, 0, 0);
    }

    public bool CanDrop(Card item)
    {
        if (IsDeck) return true;
        if (item.CardID == CardType.Card && DroppedCard) return false;
        if (item.CardID == CardType.Spell && DroppedSpell) return false;

        if (item.CardID == CardType.Card) DroppedCard = item;
        if (item.CardID == CardType.Spell) DroppedSpell = item;

        if (DroppedCard || DroppedSpell)
            emptyBorder.gameObject.SetActive(false);


        return true;
    }

    public void OnDrop(Card card)
    {
        card.transform.localScale = Vector3.one;
        SetCardPos(card);
        SetSpellPos(card);
        SetDeckParent(card);
    }

    public void OnRemove(Card item)
    {
        if (item.CardID == CardType.Card) DroppedCard = null;
        if (item.CardID == CardType.Spell) DroppedSpell = null;

        if (!IsDeck)
        {
            deckLoader.AppendCard(item);

            if (!DroppedCard && !DroppedSpell)
                emptyBorder.gameObject.SetActive(true);
        }
    }


    private void SetCardPos(Card card)
    {
        if (IsDeck || card.CardID == CardType.Spell) return;

        deckLoader.RemoveCard(card);

        card._cachedDropZone = this;

        card.transform.SetParent(transform);

        // card.ResetItem();
        // RectTransform itemRect = item.GetComponent<RectTransform>();
        // item.transform.SetParent(transform);
        // itemRect.anchorMin = new Vector2(0.5f, 0.5f);
        // itemRect.anchorMax = new Vector2(0.5f, 0.5f);
        // itemRect.anchoredPosition = Vector3.zero;
        // if (item.name.Contains("card"))
        //     item.transform.localScale = new Vector3(1.2f, 1.2f, 1f);

        // DroppedCard.cardLoader.DisplayScore(false);
    }

    private void SetSpellPos(Card card)
    {
        if (IsDeck || card.CardID == CardType.Card) return;

        deckLoader.RemoveCard(card);

        card._cachedDropZone = this;
        card.transform.SetParent(transform);

        // card.ResetItem();

        // RectTransform itemRect = item.GetComponent<RectTransform>();
        // itemRect.localScale = new Vector3(.8f, .8f, .8f);
        // if (IsAttack)
        // {
        //     itemRect.anchorMin = new Vector2(0f, .5f);
        //     itemRect.anchorMax = new Vector2(0f, .5f);
        //     itemRect.pivot = new Vector2(0f, .5f);
        //     itemRect.anchoredPosition = new Vector2(-50f, 0f);
        // }
        //
        // if (IsDefence)
        // {
        //     itemRect.anchorMin = new Vector2(1f, .5f);
        //     itemRect.anchorMax = new Vector2(1f, .5f);
        //     itemRect.pivot = new Vector2(1f, .5f);
        //     itemRect.anchoredPosition = new Vector2(50f, 0f);
        // }
    }

    private void SetDeckParent(Card card)
    {
        if (!IsDeck) return;
        card.ResetItem();
    }

    private void ArrangeDeck(GameObject item)
    {
        if (!IsDeck) return;
        Card card = item.GetComponent<Card>();
        deckLoader.Rearrange(card, this);
    }
}

public enum DropZones
{
    Attack,
    Defence,
    Deck
}