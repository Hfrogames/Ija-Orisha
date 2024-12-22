using UnityEngine;

namespace MatchIt.Player.Script.CardControl
{
    public class DropZone : MonoBehaviour
    {
        private enum DropZones
        {
            Attack,
            Defence
        }

        [SerializeField] private DropZones dropZones;

        public void OnHover()
        {
            Debug.Log($"Hovering over {dropZones} zone");
        }

        public void OnDrop()
        {
            Debug.Log($"Around {dropZones} zone");
        }
    }
}