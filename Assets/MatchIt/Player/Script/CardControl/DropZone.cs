using System;
using MatchIt.Player.Script;
using MatchIt.Player.Script.SO;
using MatchIt.Script.Event;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VInspector;


public class DropZone : MonoBehaviour
{
    public bool IsLocked { get; private set; } = false;
    [SerializeField] public DropZones dropZones;
    [SerializeField] private ZoneLoader zoneLoader;
    [SerializeField] private PackPlayerData playerData;

    private bool IsAttack => dropZones == DropZones.Attack;
    private bool IsDefence => dropZones == DropZones.Defence;
    private bool IsDeck => dropZones == DropZones.Deck;

    [HideIf(nameof(IsDeck)), SerializeField]
    private Image dropBorder;

    private DragItem _droppedCard;
    private DragItem _droppedSpell;

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
                if (_droppedCard)
                {
                    _droppedCard.gameObject.SetActive(false);
                    _droppedCard = null;
                }

                if (_droppedSpell)
                {
                    _droppedSpell.gameObject.SetActive(false);
                    _droppedSpell = null;
                }

                IsLocked = false;
                break;
            case PlayEvent.OnFormationEnd:
                if (_droppedCard)
                    _droppedCard.isLocked = true;
                if (_droppedSpell)
                    _droppedSpell.isLocked = true;
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
        if (item.CardID == CardType.Card && _droppedCard) return false;
        if (item.CardID == CardType.Spell && _droppedSpell) return false;

        if (item.CardID == CardType.Card) _droppedCard = item;
        if (item.CardID == CardType.Spell) _droppedSpell = item;

        return true;
    }

    public void OnDrop(GameObject item)
    {
        item.transform.localScale = Vector3.one;
        SetCardPos(item);
        SetSpellPos(item);
        SetDeckParent(item);

        SetPlayerData();
    }

    public void OnRemove(DragItem item)
    {
        if (item.CardID == CardType.Card) _droppedCard = null;
        if (item.CardID == CardType.Spell) _droppedSpell = null;
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

        _droppedCard.cardLoader.DisplayScore(false);
        zoneLoader.SetPoint(_droppedCard.cardLoader.cardSO, dropZones);
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

    private void SetPlayerData()
    {
        if (_droppedCard)
            playerData.SetCards(_droppedCard.cardLoader.cardSO, dropZones);
        if (_droppedSpell)
            playerData.SetCards(_droppedSpell.cardLoader.cardSO, dropZones);
    }
}

public enum DropZones
{
    Attack,
    Defence,
    Deck
}