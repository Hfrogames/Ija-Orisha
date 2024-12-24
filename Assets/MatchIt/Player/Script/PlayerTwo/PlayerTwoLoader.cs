using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MatchIt.Player.Script
{
    public class PlayerTwoLoader : MonoBehaviour
    {
        [SerializeField] private Sprite hiddenSprite;

        [SerializeField] private Image attackCardImg;
        [SerializeField] private Image defenceCardImg;
        [SerializeField] private Image attackSpellImg;
        [SerializeField] private Image defenceSpellImg;

        [SerializeField] private TextMeshProUGUI playerHealth;
        [SerializeField] private TextMeshProUGUI attackPoint;
        [SerializeField] private TextMeshProUGUI defencePoint;

        public CardSO _attackCard;
        public CardSO _defenceCard;
        public CardSO _attackSpell;
        public CardSO _defenceSpell;

        private int _attackValue = 8; // fetched by server
        private int _defenceValue = 8; // fetched by server

        private void Start()
        {
            DisplayCardData();
        }

        public void SetCards(PlayData playData)
        {
            // _attackCard = attackCard;
            // _defenceCard = defenceCard;
            // _attackSpell = attackSpell;
            // _defenceSpell = defenceSpell;
        }

        public void DisplayCardData()
        {
            attackCardImg.sprite = _attackCard.CardSprite;
            attackSpellImg.sprite = _attackSpell.CardSprite;
            defenceCardImg.sprite = _defenceCard.CardSprite;
            defenceSpellImg.sprite = _defenceSpell.CardSprite;

            attackPoint.text = _attackValue.ToString();
            defencePoint.text = _defenceValue.ToString();
        }
    }
}