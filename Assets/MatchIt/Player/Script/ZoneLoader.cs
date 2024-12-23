using TMPro;
using UnityEngine;

namespace MatchIt.Player.Script.SO
{
    public class ZoneLoader : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI valueText;

        public void SetPoint(CardSO cardSo, DropZones dropZone)
        {
            switch (dropZone)
            {
                case DropZones.Attack:
                    valueText.text = cardSo.AttackValue.ToString();
                    break;
                case DropZones.Defence:
                    valueText.text = cardSo.DefenceValue.ToString();
                    break;
            }
        }
    }
}