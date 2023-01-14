using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace Bounce.Framework
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIPopupBehaviour : MonoCached
    {
        // Global stack of popup
        static Stack<UIPopupBehaviour> s_popupStack = new Stack<UIPopupBehaviour>();

        [Header("Config")]
        [Min(0f)]
        [SerializeField] float _openDuration = 0.2f;
        [Min(0f)]
        [SerializeField] float _closeDuration = 0.2f;
        [SerializeField] bool _allowClose = true;

        [Header("Events")]
        [SerializeField] protected UnityEvent _onShow;
        [SerializeField] protected UnityEvent _onClosed;
        [SerializeField] protected UnityEvent _onClose;

        Sequence _sequence;
        CanvasGroup _canvasGroup;

        public float openDuration { get { return _openDuration; } }
        public float closeDuration { get { return _closeDuration; } }
        public bool allowClose { get { return _allowClose; } set { _allowClose = value; } }
        public CanvasGroup canvasGroup { get { return _canvasGroup; } }

        public UnityEvent onShow { get { return _onShow; } }
        public UnityEvent onClosed { get { return _onClosed; } }
        public UnityEvent onClose { get { return _onClose; } }

        public static int popupCount { get { return s_popupStack.Count; } }

        #region MonoBehaviour

        protected virtual void Awake()
        {
            // Get canvas group component and disable UI constrol at begin
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.interactable = false;

            ActivePopup();

            ConstructSequence();
        }

        protected virtual void OnDestroy()
        {
            DisablePopup();

            // Kill transition tween
            _sequence?.Kill();

            _onClosed?.Invoke();
        }

        private void Update()
        {
            InputUpdate();
        }

        #endregion

        #region Public

        public void Close()
        {
            if (!_allowClose)
                return;

            HandleClose();
        }

        public void ForceClose()
        {
            HandleClose();
        }

        public void SetEnabled(bool enabled)
        {
            this.enabled = enabled;
            _canvasGroup.interactable = enabled;
        }

        #endregion

        private void ConstructSequence()
        {
            // Init transition sequence
            _sequence = DOTween.Sequence()
                .SetAutoKill(false)
                .SetUpdate(true);
            _sequence.OnComplete(Sequence_OnComplete);
            _sequence.OnRewind(Sequence_OnRewind);

            // Construct transition sequence from popup transition components or a blank sequence with open duration
            UIPopupTransition[] popupTransitions = GetComponents<UIPopupTransition>();
            if (popupTransitions != null && _openDuration > 0f && _closeDuration > 0f)
            {
                for (int i = 0; i < popupTransitions.Length; i++)
                {
                    Tween tween = popupTransitions[i].ConstructTransition(this);

                    if (tween != null)
                        _sequence.Join(tween);
                }
            }
            else
            {
                // This popup doesn't have any transition sequence, only delay time
                _sequence.AppendInterval(1f);

                if (_openDuration <= 0f)
                {
                    _sequence.Complete();
                }
                else
                {
                    _sequence.timeScale = 1f / _openDuration;
                    _sequence.PlayForward();
                }
            }
        }

        private void Sequence_OnComplete()
        {
            SetEnabled(true);

            _onShow?.Invoke();
        }

        private void Sequence_OnRewind()
        {
            Destroy(gameObjectCached);
        }

        private void HandleClose()
        {
            // Can't close when it is transiting
            if (_sequence.IsPlaying())
                return;

            // Disable popup at this moment
            SetEnabled(false);

            // On close callback
            _onClose?.Invoke();

            if (_openDuration > 0f && _closeDuration > 0f)
            {
                // Set time scale to modify close duration
                _sequence.timeScale = _openDuration / _closeDuration;

                // Play sequence backward when close
                _sequence.PlayBackwards();
            }
            else
            {
                if (_closeDuration <= 0f)
                {
                    _sequence.Rewind();
                }
                else
                {
                    _sequence.timeScale = 1f / _closeDuration;
                    _sequence.PlayBackwards();
                }
            }
        }

        private void ActivePopup()
        {
            SetEnabled(true);

            // Disable current top popup
            if (s_popupStack.Count > 0)
                s_popupStack.Peek().SetEnabled(false);

            // Add this popup into stack
            s_popupStack.Push(this);
        }

        private void DisablePopup()
        {
            SetEnabled(false);

            // Pop this popup out of stack
            s_popupStack.Pop();

            // Get current top popup and enable it
            if (s_popupStack.Count > 0)
                s_popupStack.Peek().SetEnabled(true);
        }

        private void InputUpdate()
        {
#if UNITY_ANDROID || UNITY_EDITOR || UNITY_STANDALONE
            if (this.enabled && _canvasGroup.interactable)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                    Close();
            }
#endif
        }
    }
}