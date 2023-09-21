using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bounce.Framework
{
    public class AnimationSequence : MonoCached
    {
        [Serializable, Flags]
        public enum ActionOnEnable
        {
            Construct = 1 << 1,
            Restart = 1 << 2,
            Play = 1 << 3,
            All = Construct | Restart | Play
        }

        [Serializable, Flags]
        public enum ActionOnDisable
        {
            Kill = 1 << 1,
        }

        [Title("Steps")]
        [ListDrawerSettings(ShowIndexLabels = false, OnBeginListElementGUI = "BeginDrawListElement", OnEndListElementGUI = "EndDrawListElement")]
        [SerializeReference] List<AnimationSequenceStep> _steps = new List<AnimationSequenceStep>();

        [Title("Settings")]
        [SerializeField] bool _isAutoKill = true;

        [SerializeField] ActionOnEnable _actionOnEnable = ActionOnEnable.Construct;
        [SerializeField] ActionOnDisable _actionOnDisable;

        [MinValue(-1), HorizontalGroup("Loop")]
        [SerializeField] int _loopCount;
        [ShowIf("@_loopCount != 0"), HorizontalGroup("Loop"), LabelWidth(75.0f)]
        [SerializeField] LoopType _loopType;

        [SerializeField, HorizontalGroup("Update")]
        [InlineButton("@_isIndependentUpdate = true", Label = "Timescale Based", ShowIf = ("@_isIndependentUpdate == false"))]
        [InlineButton("@_isIndependentUpdate = false", Label = "Independent Update", ShowIf = ("@_isIndependentUpdate == true"))]
        protected UpdateType _updateType = UpdateType.Normal;

        [SerializeField, HideInInspector]
        protected bool _isIndependentUpdate = false;

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
            if (_actionOnEnable.HasFlag(ActionOnEnable.Construct))
                Construct();

            if (_actionOnEnable.HasFlag(ActionOnEnable.Restart))
                _sequence?.Restart();

            if (_actionOnEnable.HasFlag(ActionOnEnable.Play))
                _sequence?.Play();
        }

        private void OnDisable()
        {
            if (_actionOnDisable.HasFlag(ActionOnDisable.Kill))
                _sequence?.Kill();
        }

        private void Construct()
        {
            if (_sequence.IsActive())
                return;

            _sequence?.Kill();
            _sequence = DOTween.Sequence();

            for (int i = 0; i < _steps.Count; i++)
            {
                _steps[i].AddToSequence(this);
            }

            _sequence.SetAutoKill(_isAutoKill);

            _sequence.SetLoops(_loopCount, _loopType);

            _sequence.SetUpdate(_updateType, _isIndependentUpdate);
        }

#if UNITY_EDITOR
        [ButtonGroup(Order = -1, ButtonHeight = 25)]
        [Button(Name = "", Icon = SdfIconType.PlayFill)]
        private void Play()
        {
            _sequence?.Restart();

            Construct();

            DG.DOTweenEditor.DOTweenEditorPreview.PrepareTweenForPreview(_sequence);
            DG.DOTweenEditor.DOTweenEditorPreview.Start();
        }

        [ButtonGroup]
        [Button(Name = "", Icon = SdfIconType.SkipStartFill)]
        private void PlayBackward()
        {
            _sequence?.PlayBackwards();
        }

        [ButtonGroup]
        [Button(Name = "", Icon = SdfIconType.SkipEndFill)]
        private void PlayFoward()
        {
            _sequence?.PlayForward();
        }

        [ButtonGroup]
        [Button(Name = "", Icon = SdfIconType.StopFill)]
        private void Stop()
        {
            DG.DOTweenEditor.DOTweenEditorPreview.Stop(true);

            _sequence?.Kill();
            _sequence = null;
        }

        [ButtonGroup]
        [Button(Name = "", Icon = SdfIconType.SkipBackwardFill)]
        private void Rewind()
        {
            _sequence?.Rewind();
        }

        [ButtonGroup]
        [Button(Name = "", Icon = SdfIconType.SkipForwardFill)]
        private void Complete()
        {
            _sequence?.Complete();
        }

        private void BeginDrawListElement(int index)
        {
            Sirenix.Utilities.Editor.SirenixEditorGUI.BeginBox(_steps[index].displayName);
        }

        private void EndDrawListElement(int index)
        {
            Sirenix.Utilities.Editor.SirenixEditorGUI.EndBox();
        }

#endif
    }
}