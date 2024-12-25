using MatchIt.Script.Event;
using MatchIt.Script.LocalDB;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MatchIt.Player.Script
{
    public class PlayerLoader : MonoBehaviour
    {
        [SerializeField] private Sprite hiddenSprite;

        [SerializeField] private Image attackCardImg;
        [SerializeField] private Image defenceCardImg;
        [SerializeField] private Image attackSpellImg;
        [SerializeField] private Image defenceSpellImg;

        [SerializeField] private TextMeshProUGUI playerHealth;
        [SerializeField] private TextMeshProUGUI attackPoint;
        [SerializeField] private TextMeshProUGUI defencePoint;

        private CardSO _attackCard;
        private CardSO _attackSpell;
        private CardSO _defenceCard;
        private CardSO _defenceSpell;

        private int _attackValue = 0; // fetched by server
        private int _defenceValue = 0; // fetched by server
        private int _playerHealth = 0; // fetched by server

        private void OnEnable()
        {
            EventPub.OnPlayEvent += OnPlayEvent;
        }

        private void OnDisable()
        {
            EventPub.OnPlayEvent -= OnPlayEvent;
        }

        private void OnPlayEvent(PlayEvent playEvent)
        {
            switch (playEvent)
            {
                case PlayEvent.OnFormationStart:
                    ResetCardData();
                    break;
                // case PlayEvent.OnBattleData:
                //     SetCards(BattleManager.Instance.PlayerTwoData);
                //     break;
                // case PlayEvent.OnBattleStart:
                //     DisplayCardData();
                //     break;
            }
        }

        public void SetCards(PlayData playData)
        {
            _attackCard = LocalDBManager.Instance.CardSoDB.GetCard(playData.AttackCard);
            _defenceCard = LocalDBManager.Instance.CardSoDB.GetCard(playData.DefenseCard);
            _attackSpell = LocalDBManager.Instance.CardSoDB.GetCard(playData.AttackSpell);
            _defenceSpell = LocalDBManager.Instance.CardSoDB.GetCard(playData.DefenseSpell);

            _attackValue = playData.AttackPoint;
            _defenceValue = playData.DefensePoint;
            _playerHealth = playData.PlayerHealth;
        }

        public void DisplayCardData()
        {
            attackCardImg.sprite = _attackCard.CardSprite;
            attackSpellImg.sprite = _attackSpell.CardSprite;
            defenceCardImg.sprite = _defenceCard.CardSprite;
            defenceSpellImg.sprite = _defenceSpell.CardSprite;

            attackPoint.text = _attackValue.ToString();
            defencePoint.text = _defenceValue.ToString();
            playerHealth.text = _playerHealth.ToString();
        }

        private void ResetCardData()
        {
            attackCardImg.sprite = hiddenSprite;
            defenceCardImg.sprite = hiddenSprite;
            attackSpellImg.sprite = hiddenSprite;
            defenceSpellImg.sprite = hiddenSprite;
            attackPoint.text = 0.ToString();
            defencePoint.text = 0.ToString();
        }
    }
}