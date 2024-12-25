using System;
using MatchIt.Player.Script;
using UnityEngine;

public class DragItem : MonoBehaviour
{
    public bool isLocked;
    public CardLoader cardLoader;
    [field: SerializeField] public CardType CardID { get; private set; }
    [field: SerializeField] public RectTransform RectTransform { get; private set; }

    private DropZone _cachedDropZone;
    private Vector3 _cachedPosition;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _cachedDropZone = transform.GetComponentInParent<DropZone>();
        _cachedPosition = transform.position;
    }

    public void Select()
    {
        Init();
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        cardLoader.DisplayScore(true);

        if (_cachedDropZone)
            _cachedDropZone.OnRemove(this);
    }

    public void ResetItem()
    {
        if (_cachedDropZone.dropZones != DropZones.Deck)
        {
            transform.position = _cachedPosition;
            cardLoader.DisplayScore(false);
        }


        transform.SetParent(_cachedDropZone.transform);
    }
}

public enum CardType
{
    Card,
    Spell
}