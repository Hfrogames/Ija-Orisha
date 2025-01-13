using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace IjaOrisha
{
    public class HealthHud : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI healthPoint;

        private int? _health;

        public void LoadHealth(int health)
        {
            _health = health;
        }

        public Sequence UpdateHealth()
        {
            if (!_health.HasValue) return null;
            return DOTween.Sequence()
                .Append(healthPoint.transform.DOScale(0, 0.05f))
                .AppendCallback(() => { healthPoint.text = _health.ToString(); })
                .Append(healthPoint.transform.DOScale(1, 0.2f));
        }
    }
}