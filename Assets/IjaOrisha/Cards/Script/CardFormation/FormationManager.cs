using System;
using UnityEngine;

namespace IjaOrisha
{
    public class FormationManager : MonoBehaviour
    {
        [SerializeField] private DeckLoader deckLoader;

        [SerializeField] private GameObject playerOneFormation;
        [SerializeField] private GameObject playerTwoFormation;

        [SerializeField] private DropZone attackZone;
        [SerializeField] private DropZone attackZoneSpell;
        [SerializeField] private DropZone defenseZone;
        [SerializeField] private DropZone defenseZoneSpell;


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
            _attackCard = attackZone.DroppedCard?.CardSo;
            _attackSpell = attackZoneSpell.DroppedCard?.CardSo;
            _defenseCard = defenseZone.DroppedCard?.CardSo;
            _defenseSpell = defenseZoneSpell.DroppedCard?.CardSo;
        }

        public void FormationStart()
        {
            playerOneFormation.SetActive(true);
            playerTwoFormation.SetActive(true);
            
        }

        public void FormationEnd()
        {
            playerOneFormation.SetActive(false);
            playerTwoFormation.SetActive(false);
            attackZone.Clear();
            attackZoneSpell.Clear();
            defenseZone.Clear();
            defenseZoneSpell.Clear();
        }

        public void LoadDeck()
        {
            deckLoader.Reveal();
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