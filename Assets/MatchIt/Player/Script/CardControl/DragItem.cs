using System;
using UnityEngine;

public class DragItem : MonoBehaviour
{
    [field: SerializeField] public CardType CardID { get; private set; }
    [field: SerializeField] public RectTransform RectTransform { get; private set; }


    private DropZone _cachedDropZone;
    private Vector3 _cachedPosition;

    public void Init()
    {
        if (_cachedDropZone)
            _cachedDropZone.OnRemove(this);


        _cachedDropZone = transform.GetComponentInParent<DropZone>();
        _cachedPosition = transform.position;
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    public void ResetItem()
    {
        transform.position = _cachedPosition;
    }
}

public enum CardType
{
    Card,
    Spell
}