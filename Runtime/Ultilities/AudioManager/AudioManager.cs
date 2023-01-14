using UnityEngine;

namespace Bounce.Framework
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        public static BValue<float> volumeMusic = new BValue<float>(1.0f);
        public static BValue<float> volumeSound = new BValue<float>(1.0f);

        ComponentPool<AudioScript> _pool;

        #region MonoBehaviour

        protected override void Awake()
        {
            base.Awake();

            InitPool();
        }

        #endregion

        #region Public

        public static AudioScript Play(AudioConfig config, Vector3 pos, bool loop = false)
        {
            if (config.Clip == null)
                return null;

            AudioScript audio = instance._pool.Get();
            audio.enabled = true;
            audio.Play(config, loop);
            audio.transformCached.position = pos;

            return audio;
        }

        public static AudioScript Play(AudioConfig config, bool loop = false)
        {
            return Play(config, Vector3.zero, loop);
        }

        public static void ReturnPool(AudioScript audioScript)
        {
            instance._pool.Release(audioScript);
        }

        #endregion

        void InitPool()
        {
            GameObject objAudio = new GameObject(typeof(AudioScript).ToString(), typeof(AudioSource), typeof(AudioScript));

            _pool = new ComponentPool<AudioScript>(objAudio);

            objAudio.SetActive(false);
        }
    }
}