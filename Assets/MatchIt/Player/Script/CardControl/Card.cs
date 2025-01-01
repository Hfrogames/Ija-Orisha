using System;
using DG.Tweening;
using MatchIt.Player.Script;
using MatchIt.Script.TweenEffects;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool isLocked;
    public CardLoader cardLoader;
    public bool isRevealed;
    [field: SerializeField] public CardType CardID { get; private set; }

    [SerializeField] RectTransform cardRect;
    [SerializeField] private RectTransform attackScore;
    [SerializeField] private RectTransform defenceScore;

    public DropZone _cachedDropZone;
    private CardSO _cardSo;


    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _cachedDropZone = transform.GetComponentInParent<DropZone>();
    }

    public void Select()
    {
        Init();

        if (_cachedDropZone)
            _cachedDropZone.OnRemove(this);
    }

    public void ResetItem()
    {
        transform.SetParent(_cachedDropZone.transform);

        transform
            .DOLocalMove(Vector3.zero, .25f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                cardRect.anchorMin = new Vector2(0.5f, 0.5f);
                cardRect.anchorMax = new Vector2(0.5f, 0.5f);
                cardRect.anchoredPosition = Vector3.zero;
            });
    }

    public void SetCardSo(CardSO cardSo)
    {
        _cardSo = cardSo;
        CardID = cardSo.CardID;
        cardLoader.SetCard(cardSo);
    }

    public void Reveal()
    {
        if (isRevealed) return;
        gameObject.SetActive(true);
        attackScore.gameObject.SetActive(true);
        defenceScore.gameObject.SetActive(true);
        attackScore.DOShakeScale(0.5f, 0.2f, 10, 90);
        defenceScore.DOShakeScale(0.5f, 0.2f, 10, 90);
        isRevealed = true;
    }
}