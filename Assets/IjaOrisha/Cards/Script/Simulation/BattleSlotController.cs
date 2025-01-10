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
        [SerializeField] private BattlePoint battlePoint;

        private Sequence _displaySq;
        private BattleSlotController _battleSlotPl2;
        private BattleSimulationData _bSim;

        public void LoadSlot(BattleSimulationData bSim, BattleSlotController battleSlotPl2)
        {
            _bSim = bSim;
            _battleSlotPl2 = battleSlotPl2;

            attackCard.LoadCard(bSim.AttackCardSo, true);
            attackSpell.LoadCard(bSim.AttackSpellSo, true);
            defenseCard.LoadCard(bSim.DefenseCardSo, false);
            defenseSpell.LoadCard(bSim.DefenseSpellSo, true);
            
            gameObject.SetActive(true);
        }

        public Sequence ShowPoint(SlotID slotID)
        {
            _displaySq = DOTween.Sequence();
            _displaySq.AppendInterval(0.5f);

            switch (slotID)
            {
                case SlotID.AttackCard:
                    _displaySq.Append(battlePoint.SetPoint(_bSim.AttackCardSo.AttackValue));
                    break;
                case SlotID.DefenseCard:
                    _displaySq.Append(battlePoint.SetPoint(_bSim.DefenseCardSo.DefenceValue));
                    break;
            }

            return _displaySq;
        }

        public Sequence HidePoint()
        {
            return DOTween.Sequence()
                .SetEase(Ease.OutBack)
                .Append(battlePoint.transform.DOScale(0, .2f))
                .Join(_battleSlotPl2.battlePoint.transform.DOScale(0, .2f))
                .AppendCallback(() =>
                {
                    battlePoint.gameObject.SetActive(false);
                    _battleSlotPl2.battlePoint.gameObject.SetActive(false);
                });
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

        public Sequence MergeSpell(SlotID slotID)
        {
            _displaySq = DOTween.Sequence();
            float moveAmount = 280;

            switch (slotID)
            {
                case SlotID.DefenseCard:
                    if (playerID == PlayerID.Player2) moveAmount = -moveAmount;
                    _displaySq.Append(defenseCard.cardLoader.transform.DOLocalMoveX(moveAmount, .2f)
                        .SetEase(Ease.OutBack));
                    if (!_bSim.DefenseSpellSo)
                        _displaySq.Append(defenseSpell.flip.KnockOut(moveAmount * 2));
                    else
                        _displaySq
                            .Join(defenseSpell.cardLoader.transform.DOScale(1.2f, 0.1f))
                            .Join(defenseSpell.cardLoader.transform.DOLocalMoveX(-moveAmount, .2f)
                                .SetEase(Ease.OutBack));
                    break;
                case SlotID.AttackCard:
                    if (playerID == PlayerID.Player1) moveAmount = -moveAmount;
                    _displaySq.Append(attackCard.cardLoader.transform.DOLocalMoveX(moveAmount, .2f)
                        .SetEase(Ease.OutBack));
                    if (!_bSim.AttackSpellSo)
                        _displaySq.Append(attackSpell.flip.KnockOut(moveAmount * 2));
                    else
                        _displaySq
                            .Join(attackSpell.cardLoader.transform.DOScale(1.2f, 0.1f))
                            .Join(attackSpell.cardLoader.transform.DOLocalMoveX(-moveAmount, .2f)
                                .SetEase(Ease.OutBack));
                    break;
            }

            return _displaySq;
        }

        public Sequence ApplySpellPoint(SlotID slotID)
        {
            int currentPointPl1 = battlePoint.currentPoint;
            int currentPointPl2 = _battleSlotPl2.battlePoint.currentPoint;
            CardSO spellSo = null;

            if (slotID == SlotID.AttackSpell && _bSim.AttackSpellSo)
            {
                spellSo = _bSim.AttackSpellSo;
            }
            else if (slotID == SlotID.DefenseSpell)
            {
                spellSo = _bSim.DefenseSpellSo;
            }

            if (!spellSo) return null;

            _displaySq = DOTween.Sequence()
                .Append(ApplyDoubleByTwo(spellSo, currentPointPl1))
                .Append(ApplyDivideByTwo(spellSo, currentPointPl2));
            return _displaySq;
        }

        private Sequence ApplyDoubleByTwo(CardSO spellSo, int value)
        {
            if (spellSo.spell != Spells.doubleByTwo) return null;

            value *= 2;
            return battlePoint.UpdatePoint(value);
        }

        private Sequence ApplyDivideByTwo(CardSO spellSo, int value)
        {
            if (spellSo.spell != Spells.divideByTwo) return null;

            value /= 2;
            return _battleSlotPl2.battlePoint.UpdatePoint(value);
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