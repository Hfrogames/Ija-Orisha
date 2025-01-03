using UnityEngine;
using UnityEngine.Events;

public class EventSubMono : MonoBehaviour
{
    [SerializeField] private PlayEvent playEvent;
    [SerializeField] UnityEvent onEvent;

    private void OnEnable()
    {
        EventPub.OnPlayEvent += OnPlayEvent;
    }

    private void OnDisable()
    {
        EventPub.OnPlayEvent -= OnPlayEvent;
    }

    private void OnPlayEvent(PlayEvent triggeredEvent)
    {
        if (playEvent == triggeredEvent) onEvent?.Invoke();
    }
}