using VInspector;
using UnityEngine;
using UnityEngine.UI;

namespace IjaOrisha
{
    public class PlayAudio : MonoBehaviour
    {
        /**
         * Sound Player component
         * it can be attached to any gameObject
         * it plays sound according to value provided in @PlayAudioType
         */
        private enum PlayAudioType
        {
            OnEnable,
            OnDisable,
            OnTrigger,
            OnExecute,
            OnClick
        }


        private bool IsOnEnable => playAudioType == PlayAudioType.OnEnable;
        private bool IsOnDisable => playAudioType == PlayAudioType.OnDisable;
        private bool IsOnTrigger => playAudioType == PlayAudioType.OnTrigger;
        private bool IsOnClick => playAudioType == PlayAudioType.OnClick;

        [SerializeField] private PlayAudioType playAudioType;
        [SerializeField] private SoundLib soundLib;

        [ShowIf(nameof(IsOnClick)), SerializeField]
        private Button button;

        [ShowIf(nameof(IsOnEnable)), SerializeField]
        private bool isPooled = false;

        private bool _isDonePooling = false;

        private void OnEnable()
        {
            if (IsOnEnable)
            {
                if (!isPooled) PlaySound();
                else if (isPooled && _isDonePooling) PlaySound();
            }
        }

        private void OnDisable()
        {
            if (IsOnDisable) PlaySound();

            if (isPooled && !_isDonePooling) _isDonePooling = true;
        }

        private void Start()
        {
            button?.onClick.AddListener(PlaySound);
        }

        public void PlaySound()
        {
            AudioManager.Instance.PlaySound(soundLib);
        }
    }
}