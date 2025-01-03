using System;
using DG.Tweening;
using UnityEngine;


    public class CardFlip : MonoBehaviour
    {
        [SerializeField] private RectTransform front;
        [SerializeField] private RectTransform back;

        private void OnDisable()
        {
            ResetPose();
        }

        private void ResetPose()
        {
            front.gameObject.SetActive(false);
            front.rotation = Quaternion.Euler(0, 0, 0);
            back.rotation = Quaternion.Euler(0, 0, 0);
            back.gameObject.SetActive(true);
        }

        public Sequence Flip()
        {
            Sequence flipSequence = DOTween.Sequence();

            flipSequence
                .AppendInterval(.25f)
                .AppendCallback(ResetPose)
                .Join(back.DORotate(new Vector3(0, 90, 0), 0.25f).SetEase(Ease.InSine))
                .AppendCallback(() =>
                {
                    back.gameObject.SetActive(false);
                    front.rotation = Quaternion.Euler(0, 90, 0);
                    front.gameObject.SetActive(true);
                })
                .Append(front.DORotate(new Vector3(0, 0, 0), 0.25f).SetEase(Ease.OutSine));

            return flipSequence;
        }

        public void Play()
        {
            Flip();
        }
    }
