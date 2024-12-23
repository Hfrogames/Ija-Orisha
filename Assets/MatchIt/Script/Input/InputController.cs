using UnityEngine;

namespace MatchIt.Script.Input
{
    public class InputController
    {
        private DragItem _dragItem;

        public void SetFollowItem(GameObject followItem, Transform rootCanvas)
        {
            _dragItem = followItem.GetComponent<DragItem>();
            _dragItem.Init();
            _dragItem.transform.SetParent(rootCanvas);
        }

        public void FollowPosition(Vector3 touchPosition)
        {
            if (!_dragItem) return;
            _dragItem.transform.position = touchPosition;
        }

        public void UnSetFollow()
        {
            if (!_dragItem) return;

            _dragItem.ResetItem();
            _dragItem.transform.localScale = Vector3.one;
            _dragItem = null;
        }

        private GameObject _activeDzGob;
        private DropZone _activeDropZone;

        public void SetActiveDropZone(GameObject dropZone)
        {
            _activeDzGob = dropZone;
            _activeDropZone = dropZone.GetComponent<DropZone>();

            if (_dragItem)
                _activeDropZone.OnHover();
        }

        public void UnSetActiveDropZone()
        {
            if (!_activeDzGob) return;
            _activeDropZone.OnHoverOut();
            _activeDzGob = null;
            _activeDropZone = null;
        }

        public void SendToDropZone()
        {
            if (!_dragItem || !_activeDzGob) return;
            if (_activeDropZone.CanDrop(_dragItem))
            {
                // _followItemParentDropZone.OnRemove();
                _activeDropZone.OnDrop(_dragItem.gameObject);
                _dragItem = null;
                _activeDzGob = null;
            }
            else
            {
                UnSetFollow();
            }

            _activeDropZone.OnHoverOut();
        }
    }
}