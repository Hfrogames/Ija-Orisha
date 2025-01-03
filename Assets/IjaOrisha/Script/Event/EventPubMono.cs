using UnityEngine;


    public class EventPubMono : MonoBehaviour
    {
        [SerializeField] private PlayEvent playEvent;

        public void Emit()
        {
            EventPub.Emit(playEvent);
        }
    }
