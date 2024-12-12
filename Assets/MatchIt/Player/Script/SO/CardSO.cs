using UnityEngine;

namespace MatchIt.Player.Script
{
    public enum CardID
    {
        Triangle,
        Circle,
        Hexagon,
        Pentagon
    }

    [CreateAssetMenu(fileName = "CardID", menuName = "MatchIt/Card")]
    public class CardSO : ScriptableObject
    {
        public CardID cardID;
        public Sprite sprite;
    }
}