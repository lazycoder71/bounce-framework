using DG.DOTweenEditor;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Bounce.Framework
{
    [ExecuteInEditMode]
    public class AnimationSequence : MonoCached
    {
        [ListDrawerSettings(ListElementLabelName = "displayName", ShowIndexLabels = true)]
        [SerializeReference] List<AnimationSequenceStep> _steps;

        Sequence _sequence;

        public Sequence sequence { get { return _sequence; } }

        private void OnDestroy()
        {
            _sequence?.Kill();
        }

        [Button("Play", Icon = SdfIconType.Play)]
        public void PreviewPlay()
        {
            ConstructSequence();

            DOTweenEditorPreview.Stop(true);
            DOTweenEditorPreview.PrepareTweenForPreview(_sequence);
            DOTweenEditorPreview.Start();
        }

        private void ConstructSequence()
        {
            _sequence?.Restart();
            _sequence?.Kill();
            _sequence = DOTween.Sequence();

            for (int i = 0; i < _steps.Count; i++)
            {
                _steps[i].AddTweenToSequence(this);
            }

            if (!Application.isPlaying)
                _sequence.SetAutoKill(false);
        }

        private void OnGUI()
        {
            if (UnityEditor.Selection.activeGameObject != this.gameObject)
            {
                DOTweenEditorPreview.Stop(true);
                _sequence?.Restart();
                _sequence?.Kill();
            }
        }
    }
}