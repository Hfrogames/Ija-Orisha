using TMPro;
using UnityEngine;

namespace MatchIt.Player.Script
{
    public class PlayerScoreManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI attackValue;
        [SerializeField] private TextMeshProUGUI defendValue;

        private int _attackPoint;
        private int _defendPoint;

        public void SetZonePoint(CardSO cardSo, DropZones dropZones)
        {
            switch (dropZones)
            {
                case DropZones.Attack:
                    CalcAttackPoint(cardSo);
                    break;
                case DropZones.Defence:
                    CalcDefencePoint(cardSo);
                    break;
            }
        }

        private void CalcAttackPoint(CardSO cardSo)
        {
            _attackPoint = cardSo.AttackValue;
            attackValue.text = _attackPoint.ToString();
        }

        private void CalcDefencePoint(CardSO cardSo)
        {
            _defendPoint = cardSo.DefenceValue;
            attackValue.text = _defendPoint.ToString();
        }
    }
}