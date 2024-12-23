using UnityEngine;
using UnityEngine.UI;
using VInspector;


public class DropZone : MonoBehaviour
{
    public enum DropZones
    {
        Attack,
        Defence,
        Deck
    }

    [SerializeField] public DropZones dropZones;

    private bool IsAttack => dropZones == DropZones.Attack;
    private bool IsDefence => dropZones == DropZones.Defence;
    private bool IsDeck => dropZones == DropZones.Deck;

    [HideIf(nameof(IsDeck)), SerializeField]
    private Image dropBorder;

    private DragItem _droppedCard;
    private DragItem _droppedSpell;


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
    }

    public void OnRemove(DragItem item)
    {
        if (item.CardID == CardType.Card) _droppedCard = null;
        if (item.CardID == CardType.Spell) _droppedSpell = null;
    }

    private void SetCardPos(GameObject item)
    {
        if (IsDeck) return;
        RectTransform itemRect = item.GetComponent<RectTransform>();
        item.transform.SetParent(transform);
        itemRect.anchorMin = new Vector2(0.5f, 0.5f);
        itemRect.anchorMax = new Vector2(0.5f, 0.5f);
        itemRect.anchoredPosition = Vector3.zero;
        if (item.name.Contains("card"))
            item.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
    }

    private void SetSpellPos(GameObject item)
    {
        if (IsDeck || !item.name.Contains("spell")) return;

        RectTransform itemRect = item.GetComponent<RectTransform>();
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