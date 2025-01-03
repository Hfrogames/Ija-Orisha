using IjaOrisha.Cards.Script.CardControl;
using UnityEngine;

namespace IjaOrisha.Script.Input
{
    public class InputController
    {
        private Card _card;

        public void SetFollowItem(GameObject followItem, Transform rootCanvas)
        {
            _card = followItem.GetComponent<Card>();
            _card.Select();
            _card.transform.SetParent(rootCanvas);
        }

        public void FollowPosition(Vector3 touchPosition)
        {
            if (!_card) return;
            _card.transform.position =
                Vector3.Lerp(_card.transform.position, touchPosition, 30 * Time.deltaTime);
        }

        public void UnSetFollow()
        {
            if (!_card) return;

            _card.ResetItem();

            _card = null;
        }

        private GameObject _activeDzGob;
        private DropZone _activeDropZone;

        public void SetActiveDropZone(GameObject dropZone)
        {
            _activeDzGob = dropZone;
            _activeDropZone = dropZone.GetComponent<DropZone>();

            if (_card)
                _activeDropZone.OnHover(_card.gameObject);
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
            if (!_card || !_activeDzGob) return;
            
            if (_card._cachedDropZone == _activeDropZone)
            {
                _card.ResetItem();
                _card = null;
                return;
            }

            if (_activeDropZone.CanDrop(_card))
            {
                // _followItemParentDropZone.OnRemove();
                _activeDropZone.OnDrop(_card);
                _card = null;
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