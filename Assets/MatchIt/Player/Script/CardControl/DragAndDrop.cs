using UnityEngine;

namespace MatchIt.Player.Script.CardControl
{
    public class DragAndDrop : MonoBehaviour
    {
        [SerializeField] private DropZone attackDropZone;
        [SerializeField] private DropZone defenseDropZone;

        private RectTransform _selectedCard;


        private void OnSelect()
        {
            Debug.Log("card selected");
        }

        private void DropZone()
        {
        }
    }
}