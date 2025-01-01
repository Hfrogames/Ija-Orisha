using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


namespace MatchIt.Script.Input
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

        private InputController _inputController = new InputController();

        private void OnHover(InputValue touchPos)
        {
            _touchPosition = touchPos.Get<Vector2>();

            if (_cachedTouchPosition != _touchPosition)
            {
                _cachedTouchPosition = _touchPosition;
                OnPositionReset();
            }

            FindHoverObject();

            _inputController.FollowPosition(_touchPosition);
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
                _inputController.UnSetFollow();

            if (_isOnDropZone)
                _inputController.SendToDropZone();
        }

        private void OnPositionReset()
        {
            _isOnDropZone = false;
            _inputController.UnSetActiveDropZone();
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
            _inputController.SetActiveDropZone(hit);
        }

        private void OnCard(GameObject hit)
        {
            if (!hit.CompareTag(cards.ToString()) || _isDragging) return;

            //
            // if (!_isDragging)
            // {            }

            var cachedDragItem = hit.GetComponent<Card>();

            if (cachedDragItem.isLocked) return;

            _isDragging = true;
            _inputController.SetFollowItem(hit, gamePlayGraphicsRaycaster.transform);
        }
    }
}