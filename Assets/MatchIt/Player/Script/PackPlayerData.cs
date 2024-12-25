using UnityEngine;

namespace MatchIt.Player.Script
{
    public class PackPlayerData : MonoBehaviour
    {
        private CardSO _attackCard;
        private CardSO _attackSpell;
        private CardSO _defenseCard;
        private CardSO _defenseSpell;

        public void SetCards(CardSO card, DropZones dropZones)
        {
            if (dropZones == DropZones.Attack)
            {
                if (card.CardID == CardType.Card)
                    _attackCard = card;

                if (card.CardID == CardType.Spell)
                    _attackSpell = card;
            }

            if (dropZones == DropZones.Defence)
            {
                if (card.CardID == CardType.Card)
                    _defenseCard = card;
                if (card.CardID == CardType.Spell)
                    _defenseSpell = card;
            }
        }
        
        public PlayData Pack()
        {
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