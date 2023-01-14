using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace Bounce.Framework
{
    public class SceneLoader : MonoCached
    {
        static readonly string s_fadeIn = "FadeIn";
        static readonly string s_fadeOut = "FadeOut";

        enum State
        {
            // Wait for load scene command
            Idle,
            // Playing fade in animation
            FadeIn,
            // End of fade in animation, load new scene
            Loading,
            // Playing fade out animation
            FadeOut,
        }

        // Speed to fade in and fade out
        float _fadeInAnimSpeed = 0;
        float _fadeOutAnimSpeed = 0;

        // Index of scene will be loaded
        int _sceneIndex = 0;
        // Scene async
        AsyncOperation _sceneAsync;
        // Animator component
        Animator _animator;
        // State machine
        StateMachine<State> _stateMachine;
        // Tween
        Tween _tween;

        #region MonoBehaviour

        void Start()
        {
            gameObjectCached.SetActive(false);
        }

        void OnDestroy()
        {
            _tween?.Kill();
        }

        void Update()
        {
            _stateMachine.Update();
        }

        #endregion

        #region States

        void State_OnFadeInStart()
        {
            // Load scene async
            _sceneAsync = SceneManager.LoadSceneAsync(_sceneIndex);
            _sceneAsync.allowSceneActivation = false;

            //Play fade in animation
            _animator.Play(s_fadeIn);
            _animator.speed = _fadeInAnimSpeed;

            //Wait until animation is end
            _tween?.Kill();
            _tween = DOVirtual.DelayedCall(BFactory.sceneTransitionFadeInDuration + BFactory.sceneTransitionLoadDuration, () =>
            {
                _stateMachine.CurrentState = State.Loading;
                _sceneAsync.allowSceneActivation = true;
            }, true);
        }

        void State_OnLoadingUpdate()
        {
            if (_sceneAsync.isDone)
            {
                _stateMachine.CurrentState = State.FadeOut;
            }
        }

        void State_OnFadeOutStart()
        {
            //Play fade out animation
            _animator.Play(s_fadeOut);
            _animator.speed = _fadeOutAnimSpeed;

            //Wait until animation is end
            _tween?.Kill();
            _tween = DOVirtual.DelayedCall(BFactory.sceneTransitionFadeOutDuration, () =>
            {
                _stateMachine.CurrentState = State.Idle;
                gameObjectCached.SetActive(false);
            }, true);
        }

        #endregion

        #region Public

        public void Load(int sceneIndex)
        {
            if (_stateMachine.CurrentState != State.Idle)
            {
                BDebug.Log("[{0}] A scene is loading, can't execute load scene command!", typeof(SceneLoader));
                return;
            }

            gameObjectCached.SetActive(true);

            _sceneIndex = sceneIndex;
            _stateMachine.CurrentState = State.FadeIn;
        }

        public void Reload()
        {
            Load(SceneManager.GetActiveScene().buildIndex);
        }

        public void Construct()
        {
            // Get component reference
            _animator = GetComponentInChildren<Animator>();

            // Calculate fade in/out speed
            CalculateAnimSpeed();

            // Construct state machine
            _stateMachine = new StateMachine<State>();
            _stateMachine.AddState(State.FadeIn, State_OnFadeInStart);
            _stateMachine.AddState(State.Loading, null, State_OnLoadingUpdate);
            _stateMachine.AddState(State.FadeOut, State_OnFadeOutStart);
            _stateMachine.AddState(State.Idle);

            _stateMachine.CurrentState = State.Idle;
        }

        #endregion

        void CalculateAnimSpeed()
        {
            AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;

            foreach (AnimationClip clip in clips)
            {
                if (clip.name == s_fadeIn)
                {
                    _fadeInAnimSpeed = clip.length / BFactory.sceneTransitionFadeInDuration;
                }
                else if (clip.name == s_fadeOut)
                {
                    _fadeOutAnimSpeed = clip.length / BFactory.sceneTransitionFadeOutDuration;
                }
            }
        }
    }

    public static class SceneLoaderHelper
    {
        static SceneLoader _instance;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void LazyInit()
        {
            if (_instance == null)
            {
                _instance = BFactory.sceneTransitionPrefab.Create().GetComponent<SceneLoader>();
                _instance.Construct();

                GameObject.DontDestroyOnLoad(_instance.gameObjectCached);
            }
        }

        public static void Load(int sceneIndex)
        {
            LazyInit();

            _instance.Load(sceneIndex);
        }

        public static void Reload()
        {
            LazyInit();

            _instance.Reload();
        }
    }
}