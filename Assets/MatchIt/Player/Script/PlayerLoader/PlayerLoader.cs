using DG.Tweening;
using MatchIt.Script.Event;
using MatchIt.Script.LocalDB;
using MatchIt.Script.TweenEffects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MatchIt.Player.Script
{
    public class PlayerLoader : MonoBehaviour
    {
        [SerializeField] protected Sprite hiddenSprite;

        [SerializeField] protected CardFlip attackCardFlip;
        [SerializeField] protected CardFlip attackSpellFlip;
        [SerializeField] protected Image attackCardImg;
        [SerializeField] protected Image attackSpellImg;
        [SerializeField] protected TextMeshProUGUI attackPoint;

        [SerializeField] protected CardFlip defenceCardFlip;
        [SerializeField] protected CardFlip defenceSpellFlip;
        [SerializeField] protected Image defenceCardImg;
        [SerializeField] protected Image defenceSpellImg;
        [SerializeField] protected TextMeshProUGUI defencePoint;

        [SerializeField] protected TextMeshProUGUI playerHealth;

        protected CardSO _attackCard;
        protected CardSO _attackSpell;
        protected CardSO _defenceCard;
        protected CardSO _defenceSpell;

        protected int _attackValue = 0; // fetched by server
        protected int _defenceValue = 0; // fetched by server
        protected int _playerHealth = 0; // fetched by server

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
            }
        }

        private void ResetCardData()
        {
            // First, ensure all cards are in their initial state
            attackCardFlip.gameObject.SetActive(false);
            attackSpellFlip.gameObject.SetActive(false);
            defenceCardFlip.gameObject.SetActive(false);
            defenceSpellFlip.gameObject.SetActive(false);

            attackCardImg.sprite = hiddenSprite;
            defenceCardImg.sprite = hiddenSprite;
            attackSpellImg.sprite = hiddenSprite;
            defenceSpellImg.sprite = hiddenSprite;
            attackPoint.text = 0.ToString();
            defencePoint.text = 0.ToString();
        }

        public void SetCards(PlayData playData)
        {
            _attackCard = LocalDBManager.Instance.CardSoDB.GetCard(playData.AttackCard);
            _attackSpell = LocalDBManager.Instance.CardSoDB.GetCard(playData.AttackSpell);
            _defenceCard = LocalDBManager.Instance.CardSoDB.GetCard(playData.DefenseCard);
            _defenceSpell = LocalDBManager.Instance.CardSoDB.GetCard(playData.DefenseSpell);

            _attackValue = playData.AttackPoint;
            _defenceValue = playData.DefensePoint;
            _playerHealth = playData.PlayerHealth;
        }

        public virtual void DisplayCardData()
        {

        }
        

        public virtual void ApplyCardSpell()
        {
            attackPoint.DOText(_attackValue.ToString(), 2f);
            // attackPoint.text = _attackValue.ToString();
        }

        public virtual void AttackDefence()
        {
            attackSpellImg.DOFlip();
        }

        public virtual void DefenceAttacked()
        {
            defencePoint.DOText(_defenceValue.ToString(), 2f);
            // defencePoint.text = _defenceValue.ToString();
        }

        public virtual void AttackHealthPoint()
        {
            playerHealth.DOText(_playerHealth.ToString(), 2f);
            // playerHealth.text = _playerHealth.ToString();
        }
    }
}