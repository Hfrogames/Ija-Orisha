using DG.Tweening;
using TMPro;
using UnityEngine;

namespace IjaOrisha
{
    public class HealthHud : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI healthPl1;
        [SerializeField] private TextMeshProUGUI healthPl2;

        public Sequence UpdateHealth(PlayerID playerID)
        {
            Sequence healthSq = DOTween.Sequence();
            TextMeshProUGUI textToEdit;
            int health = 0;


            if (playerID == PlayerID.Player1)
            {
                textToEdit = healthPl1;
                health = BattlePlayer.PlayerOneBd.PlayerHealth;
            }
            else
            {
                textToEdit = healthPl2;
                health = BattlePlayer.PlayerTwoBd.PlayerHealth;
            }

            healthSq
                .Append(textToEdit.transform.DOScale(0, 0.05f))
                .AppendCallback(() => { textToEdit.text = health.ToString(); })
                .Append(textToEdit.transform.DOScale(1, 0.2f));
            return healthSq;
        }
    }
}