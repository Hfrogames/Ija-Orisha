using DG.Tweening;
using UnityEngine;
using VInspector;

public class DisableSelf : MonoBehaviour
{
    /**
     * A utility component
     * it disable itself base on @Role
     */
    private enum Role
    {
        OnTimeout,
        OnPlayEvent
    }

    private bool IsAfterTimeout => role == Role.OnTimeout;
    private bool IsPlayEvent => role == Role.OnPlayEvent;


    [SerializeField] Role role;

    [SerializeField] float timeout = 0;

    private void OnEnable()
    {
        if (IsAfterTimeout) ManageDisableState();
        if (IsPlayEvent) EventPub.OnPlayEvent += GamePlayEventHandler;
    }

    private void OnDisable()
    {
        if (IsPlayEvent) EventPub.OnPlayEvent -= GamePlayEventHandler;
    }


    private void ManageDisableState()
    {
        switch (role)
        {
            case Role.OnTimeout:
            case Role.OnPlayEvent:
                DOVirtual.DelayedCall(timeout, HandleDisable);
                break;
        }
    }

    private void HandleDisable()
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }

    #region isPlayEvent

    [ShowIf(nameof(IsPlayEvent)), SerializeField]
    private PlayEvent playEvents;

    private void GamePlayEventHandler(PlayEvent playEvent)
    {
        if (playEvents == playEvent) ManageDisableState();
    }

    #endregion
}