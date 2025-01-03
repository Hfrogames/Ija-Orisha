using System;
using IjaOrisha.Cards.Script.CardControl;
using UnityEngine;

namespace IjaOrisha.Player.Script
{
    public class PackPlayerData : MonoBehaviour
    {
        [SerializeField] private DropZone attackZone;
        [SerializeField] private DropZone defenseZone;

        private CardSO _attackCard;
        private CardSO _attackSpell;
        private CardSO _defenseCard;
        private CardSO _defenseSpell;

        public BattleData Pack()
        {
            SetCards();

            return new BattleData()
            {
                AttackCard = _attackCard?.name ?? "None",
                DefenseCard = _defenseCard?.name ?? "None",
                AttackSpell = _attackSpell?.spell.ToString() ?? "None",
                DefenseSpell = _defenseSpell?.spell.ToString() ?? "None",
                AttackPoint = _attackCard?.AttackValue ?? 0,
                DefensePoint = _defenseCard?.DefenceValue ?? 0,
                PlayerHealth = 100
            };
        }

        private void SetCards()
        {
            _attackCard = attackZone.DroppedCard?.cardLoader.cardSO;
            // _attackSpell = attackZone.DroppedSpell?.cardLoader.cardSO;
            _defenseCard = defenseZone.DroppedCard?.cardLoader.cardSO;
            // _defenseSpell = defenseZone.DroppedSpell?.cardLoader.cardSO;
        }


        public void Display()
        {
            Debug.Log(JsonUtility.ToJson(Pack()));
        }
    }
}

[Serializable]
public class BattleData
{
    public string AttackCard;
    public string DefenseCard;
    public string AttackSpell;
    public string DefenseSpell;
    public int AttackPoint;
    public int DefensePoint;
    public int PlayerHealth;
}