using System;
using UnityEngine;

public class DragItem : MonoBehaviour
{
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
        if (_cachedDropZone)
            _cachedDropZone.OnRemove(this);
        Init();
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    public void ResetItem()
    {
        if (_cachedDropZone.dropZones != DropZone.DropZones.Deck)
            transform.position = _cachedPosition;

        transform.SetParent(_cachedDropZone.transform);
    }
    
}

public enum CardType
{
    Card,
    Spell
}