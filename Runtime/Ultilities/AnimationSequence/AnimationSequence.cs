using DG.DOTweenEditor;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bounce.Framework
{
    [ExecuteInEditMode]
    public class AnimationSequence : MonoCached
    {
        [Title("Steps")]
        [ListDrawerSettings(ShowIndexLabels = false, OnBeginListElementGUI = "BeginDrawListElement", OnEndListElementGUI = "EndDrawListElement")]
        [SerializeReference] List<AnimationSequenceStep> _steps = new List<AnimationSequenceStep>();

        [Title("Settings")]
        [SerializeField] bool _isAutoKill = true;
        [SerializeField] bool _playOnEnable = false;
        [MinValue(-1), HorizontalGroup("Loop")]
        [SerializeField] int _loopCount;
        [ShowIf("@_loopCount<0"), HorizontalGroup("Loop"), LabelWidth(75.0f)]
        [SerializeField] LoopType _loopType;

        Sequence _sequence;

        RectTransform _rectTransform;
        Graphic _graphic;

        public RectTransform rectTransform
        {
            get
            {
                if (_rectTransform == null)
                    _rectTransform = GetComponent<RectTransform>();

                return _rectTransform;
            }
        }

        public Graphic graphic
        {
            get
            {
                if (_graphic == null)
                    _graphic = GetComponent<Graphic>();

                return _graphic;
            }
        }

        public Sequence sequence { get { return _sequence; } }

        private void OnDestroy()
        {
            _sequence?.Kill();
        }

        private void OnEnable()
        {
            Construct();

            if (_playOnEnable)
            {
                _sequence.Restart();
                _sequence.Play();
            }
        }

        private void OnGUI()
        {
            if (UnityEditor.Selection.activeGameObject != this.gameObject)
            {
                Stop();
            }
        }

        [ButtonGroup(Order = -1, ButtonHeight = 25)]
        [Button(Name = "", Icon = SdfIconType.SkipBackwardFill)]
        public void Rewind()
        {
            _sequence?.Rewind();
        }

        [ButtonGroup]
        [Button(Name = "", Icon = SdfIconType.SkipStartFill)]
        public void PlayBackward()
        {
            _sequence?.PlayBackwards();
        }

        [ButtonGroup]
        [Button(Name = "", Icon = SdfIconType.PlayFill)]
        public void Play()
        {
            Stop();

            Construct();

            DOTweenEditorPreview.PrepareTweenForPreview(_sequence);
            DOTweenEditorPreview.Start();
        }

        [ButtonGroup]
        [Button(Name = "", Icon = SdfIconType.SkipEndFill)]
        public void PlayFoward()
        {
            _sequence?.PlayForward();
        }

        [ButtonGroup]
        [Button(Name = "", Icon = SdfIconType.SkipForwardFill)]
        public void Complete()
        {
            _sequence?.Complete();
        }

        [ButtonGroup]
        [Button(Name = "", Icon = SdfIconType.StopFill)]
        private void Stop()
        {
            DOTweenEditorPreview.Stop(true);

            _sequence?.Kill();
            _sequence = null;
        }

        private void Construct()
        {
            if (_sequence.IsActive())
                return;

            _sequence?.Kill();
            _sequence = DOTween.Sequence();

            _sequence.SetLoops(_loopCount, _loopType);
            _sequence.SetAutoKill(Application.isPlaying ? _isAutoKill : false);

            for (int i = 0; i < _steps.Count; i++)
            {
                _steps[i].AddToSequence(this);
            }
        }

        private void BeginDrawListElement(int index)
        {
            SirenixEditorGUI.BeginBox(_steps[index].displayName);
        }

        private void EndDrawListElement(int index)
        {
            SirenixEditorGUI.EndBox();
        }
    }
}