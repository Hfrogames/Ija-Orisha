using IjaOrisha;
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

    [field: SerializeField, Range(0, 20), ShowIf(nameof(isCard))]
    public int AttackValue { get; private set; }

    [field: SerializeField, Range(0, 20), ShowIf(nameof(isCard))]
    public int DefenceValue { get; private set; }

    [field: SerializeField, ShowIf(nameof(isCard))]
    public SoundLib AttackSFX { get; private set; }

    [field: SerializeField, ShowIf(nameof(isCard))]
    public SoundLib DefenceSFX { get; private set; }


    [field: SerializeField, ShowIf(nameof(isSpell))]
    public Spells spell { get; private set; }

    [field: SerializeField, ShowIf(nameof(isSpell))]
    public int spellValue { get; private set; }

    [field: SerializeField, ShowIf(nameof(isSpell))]
    public SoundLib SpellSFX { get; private set; }
}

public enum CardType
{
    Card,
    Spell
}