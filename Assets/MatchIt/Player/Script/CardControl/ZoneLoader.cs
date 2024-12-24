using TMPro;
using UnityEngine;

namespace MatchIt.Player.Script.SO
{
    public class ZoneLoader : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI valueText;

        private CardSO _card;
        private CardSO _spell;

        public void SetPoint(CardSO cardSo, DropZones dropZone)
        {
            SetCard(cardSo);

            SetAttackPoint(dropZone);
            SetDefencePoint(dropZone);
        }

        private void SetCard(CardSO cardSo)
        {
            if (cardSo.CardID == CardType.Card)
                _card = cardSo;
            if (cardSo.CardID == CardType.Spell)
                _spell = cardSo;
        }

        private int CalcPoint(int cardPoint)
        {
            int localCardPoint = cardPoint;

            if (_spell)
                switch (_spell.spell)
                {
                    case Spells.DoublePoint:
                        break;
                    case Spells.DividePoint:
                        break;
                }

            return localCardPoint;
        }

        private void SetAttackPoint(DropZones dropZone)
        {
            if (dropZone != DropZones.Attack) return;
            valueText.text = _card.AttackValue.ToString();
        }

        private void SetDefencePoint(DropZones dropZone)
        {
            if (dropZone != DropZones.Defence) return;
            valueText.text = _card.DefenceValue.ToString();
        }
    }
}