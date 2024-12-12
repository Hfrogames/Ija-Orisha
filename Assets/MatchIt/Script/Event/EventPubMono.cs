using UnityEngine;

namespace MatchIt.Script.Event
{
    public class EventPubMono : MonoBehaviour
    {
        [SerializeField] private PlayEvent playEvent;

        public void Emit()
        {
            EventPub.Emit(playEvent);
        }
    }
}