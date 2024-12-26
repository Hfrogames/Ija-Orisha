using UnityEngine;

namespace MatchIt.Player.Script
{
    public class PackPlayerData : MonoBehaviour
    {
        [SerializeField] private DropZone attackZone;
        [SerializeField] private DropZone defenseZone;

        private CardSO _attackCard;
        private CardSO _attackSpell;
        private CardSO _defenseCard;
        private CardSO _defenseSpell;

        public PlayData Pack()
        {
            SetCards();

            return new PlayData()
            {
                AttackCard = _attackCard?.name ?? "None",
                DefenseCard = _defenseCard?.name ?? "None",
                AttackSpell = _attackSpell?.name ?? "None",
                DefenseSpell = _defenseSpell?.name ?? "None",
                AttackPoint = _attackCard?.AttackValue ?? 0,
                DefensePoint = _defenseCard?.DefenceValue ?? 0,
                PlayerHealth = 100
            };
        }

        private void SetCards()
        {
            _attackCard = attackZone.DroppedCard?.cardLoader.cardSO;
            _attackSpell = attackZone.DroppedSpell?.cardLoader.cardSO;
            _defenseCard = defenseZone.DroppedCard?.cardLoader.cardSO;
            _defenseSpell = defenseZone.DroppedSpell?.cardLoader.cardSO;
        }


        public void Display()
        {
            Debug.Log(JsonUtility.ToJson(Pack()));
        }
    }
}

public class PlayData
{
    public string AttackCard;
    public string DefenseCard;
    public string AttackSpell;
    public string DefenseSpell;
    public int AttackPoint;
    public int DefensePoint;
    public int PlayerHealth;
}