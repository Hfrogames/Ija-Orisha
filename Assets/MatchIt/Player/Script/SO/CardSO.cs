using UnityEngine;
using VInspector;

[CreateAssetMenu(fileName = "CardID", menuName = "MatchIt/Card")]
public class CardSO : ScriptableObject
{
    [field: SerializeField] public CardType CardID { get; private set; }

    [field: SerializeField] public Sprite CardSprite { get; private set; }


    private bool isCard => CardID == CardType.Card;
    private bool isSpell => CardID == CardType.Spell;


    [field: SerializeField, ShowIf(nameof(isCard))]
    public Orisha Orisha { get; private set; }

    [field: SerializeField, Range(0, 10), ShowIf(nameof(isCard))]
    public int AttackValue { get; private set; }

    [field: SerializeField, Range(0, 10), ShowIf(nameof(isCard))]
    public int DefenceValue { get; private set; }


    [field: SerializeField, ShowIf(nameof(isSpell))]
    public Spells spell { get; private set; }

    [field: SerializeField, ShowIf(nameof(isSpell))]
    public int spellValue { get; private set; }


    public CardData GetCardData()
    {
        return new CardData(this);
    }

    public SpellData GetSpellData()
    {
        return new SpellData(this);
    }
}

public class CardData
{
    public CardType CardID;
    public Orisha Orisha;
    public int AttackValue;
    public int DefenceValue;

    public CardData(CardSO cardSO)
    {
        CardID = cardSO.CardID;
        Orisha = cardSO.Orisha;
        AttackValue = cardSO.AttackValue;
        DefenceValue = cardSO.DefenceValue;
    }
}

public class SpellData
{
    public CardType CardID;
    public Spells Spell;
    public int Value;

    public SpellData(CardSO cardSO)
    {
        CardID = cardSO.CardID;
        Spell = cardSO.spell;
        Value = cardSO.spellValue;
    }
}