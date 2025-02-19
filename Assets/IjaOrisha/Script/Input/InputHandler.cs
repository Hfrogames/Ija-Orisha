using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


namespace IjaOrisha
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private GraphicRaycaster gamePlayGraphicsRaycaster;
        [SerializeField] private EventSystem eventSystem;
        [SerializeField] private Tags cardZone;
        [SerializeField] private Tags cards;

        private Vector2 _touchPosition;
        private Vector2 _cachedTouchPosition;
        private bool _isTouched;
        private bool _isDragging;
        private bool _isOnDropZone;

        private CardInputController _cardInputController = new CardInputController();

        private void OnHover(InputValue touchPos)
        {
            _touchPosition = touchPos.Get<Vector2>();

            if (_cachedTouchPosition != _touchPosition)
            {
                _cachedTouchPosition = _touchPosition;
                OnPositionReset();
            }

            FindHoverObject();

            _cardInputController.FollowPosition(_touchPosition);
        }

        private void OnTouch()
        {
            _isTouched = true;
            FindHoverObject();
        }

        private void OnUntouch()
        {
            _isTouched = _isDragging = false;
            if (!_isOnDropZone)
                _cardInputController.UnSetFollow();

            if (_isOnDropZone)
                _cardInputController.SendToDropZone();
        }

        private void OnPositionReset()
        {
            _isOnDropZone = false;
            _cardInputController.UnSetActiveDropZone();
        }

        private void FindHoverObject()
        {
            if (!_isTouched) return;
            PointerEventData pointerData = new PointerEventData(eventSystem)
            {
                position = _touchPosition
            };

            List<RaycastResult> results = new List<RaycastResult>();

            gamePlayGraphicsRaycaster.Raycast(pointerData, results);

            foreach (RaycastResult result in results)
            {
                OnCardZone(result.gameObject);

                OnCard(result.gameObject);
            }
        }


        private void OnCardZone(GameObject hit)
        {
            if (!hit.CompareTag(cardZone.ToString())) return;

            var cachedDropZone = hit.GetComponent<DropZone>();

            if (cachedDropZone.IsLocked) return;

            _isOnDropZone = true;
            _cardInputController.SetActiveDropZone(hit);
        }

        private void OnCard(GameObject hit)
        {
            if (!hit.CompareTag(cards.ToString()) || _isDragging) return;

            var cachedDragItem = hit.GetComponent<CardLoader>();

            if (cachedDragItem.isLocked) return;

            _isDragging = true;
            _cardInputController.SetFollowItem(hit, gamePlayGraphicsRaycaster.transform);
        }
    }
}