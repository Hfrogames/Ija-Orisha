using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MatchIt.Player.Script
{
    public class CardLoader : MonoBehaviour
    {
        [field: SerializeField] public CardSO cardSO { get; private set; }

        [SerializeField] private Image orishaIcon;
        [SerializeField] private TextMeshProUGUI attackValueText;
        [SerializeField] private TextMeshProUGUI defenceValueText;

        private void Start()
        {
            SetCardValue();
            SetSpellValue();
        }

        public void DisplayScore(bool score)
        {
            attackValueText.transform.parent.gameObject.SetActive(score);
            defenceValueText.transform.parent.gameObject.SetActive(score);
        }

        private void SetCardValue()
        {
            if (cardSO == null || cardSO.CardID == CardType.Spell) return;
            orishaIcon.sprite = cardSO.CardSprite;
            attackValueText.text = cardSO.AttackValue.ToString();
            defenceValueText.text = cardSO.DefenceValue.ToString();
        }

        private void SetSpellValue()
        {
            if (cardSO == null || cardSO.CardID == CardType.Card) return;

            orishaIcon.sprite = cardSO.CardSprite;
            attackValueText.text = cardSO.spellValue.ToString();
            defenceValueText.gameObject.SetActive(false);
        }
    }
}