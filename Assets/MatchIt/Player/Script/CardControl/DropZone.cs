using MatchIt.Player.Script;
using MatchIt.Player.Script.SO;
using MatchIt.Script.Event;
using UnityEngine;
using UnityEngine.UI;
using VInspector;


public class DropZone : MonoBehaviour
{
    public bool IsLocked { get; private set; } = false;
    public DragItem DroppedCard { get; private set; }
    public DragItem DroppedSpell { get; private set; }

    [SerializeField] public DropZones dropZones;

    private bool IsAttack => dropZones == DropZones.Attack;
    private bool IsDefence => dropZones == DropZones.Defence;
    private bool IsDeck => dropZones == DropZones.Deck;

    [HideIf(nameof(IsDeck)), SerializeField]
    private Image dropBorder;


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

    public void OnHover()
    {
        if (IsDeck) return;
        dropBorder.color = new Color32(29, 255, 0, 100);
    }

    public void OnHoverOut()
    {
        if (IsDeck) return;
        dropBorder.color = new Color32(29, 255, 0, 0);
    }

    public bool CanDrop(DragItem item)
    {
        if (IsDeck) return true;
        if (item.CardID == CardType.Card && DroppedCard) return false;
        if (item.CardID == CardType.Spell && DroppedSpell) return false;

        if (item.CardID == CardType.Card) DroppedCard = item;
        if (item.CardID == CardType.Spell) DroppedSpell = item;

        return true;
    }

    public void OnDrop(GameObject item)
    {
        item.transform.localScale = Vector3.one;
        SetCardPos(item);
        SetSpellPos(item);
        SetDeckParent(item);
    }

    public void OnRemove(DragItem item)
    {
        if (item.CardID == CardType.Card) DroppedCard = null;
        if (item.CardID == CardType.Spell) DroppedSpell = null;
    }


    private void SetCardPos(GameObject item)
    {
        if (IsDeck || !item.name.Contains("card")) return;
        RectTransform itemRect = item.GetComponent<RectTransform>();
        item.transform.SetParent(transform);
        itemRect.anchorMin = new Vector2(0.5f, 0.5f);
        itemRect.anchorMax = new Vector2(0.5f, 0.5f);
        itemRect.anchoredPosition = Vector3.zero;
        if (item.name.Contains("card"))
            item.transform.localScale = new Vector3(1.2f, 1.2f, 1f);

        DroppedCard.cardLoader.DisplayScore(false);
    }

    private void SetSpellPos(GameObject item)
    {
        if (IsDeck || !item.name.Contains("spell")) return;

        RectTransform itemRect = item.GetComponent<RectTransform>();
        item.transform.SetParent(transform);
        if (IsAttack)
        {
            itemRect.anchorMin = new Vector2(0f, .5f);
            itemRect.anchorMax = new Vector2(0f, .5f);
            itemRect.pivot = new Vector2(0f, .5f);
            itemRect.anchoredPosition = new Vector2(-50f, 0f);
        }

        if (IsDefence)
        {
            itemRect.anchorMin = new Vector2(1f, .5f);
            itemRect.anchorMax = new Vector2(1f, .5f);
            itemRect.pivot = new Vector2(1f, .5f);
            itemRect.anchoredPosition = new Vector2(50f, 0f);
        }
    }

    private void SetDeckParent(GameObject item)
    {
        if (!IsDeck) return;
        item.transform.SetParent(transform);
    }
}

public enum DropZones
{
    Attack,
    Defence,
    Deck
}