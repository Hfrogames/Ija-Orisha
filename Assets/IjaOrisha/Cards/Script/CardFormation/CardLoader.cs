using DG.Tweening;
using IjaOrisha.Cards.Script.CardFormation;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace IjaOrisha
{
    public class CardLoader : MonoBehaviour
    {
        public DropZone CachedDropZone { get; private set; }
        public CardType CardID { get; private set; }
        public CardSO CardSo { get; private set; }

        [SerializeField] private Image orishaImg;

        [SerializeField] private RectTransform cardRect;

        [SerializeField] private RectTransform attackRect;
        [SerializeField] private TextMeshProUGUI attackText;

        [SerializeField] private RectTransform defenceRect;
        [SerializeField] private TextMeshProUGUI defenceText;

        [SerializeField] private RectTransform spellRect;
        [SerializeField] private TextMeshProUGUI spellText;

        [HideInInspector] public bool isLocked;


        public void SetCardSo(CardSO localCardSo)
        {
            CardSo = localCardSo;

            SetCardValue();
        }

        private void SetCardValue()
        {
            orishaImg.sprite = CardSo.CardSprite;
            CardID = CardSo.CardID;

            if (CardSo.CardID == CardType.Card)
            {
                attackRect.gameObject.SetActive(true);
                attackText.text = CardSo.AttackValue.ToString();
                defenceRect.gameObject.SetActive(true);
                defenceText.text = CardSo.DefenceValue.ToString();
            }
            else
            {
                spellRect.gameObject.SetActive(true);
                spellText.text = CardSo.spellValue.ToString();
            }

            Init();
        }

        private void Init()
        {
            CachedDropZone = transform.GetComponentInParent<DropZone>();
        }

        public void Select()
        {
            Init();

            if (CachedDropZone)
                CachedDropZone.OnRemove(this);
        }

        public void Release(DropZone releasedOn)
        {
            CachedDropZone = releasedOn;
        }

        public void ResetItem()
        {
            transform.SetParent(CachedDropZone.transform);

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

        public void HidePoint(bool hideDefence)
        {
            if (hideDefence)
                defenceRect.gameObject.SetActive(false);
            else
                attackRect.gameObject.SetActive(false);
        }
    }
}