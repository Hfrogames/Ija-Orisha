using IjaOrisha.Cards.Script.CardFormation;
using UnityEngine;

namespace IjaOrisha.Script.Input
{
    public class CardInputController
    {
        private CardLoader _cardLoader;

        public void SetFollowItem(GameObject followItem, Transform rootCanvas)
        {
            _cardLoader = followItem.GetComponent<CardLoader>();
            _cardLoader.Select();
            _cardLoader.transform.SetParent(rootCanvas);
        }

        public void FollowPosition(Vector3 touchPosition)
        {
            if (!_cardLoader) return;
            _cardLoader.transform.position =
                Vector3.Lerp(_cardLoader.transform.position, touchPosition, 30 * Time.deltaTime);
        }

        public void UnSetFollow()
        {
            if (!_cardLoader) return;

            _cardLoader.ResetItem();

            _cardLoader = null;
        }

        private GameObject _activeDzGob;
        private DropZone _activeDropZone;

        public void SetActiveDropZone(GameObject dropZone)
        {
            _activeDzGob = dropZone;
            _activeDropZone = dropZone.GetComponent<DropZone>();

            if (_cardLoader)
                _activeDropZone.OnHover(_cardLoader);
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
            if (!_cardLoader || !_activeDzGob) return;
            
            if (_cardLoader.CachedDropZone == _activeDropZone)
            {
                _cardLoader.ResetItem();
                _cardLoader = null;
                return;
            }

            if (_activeDropZone.CanDrop(_cardLoader))
            {
                _activeDropZone.OnDrop(_cardLoader);
                _cardLoader = null;
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