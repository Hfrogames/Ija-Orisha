using UnityEngine;

[CreateAssetMenu(fileName = "CardID", menuName = "MatchIt/Card")]
public class CardSO : ScriptableObject
{
    [field: SerializeField] public CardType CardID { get; private set; }
    [field: SerializeField] public Orisha Orisha { get; private set; }
    [field: SerializeField,Range(0,10)] public int AttackValue { get; private set; }
    [field: SerializeField,Range(0,10)] public int DefenceValue { get; private set; }
    [field:SerializeField] public Sprite CardSprite { get; private set; }

    public CardData GetCardData()
    {
        return new CardData(this);
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