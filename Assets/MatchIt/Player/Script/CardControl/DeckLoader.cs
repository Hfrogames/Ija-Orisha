using UnityEngine;
using System.Collections;

namespace MatchIt.Player.Script.CardControl
{
    public class DeckLoader : MonoBehaviour
    {
        [SerializeField] private DragItem[] cards;
        [SerializeField] private float delayBetweenCards = 0.5f; // Delay in seconds

        public void Reveal()
        {
            StartCoroutine(RevealCardsSequentially());
        }

        private IEnumerator RevealCardsSequentially()
        {
            foreach (var dragItem in cards)
            {
                dragItem.Reveal();
                yield return new WaitForSeconds(delayBetweenCards);
            }
        }
    }
}