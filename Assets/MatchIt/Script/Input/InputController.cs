using Shapes2D;
using UnityEngine;

namespace MatchIt.Script.Input
{
    public class InputController
    {
        private GameObject _followItem;
        private Vector3 _followItemCachedPos;

        public void SetFollowItem(GameObject followItem)
        {
            _followItem = followItem;
            _followItemCachedPos = followItem.transform.position;
        }

        public void FollowPosition(Vector3 touchPosition)
        {
            if (!_followItem) return;
            _followItem.transform.position = touchPosition;
        }

        public void UnSetFollow()
        {
            if (!_followItem) return;

            _followItem.transform.position = _followItemCachedPos;
            _followItem = null;
        }
    }
}