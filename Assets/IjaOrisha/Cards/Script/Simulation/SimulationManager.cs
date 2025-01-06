using DG.Tweening;
using IjaOrisha.Script.Battle;
using IjaOrisha.Script.LocalDB;
using UnityEngine;

namespace IjaOrisha
{
    public enum AttackResult
    {
        Defeat,
        Victory,
        Draw
    }

    public class SimulationManager : MonoBehaviour
    {
        [SerializeField] private BattleSlotController bSlotControllerPl2;
        [SerializeField] private BattleHero bHeroPl2;
        [SerializeField] private BattleSlotController bSlotControllerPl1;
        [SerializeField] private BattleHero bHeroPl1;

        private LocalDB _cardSoDB;
        private LocalDB _spellSoDB;

        private BattleSimulationData _bSimPl1;
        private BattleSimulationData _bSimPl2;

        private AttackResult _attackResultPl1;
        private AttackResult _attackResultPl2;

        public void LoadSimulationData()
        {
            _bSimPl1 = GetBattleData(BattlePlayer.PlayerOneBd);
            _bSimPl2 = GetBattleData(BattlePlayer.PlayerTwoBd);

            bSlotControllerPl1.LoadSlot(_bSimPl1);
            bSlotControllerPl2.LoadSlot(_bSimPl2);

            bHeroPl1.Load(_bSimPl1, bHeroPl2);
            bHeroPl2.Load(_bSimPl2, bHeroPl1);

            EventPub.Emit(PlayEvent.OnBattleStart);
        }

        public void SimulationStart()
        {
            Sequence seq = DOTween.Sequence();
            Simulate(
                PlayerID.Player1,
                bSlotControllerPl1,
                bSlotControllerPl2,
                bHeroPl1,
                bHeroPl2
            ).OnComplete(() =>
                Simulate(
                    PlayerID.Player2,
                    bSlotControllerPl2,
                    bSlotControllerPl1,
                    bHeroPl2,
                    bHeroPl1
                ));
        }

        private Sequence Simulate(
            PlayerID playerID,
            BattleSlotController bSlotPl1,
            BattleSlotController bSlotPl2,
            BattleHero locBHeroPl1,
            BattleHero locBHeroPl2)
        {
            Sequence seq = DOTween.Sequence();
            seq
                // display left card
                .Append(bSlotPl2.ShowSlot(SlotID.DefenseCard))
                .Append(bSlotPl1.ShowSlot(SlotID.AttackCard))

                // display player attack point
                .Append(locBHeroPl1.Attack())
                .AppendInterval(2)

                // display left spell
                .Append(bSlotPl2.ShowSlot(SlotID.DefenseSpell))
                .Append(locBHeroPl2.ApplyDefenceSpell())
                .Append(bSlotPl1.ShowSlot(SlotID.AttackSpell))
                .Append(locBHeroPl1.ApplyAttackSpell())

                // hide looser
                .AppendCallback(() => HideLooser(seq, playerID));
            return seq;
        }

        public void SimulationEnd()
        {
            bSlotControllerPl2.gameObject.SetActive(false);
            bSlotControllerPl1.gameObject.SetActive(false);
        }

        private void HideLooser(Sequence seq, PlayerID playerID)
        {
            seq.AppendInterval(1f);

            int attackPoint = 0;
            int defensePoint = 0;
            BattleSlotController bSlotDefence;
            BattleSlotController bSlotAttack;
            BattleHero offenceHero;
            BattleHero defenceHero;

            if (playerID == PlayerID.Player1)
            {
                attackPoint = _bSimPl1.BattleData.AttackPoint;
                defensePoint = _bSimPl2.BattleData.DefensePoint;
                bSlotAttack = bSlotControllerPl1;
                bSlotDefence = bSlotControllerPl2;
                offenceHero = bHeroPl1;
                defenceHero = bHeroPl2;
            }
            else
            {
                attackPoint = _bSimPl2.BattleData.AttackPoint;
                defensePoint = _bSimPl1.BattleData.DefensePoint;
                bSlotAttack = bSlotControllerPl2;
                bSlotDefence = bSlotControllerPl1;
                offenceHero = bHeroPl2;
                defenceHero = bHeroPl1;
            }

            if (attackPoint > defensePoint)
            {
                seq.Append(bSlotDefence.HideDefenceSlot())
                    .Append(defenceHero.Failed());
            }
            else if (attackPoint < defensePoint)
            {
                seq.Append(bSlotAttack.HideAttackSlot())
                    .Append(offenceHero.Failed());
            }
            else
            {
                seq.Append(bSlotAttack.HideAttackSlot())
                    .Append(bSlotDefence.HideDefenceSlot())
                    .Append(offenceHero.Failed())
                    .Append(defenceHero.Failed());
            }
        }

        private BattleSimulationData GetBattleData(BattleData battleData)
        {
            if (!_cardSoDB || !_spellSoDB)
            {
                _cardSoDB = LocalDBManager.Instance.CardSoDB;
                _spellSoDB = LocalDBManager.Instance.SpellSoDB;
            }


            return new BattleSimulationData()
            {
                BattleData = battleData,
                AttackCardSo = _cardSoDB.GetCard(battleData.AttackCard),
                AttackSpellSo = _spellSoDB.GetCard(battleData.AttackSpell),
                DefenseCardSo = _cardSoDB.GetCard(battleData.DefenseCard),
                DefenseSpellSo = _spellSoDB.GetCard(battleData.DefenseSpell)
            };
        }
    }
}

public class BattleSimulationData
{
    public BattleData BattleData;
    public CardSO AttackCardSo;
    public CardSO AttackSpellSo;
    public CardSO DefenseCardSo;
    public CardSO DefenseSpellSo;
}