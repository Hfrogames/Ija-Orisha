using DG.Tweening;
using UnityEngine;
using VInspector;

public class EnableOthers : MonoBehaviour
{
    /**
     * A utility component
     * it is used to enable another component base on @Role
     */
    private enum Role
    {
        None,
        AfterTimeout,
        OnPlayEvent
    }

    [SerializeField] Role role;

    [SerializeField] float delay = 0;

    private bool IsAfterTimeOut => role == Role.AfterTimeout;
    private bool IsPlayEvent => role == Role.OnPlayEvent;


    [SerializeField] GameObject[] othersToEnable;

    private void OnEnable()
    {
        if (IsAfterTimeOut) ManageEnableType();
        if (IsPlayEvent) EventPub.OnPlayEvent += GamePlayEventHandler;
    }

    private void OnDisable()
    {
        if (IsPlayEvent) EventPub.OnPlayEvent -= GamePlayEventHandler;
    }

    private void ManageEnableType()
    {
        switch (role)
        {
            case Role.OnPlayEvent:
            case Role.AfterTimeout:
                DOVirtual.DelayedCall(delay, EnableProvided);
                break;
        }
    }

    private void EnableProvided()
    {
        foreach (var t in othersToEnable)
        {
            t.SetActive(true);
        }
    }

    #region isGamePlayEvents

    [ShowIf(nameof(IsPlayEvent)), SerializeField]
    private PlayEvent playEvents;

    private void GamePlayEventHandler(PlayEvent playEvent)
    {
        if (playEvents == playEvent) ManageEnableType();
    }

    #endregion
}