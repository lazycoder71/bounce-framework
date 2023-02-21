using DG.DOTweenEditor;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using System.Collections.Generic;
using UnityEngine;

namespace Bounce.Framework
{
    [ExecuteInEditMode]
    public class AnimationSequence : MonoCached
    {
        [ListDrawerSettings(ShowIndexLabels = true, OnBeginListElementGUI = "BeginDrawListElement", OnEndListElementGUI = "EndDrawListElement")]
        [SerializeReference] List<AnimationSequenceStep> _steps = new List<AnimationSequenceStep>();

        [SerializeField] AnimationSequenceSettings _settings;

        Sequence _sequence;

        public Sequence sequence { get { return _sequence; } }

        private void OnDestroy()
        {
            _sequence?.Kill();
        }

        private void Awake()
        {
            Construct();
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
            _sequence?.Kill();
            _sequence = DOTween.Sequence();

            for (int i = 0; i < _steps.Count; i++)
            {
                _steps[i].AddToSequence(this);
            }

            if (!Application.isPlaying)
                _sequence.SetAutoKill(false);
        }

        private void OnGUI()
        {
            if (UnityEditor.Selection.activeGameObject != this.gameObject)
            {
                Stop();
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