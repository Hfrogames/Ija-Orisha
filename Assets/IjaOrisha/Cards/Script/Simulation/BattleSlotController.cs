using DG.Tweening;
using UnityEngine;

namespace IjaOrisha
{
    public enum SlotID
    {
        AttackCard,
        AttackSpell,
        DefenseCard,
        DefenseSpell
    }

    public class BattleSlotController : MonoBehaviour
    {
        [SerializeField] private PlayerID playerID;

        [SerializeField] private BattleSlot attackCard;
        [SerializeField] private BattleSlot attackSpell;
        [SerializeField] private BattleSlot defenseCard;
        [SerializeField] private BattleSlot defenseSpell;

        private Sequence _displaySq;
        private BattleSimulationData _bSim;

        public void LoadSlot(BattleSimulationData bSim)
        {
            _bSim = bSim;
            gameObject.SetActive(true);

            attackCard.LoadCard(bSim.AttackCardSo, true);
            attackSpell.LoadCard(bSim.AttackSpellSo, true);
            defenseCard.LoadCard(bSim.DefenseCardSo, false);
            defenseSpell.LoadCard(bSim.DefenseSpellSo, true);
        }

        public Sequence ShowSlot(SlotID slotID)
        {
            _displaySq = DOTween.Sequence();
            _displaySq.AppendInterval(0.5f);

            switch (slotID)
            {
                case SlotID.AttackCard:
                    _displaySq.Join(attackCard.ShowCard());
                    break;
                case SlotID.AttackSpell:
                    _displaySq.Join(attackSpell.ShowCard());
                    break;
                case SlotID.DefenseCard:
                    _displaySq.Join(defenseCard.ShowCard());
                    break;
                case SlotID.DefenseSpell:
                    _displaySq.Join(defenseSpell.ShowCard());
                    break;
            }

            return _displaySq;
        }

        public Sequence HideSlot(SlotID slotID)
        {
            _displaySq = DOTween.Sequence();

            switch (slotID)
            {
                case SlotID.AttackCard:
                    _displaySq.Join(attackCard.HideCard())
                        .AppendInterval(0.5f)
                        .Join(attackSpell.HideCard());
                    break;
                case SlotID.DefenseCard:
                    _displaySq.Join(defenseCard.HideCard())
                        .AppendInterval(0.5f)
                        .Join(defenseSpell.HideCard());
                    break;
            }

            return _displaySq;
        }
   
    }
}