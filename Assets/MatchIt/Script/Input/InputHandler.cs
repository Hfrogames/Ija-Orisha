using System.Collections.Generic;
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
        private bool _isTouched;
        private bool _isDragging;

        private InputController _inputController = new InputController();

        private void OnHover(InputValue touchPos)
        {
            _touchPosition = touchPos.Get<Vector2>();

            FindHoverObject();

            _inputController.FollowPosition(_touchPosition);
        }

        private void OnTouch()
        {
            _isTouched = true;
            Debug.Log(_isTouched + " touch");
        }

        private void OnUntouch()
        {
            _isTouched = _isDragging = false;

            _inputController.UnSetFollow();
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
            if (hit.CompareTag(cardZone.ToString()))
            {
                Debug.Log("It on card zone");
            }
        }

        private void OnCard(GameObject hit)
        {
            if (hit.CompareTag(cards.ToString()))
            {
                if (!_isDragging)
                {
                    _isDragging = true;
                    _inputController.SetFollowItem(hit);
                }

                Debug.Log("It on card");
            }
        }

        // private void FollowPosition()
        // {
        //     if (!_followItem) return;
        //
        //     _followItem.position = _touchPosition;
        // }

        // private void ResetFollowPosition()
        // {
        //     if (!_followItem) return;
        //
        //     _followItem.position = _followItemCachedPosition;
        //
        //     _followItem = null;
        // }
    }
}