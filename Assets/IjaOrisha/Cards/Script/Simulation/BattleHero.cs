using DG.Tweening;
using TMPro;
using UnityEngine;

namespace IjaOrisha
{
    public class BattleHero : MonoBehaviour
    {
        [SerializeField] private PlayerID playerID;

        [SerializeField] private RectTransform attackHeroWeapon;
        [SerializeField] private TextMeshProUGUI attackPoint;

        [SerializeField] private RectTransform defendHeroShield;
        [SerializeField] private TextMeshProUGUI defendPoint;

        private BattleSimulationData _bSim;
        private BattleHero _enemyHero;

        public void Load(BattleSimulationData bSim, BattleHero enemyHero)
        {
            _bSim = bSim;
            _enemyHero = enemyHero;
            ResetPose();
        }

        public Sequence Attack()
        {
            // add attack effect
            Sequence attackSq = DOTween.Sequence();
            attackSq
                .AppendCallback(() => defendHeroShield.gameObject.SetActive(false))
                .AppendCallback(() => attackHeroWeapon.gameObject.SetActive(true))
                .AppendCallback(() => UpdateAttackPoint(_bSim.AttackCardSo.AttackValue))
                .Append(_enemyHero.Defend());
            return attackSq;
        }

        private Sequence Defend()
        {
            // defend attack
            Sequence defenseSq = DOTween.Sequence();
            defenseSq
                .AppendCallback(() => attackHeroWeapon.gameObject.SetActive(false))
                .AppendCallback(() => defendHeroShield.gameObject.SetActive(true))
                .AppendCallback(() => UpdateDefencePoint(_bSim.DefenseCardSo.DefenceValue));
            return defenseSq;
        }

        public Sequence ApplyAttackSpell()
        {
            // add spell effect
            Sequence spellSq = DOTween.Sequence();
            spellSq
                .AppendCallback(() => UpdateAttackPoint(_bSim.BattleData.AttackPoint))
                .AppendInterval(1f);
            return spellSq;
        }

        public Sequence ApplyDefenceSpell()
        {
            Sequence spellSq = DOTween.Sequence();
            spellSq
                .AppendCallback(() => UpdateDefencePoint(_bSim.BattleData.DefensePoint))
                .AppendInterval(1f);
            return spellSq;
        }

        public Sequence Failed()
        {
            // attack failed
            Sequence failedSq = DOTween.Sequence();
            failedSq
                .AppendCallback(DestroyShield)
                .AppendCallback(DestroyWeapon)
                .AppendInterval(1f)
                .AppendCallback(ResetPose);
            return failedSq;
        }

        private void DestroyWeapon()
        {
            attackHeroWeapon.gameObject.SetActive(false);
        }

        private void DestroyShield()
        {
            defendHeroShield.gameObject.SetActive(false);
        }

        private void ResetPose()
        {
            attackHeroWeapon.gameObject.SetActive(false);
            defendHeroShield.gameObject.SetActive(false);
        }

        private void UpdateAttackPoint(int value)
        {
            // add point
            attackPoint.text = value.ToString();
        }

        private void UpdateDefencePoint(int value)
        {
            // add point
            defendPoint.text = value.ToString();
        }
    }
}