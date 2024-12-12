using TMPro;
using UnityEngine;

namespace MatchIt.UI.Script
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI winnerText;
        [SerializeField] private GameObject winnerWrap;

        public void DisplayWinner(string player)
        {
            winnerText.text = $"{player} wins!";
            winnerWrap.SetActive(true);
        }

        public void HideWinner()
        {
            winnerWrap.SetActive(false);
        }
    }
}