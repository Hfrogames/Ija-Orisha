using UnityEngine;
using VInspector;
using System;


namespace IjaOrisha
{
    public enum SoundLib
    {
        Theme,
        Intro,
        Click,
        SangoAttack,
        SangoDefence,
        OgunAttack,
        OgunDefence,
        ObatalaAttack,
        ObatalaDefence,
        EsuAttack,
        EsuDefence,
        OlokunAttack,
        OlokunDefence,
        OsunAttack,
        OsunDefence,
        YemojaAttack,
        YemojaDefence,
        OyaAttack,
        OyaDefence,
        OsanyinAttack,
        OsanyinDefence,
        ObaAttack,
        ObaDefence,
    }

    public class AudioManager : MonoBehaviour
    {
        /**
         * It manages all sound related operation
         * it plays sound when event occurs
         * it used my PlayAudio to play needed sound
         */
        public static AudioManager Instance { get; private set; }


        [Foldout("Audio Source"), SerializeField]
        private AudioSource themeSource, uiSource;

        [SerializeField] private AudioSource[] sfxSources;

        [Foldout("Audio Clips"), SerializeField]
        private SerializedDictionary<SoundLib, AudioClip> soundClips;


        [EndFoldout]

        // source last audio source used
        private int _lastSfxSourceIndex = -1;


        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void OnEnable()
        {
            EventPub.OnPlayEvent += OnPlayEvent;
        }

        private void OnDisable()
        {
            EventPub.OnPlayEvent -= OnPlayEvent;
        }

        private void OnPlayEvent(PlayEvent playEvent)
        {
            switch (playEvent)
            {
            }
        }

        private AudioSource SoundSource(SoundLib soundLib)
        {
            switch (soundLib)
            {
                case SoundLib.Theme:
                case SoundLib.Intro:
                    return themeSource;
                case SoundLib.Click:
                    return uiSource;
                default:
                    _lastSfxSourceIndex = (_lastSfxSourceIndex + 1) % sfxSources.Length;
                    return sfxSources[_lastSfxSourceIndex];
            }
        }

        public void PlaySound(SoundLib sound)
        {
            if (soundClips.TryGetValue(sound, out var clip))
            {
                AudioSource soundSource = SoundSource(sound);
                soundSource.resource = clip;

                soundSource.Play();
            }
        }

        public void PlaySound(string soundName)
        {
            if (Enum.TryParse(soundName, out SoundLib sound))
            {
                PlaySound(sound);
            }
            else
            {
                Debug.LogWarning($"Sound '{soundName}' not found in SoundLib.");
            }
        }
    }
}