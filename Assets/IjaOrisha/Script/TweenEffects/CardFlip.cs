using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;


public class CardFlip : MonoBehaviour
{
    [SerializeField] private RectTransform card;
    [SerializeField] private RectTransform emptyFront;
    [SerializeField] private RectTransform back;

    private void OnDisable()
    {
        ResetPose();
    }

    public void ResetPose()
    {
        card.gameObject.SetActive(false);
        emptyFront.gameObject.SetActive(false);
        card.rotation = Quaternion.Euler(0, 0, 0);
        emptyFront.rotation = Quaternion.Euler(0, 0, 0);
        back.rotation = Quaternion.Euler(0, 0, 0);
        back.gameObject.SetActive(true);
    }

    public Sequence Flip(float duration = .2f, bool isEmpty = false)
    {
        Sequence flipSequence = DOTween.Sequence();

        RectTransform front = isEmpty ? emptyFront : card;
        flipSequence
            .AppendInterval(.25f)
            .AppendCallback(ResetPose)
            .Join(back.DORotate(new Vector3(0, 90, 0), duration).SetEase(Ease.InSine))
            .AppendCallback(() =>
            {
                back.gameObject.SetActive(false);
                front.rotation = Quaternion.Euler(0, 90, 0);
                front.gameObject.SetActive(true);
            })
            .Append(front.DORotate(new Vector3(0, 0, 0), duration).SetEase(Ease.OutSine));

        return flipSequence;
    }

    public Tween KnockOut(float direction)
    {
        return emptyFront.transform.DOLocalMoveX(direction, .2f).SetEase(Ease.OutSine);
    }

    public void Play(bool isEmpty)
    {
        Flip(.1f, isEmpty);
    }
}