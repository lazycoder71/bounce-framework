#if UNITY_EDITOR

using DG.DOTweenEditor;
using UnityEditor;
using UnityEngine;

namespace Bounce.Framework
{
    [CustomEditor(typeof(AnimationSequence))]
    public class AnimationSequenceInspector : Sirenix.OdinInspector.Editor.OdinEditor
    {
        protected override void OnEnable()
        {
            base.OnEnable();

            EditorApplication.playModeStateChanged += EditorApplication_PlayModeStateChanged;

#if UNITY_2021_1_OR_NEWER
            UnityEditor.SceneManagement.PrefabStage.prefabSaving += PrefabStage_PrefabSaving;
#else
            UnityEditor.Experimental.SceneManagement.PrefabStage.prefabSaving += PrefabStage_PrefabSaving;
#endif
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            EditorApplication.playModeStateChanged -= EditorApplication_PlayModeStateChanged;

#if UNITY_2021_1_OR_NEWER
            UnityEditor.SceneManagement.PrefabStage.prefabSaving -= PrefabStage_PrefabSaving;
#else
            UnityEditor.Experimental.SceneManagement.PrefabStage.prefabSaving -= PrefabStage_PrefabSaving;
#endif

            DOTweenEditorPreview.Stop(true);
        }

        private void EditorApplication_PlayModeStateChanged(PlayModeStateChange stateChanged)
        {
            DOTweenEditorPreview.Stop(true);
        }

        private void PrefabStage_PrefabSaving(GameObject prefab)
        {
            DOTweenEditorPreview.Stop(true);
        }
    }
}

#endif