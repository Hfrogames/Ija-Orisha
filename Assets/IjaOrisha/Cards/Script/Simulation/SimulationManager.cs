using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

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
        [SerializeField] private BattleInfo battleInfo;

        [SerializeField] private HealthHud healthPl1;
        [SerializeField] private HealthHud healthPl2;

        [SerializeField] private BattleSlotController bSlotControllerPl2;
        [SerializeField] private BattleSlotController bSlotControllerPl1;

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

            bSlotControllerPl1.LoadSlot(_bSimPl1, bSlotControllerPl2);
            bSlotControllerPl2.LoadSlot(_bSimPl2, bSlotControllerPl1);

            healthPl1.LoadHealth(BattlePlayer.PlayerOneBd.PlayerHealth);
            healthPl2.LoadHealth(BattlePlayer.PlayerTwoBd.PlayerHealth);

            EventPub.Emit(PlayEvent.OnSimulationStart);
        }

        public void SimulationStart()
        {
            Simulate(PlayerID.Player1, bSlotControllerPl1, bSlotControllerPl2, healthPl2, "Attack!")
                .Append(Simulate(PlayerID.Player2, bSlotControllerPl2, bSlotControllerPl1, healthPl1, "Defend"))
                .OnComplete(() => { EventPub.Emit(PlayEvent.OnSimulationEnd); });

            // Get the total duration
            // float totalDuration = seq.Duration();
            // Debug.Log("Total Sequence Duration: " + totalDuration + " seconds");
        }

        private Sequence Simulate(PlayerID playerID, BattleSlotController bSlotPl1, BattleSlotController bSlotPl2,
            HealthHud health, string infoText)
        {
            return DOTween.Sequence()
                    // announce round info
                    .Append(battleInfo.Announce(infoText))
                    // reveal defence
                    .Join(bSlotPl2.ShowSlot(SlotID.DefenseCard))
                    .Append(bSlotPl2.ShowSlot(SlotID.DefenseSpell))
                    .Append(bSlotPl2.ShowPoint(SlotID.DefenseCard))
                    // rest
                    .AppendInterval(.5f)

                    // reveal attack
                    .Append(bSlotPl1.ShowSlot(SlotID.AttackCard))
                    .Append(bSlotPl1.ShowPoint(SlotID.AttackCard))
                    .Append(bSlotPl1.ShowSlot(SlotID.AttackSpell))

                    // rest
                    .AppendInterval(1)

                    // apply spell
                    .Append(bSlotPl2.MergeSpell(SlotID.DefenseCard))
                    .Append(bSlotPl2.ApplySpellPoint(SlotID.DefenseSpell))

                    // rest
                    .AppendInterval(1)

                    // apply spell
                    .Append(bSlotPl1.MergeSpell(SlotID.AttackCard))
                    .Append(bSlotPl1.ApplySpellPoint(SlotID.AttackSpell))

                    // start attack
                    .Append(bSlotPl1.Attack())
                    .Join(bSlotPl2.Defend())
                    .AppendInterval(2)

                    // hide looser
                    .Append(HideLooser(playerID))
                    .Append(health.UpdateHealth())
                    .Append(bSlotPl1.HidePoint())
                ;
        }

        public void SimulationEnd()
        {
            bSlotControllerPl2.gameObject.SetActive(false);
            bSlotControllerPl1.gameObject.SetActive(false);
        }

        private Sequence HideLooser(PlayerID playerID)
        {
            Sequence seq = DOTween.Sequence();

            int attackPoint = 0;
            int defensePoint = 0;
            BattleSlotController bSlotDefence;
            BattleSlotController bSlotAttack;

            if (playerID == PlayerID.Player1)
            {
                attackPoint = _bSimPl1.BattleData.AttackPoint;
                defensePoint = _bSimPl2.BattleData.DefensePoint;
                bSlotAttack = bSlotControllerPl1;
                bSlotDefence = bSlotControllerPl2;
            }
            else
            {
                attackPoint = _bSimPl2.BattleData.AttackPoint;
                defensePoint = _bSimPl1.BattleData.DefensePoint;
                bSlotAttack = bSlotControllerPl2;
                bSlotDefence = bSlotControllerPl1;
            }

            if (attackPoint > defensePoint)
            {
                seq
                    .Append(battleInfo.Announce("Clean hit!"))
                    .Append(bSlotDefence.HideSlot(SlotID.DefenseCard))
                    ;
            }
            else if (attackPoint < defensePoint)
            {
                seq
                    .Append(battleInfo.Announce("Blocked!"))
                    .Append(bSlotAttack.HideSlot(SlotID.AttackCard))
                    .AppendCallback(bSlotDefence.DefendSound)
                    ;
            }
            else
            {
                seq
                    .Append(battleInfo.Announce("Draw"))
                    .Append(bSlotAttack.HideSlot(SlotID.AttackCard))
                    .Append(bSlotDefence.HideSlot(SlotID.DefenseCard));
            }

            return seq;
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