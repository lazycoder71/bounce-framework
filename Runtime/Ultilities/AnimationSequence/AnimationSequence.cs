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

        [Button(Name = "", Icon = SdfIconType.Play, ButtonHeight = 50), PropertyOrder(-1)]
        public void Preview()
        {
            Stop();

            Construct();

            DOTweenEditorPreview.PrepareTweenForPreview(_sequence);
            DOTweenEditorPreview.Start();
        }

        private void Stop()
        {
            _sequence?.Restart();
            _sequence?.Kill();

            DOTweenEditorPreview.Stop(true);
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