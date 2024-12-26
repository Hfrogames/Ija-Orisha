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

        [SerializeField] protected RectTransform attackZone;
        [SerializeField] protected CardFlip attackCardFlip;
        [SerializeField] protected CardFlip attackSpellFlip;
        [SerializeField] protected Image attackCardImg;
        [SerializeField] protected Image attackSpellImg;
        [SerializeField] protected TextMeshProUGUI attackPoint;

        [SerializeField] protected RectTransform defenceZone;
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

        protected Sequence CardDataSq;
        protected Sequence CardSpellSq;

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
            CardDataSq = DOTween.Sequence();
            CardDataSq.OnComplete(ApplyCardSpell);
        }


        public virtual void ApplyCardSpell()
        {
            CardSpellSq = DOTween.Sequence();

            // Defence Spell
            CardSpellSq
                .AppendInterval(.5f)
                .AppendCallback(() =>
                {
                    defenceSpellFlip.gameObject.SetActive(true);
                    defenceSpellImg.sprite = _defenceSpell?.CardSprite;
                })
                .Join(defenceSpellFlip.transform.DOScale(.8f, 0.2f).From(0).SetEase(Ease.OutSine))

                // Attack Spell
                .AppendCallback(() =>
                {
                    attackSpellFlip.gameObject.SetActive(true);
                    attackSpellImg.sprite = _attackSpell?.CardSprite;
                })
                .Join(attackSpellFlip.transform.DOScale(.8f, 0.2f).From(0).SetEase(Ease.OutSine))
                // DO card flip
                .Append(attackSpellFlip.Flip())
                .Append(defenceSpellFlip.Flip())

                // Optional: Add text animations for points
                .AppendCallback(() =>
                {
                    attackPoint.text = _attackValue.ToString();
                    defencePoint.text = _defenceValue.ToString();
                    attackPoint.transform.parent.localScale = Vector3.zero;
                    defencePoint.transform.parent.localScale = Vector3.zero;
                })
                .Join(attackPoint.transform.parent.DOScale(1, 0.2f).SetEase(Ease.OutSine))
                .Join(defencePoint.transform.parent.DOScale(1, 0.2f).SetEase(Ease.OutSine));
        }

        public virtual void AttackDefence(int defendingPoint)
        {
            bool isStronger = defendingPoint > _attackValue;
            Sequence attackDefenceSq = DOTween.Sequence();
            attackDefenceSq
                .Append(attackCardImg.transform.DOScale(1.3f, .5f))
                .AppendInterval(1f)
                .Append(isStronger
                    ? attackZone.DOScale(0f, .5f)
                    : attackCardImg.transform.DOScale(1.5f, .5f));
        }

        public virtual void DefenceAttacked(int attackingPoint)
        {
            bool isDefected = attackingPoint > _defenceValue;
            Sequence defenceAttackedSq = DOTween.Sequence();

            defenceAttackedSq
                .AppendInterval(.5f)
                .Append(isDefected
                    ? defenceZone.DOScale(0f, .5f)
                    : defenceCardImg.transform.DOScale(1.3f, .5f));
        }

        public virtual void AttackHealthPoint()
        {
            Sequence attackHealthSq = DOTween.Sequence();

            // attackHealthSq

            playerHealth.DOText(_playerHealth.ToString(), 2f);
            // playerHealth.text = _playerHealth.ToString();
        }
    }
}