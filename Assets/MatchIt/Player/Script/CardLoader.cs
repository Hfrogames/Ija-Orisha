using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MatchIt.Player.Script
{
    public class CardLoader : MonoBehaviour
    {
        [SerializeField] private CardSO cardSO;

        [SerializeField] private Image orishaIcon;
        [SerializeField] private TextMeshPro attackValueText;
        [SerializeField] private TextMeshPro defenceValueText;

        private void Start()
        {
            orishaIcon.sprite = cardSO.CardSprite;
            attackValueText.text = cardSO.AttackValue.ToString();
            defenceValueText.text = cardSO.DefenceValue.ToString();
        }
    }
}