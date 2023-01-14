using DG.Tweening;
using UnityEngine;

namespace Bounce.Framework
{
    public class AudioScript : MonoCached
    {
        AudioConfig _config;
        AudioSource _audioSource;

        Tween _tween;

        public AudioSource audioSource
        {
            get
            {
                if (_audioSource == null)
                    _audioSource = gameObject.GetComponent<AudioSource>();
                return _audioSource;
            }
        }

        #region MonoBehaviour

        void Awake()
        {
            audioSource.rolloffMode = AudioRolloffMode.Linear;

            transformCached.SetParent(AudioManager.instance.transformCached);

            AudioManager.volumeSound.eventValueChanged += VolumeSound_EventValueChanged;
            AudioManager.volumeMusic.eventValueChanged += VolumeMusic_EventValueChanged;
        }

        void OnDestroy()
        {
            _tween?.Kill();
        }

        #endregion

        #region Public

        public void Play(AudioConfig config, bool loop = false)
        {
            Construct(config, loop);

            _tween?.Kill();

            if (!loop)
                _tween = DOVirtual.DelayedCall(config.Clip.length, Stop);
        }

        public void PlayFadeIn(AudioConfig config, float fadeDuration, bool loop = false)
        {
            Construct(config, loop);

            fadeDuration = Mathf.Min(fadeDuration, config.Clip.length);

            _tween?.Kill();
            _tween = audioSource.DOFade(GetVolume(), fadeDuration)
               .ChangeStartValue(0f)
               .SetUpdate(true)
               .OnComplete(() =>
               {
                   _tween?.Kill();

                   if (!loop)
                       _tween = DOVirtual.DelayedCall(config.Clip.length - fadeDuration, Stop);
               });
        }

        public void FadeOut(float duration = 0.5f)
        {
            _tween?.Kill();
            _tween = audioSource.DOFade(0f, duration)
                .ChangeStartValue(GetVolume())
                .SetUpdate(true)
                .OnComplete(Stop);
        }

        public void Stop()
        {
            if (AudioManager.isDestroyed)
                return;

            _tween?.Kill();

            audioSource.Stop();

            AudioManager.ReturnPool(this);
        }

        #endregion

        float GetVolume()
        {
            return _config.VolumeScale * (_config.Type == AudioType.Music ? AudioManager.volumeMusic.value : AudioManager.volumeSound.value);
        }

        void VolumeSound_EventValueChanged(float volume)
        {
            UpdateVolume();
        }

        void VolumeMusic_EventValueChanged(float volume)
        {
            UpdateVolume();
        }

        void Construct(AudioConfig config, bool loop = false)
        {
            _config = config;

            audioSource.clip = config.Clip;
            audioSource.loop = loop;
            audioSource.minDistance = config.Distance.x;
            audioSource.maxDistance = config.Distance.y;
            audioSource.spatialBlend = config.Is3D ? 1f : 0f;

            UpdateVolume();

            audioSource.Play();
        }

        void UpdateVolume()
        {
            if (_config == null)
                return;

            float volumeFinal = GetVolume();

            audioSource.mute = volumeFinal <= 0;
            audioSource.volume = volumeFinal;
        }
    }
}