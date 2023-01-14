using System;
using System.Linq;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace Bounce.Framework.Editor
{
    internal class EndNameEdit : EndNameEditAction
    {
        #region implemented abstract members of EndNameEditAction

        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            AssetDatabase.CreateAsset(EditorUtility.InstanceIDToObject(instanceId), AssetDatabase.GenerateUniqueAssetPath(pathName));
        }

        #endregion
    }

    /// <summary>
    /// Scriptable object window.
    /// </summary>
    public class ScriptableObjectWindow : EditorWindow
    {
        private string _strSearch = "";
        private Vector2 _scrollPos;

        private string[] _names;
        private Type[] _types;

        bool _focused = false;

        public Type[] Types
        {
            get { return _types; }
            set
            {
                _types = value;
                _names = _types.Select(t => t.FullName).ToArray();
            }
        }

        public void OnGUI()
        {
            //Search bar
            GUILayout.BeginHorizontal(GUI.skin.FindStyle("Toolbar"));
            GUI.SetNextControlName("SearchBar");
            _strSearch = GUILayout.TextField(_strSearch, GUI.skin.FindStyle("ToolbarSeachTextField"));

            if (GUILayout.Button("", GUI.skin.FindStyle("ToolbarSeachCancelButton")))
            {
                // Remove focus if cleared
                _strSearch = "";
                GUI.FocusControl(null);
            }
            GUILayout.EndHorizontal();

            if (_types == null)
                return;

            _scrollPos = GUILayout.BeginScrollView(_scrollPos, false, true);

            for (int i = 0; i < _types.Length; i++)
            {
                if (_strSearch != "" && _types[i].Name.IndexOf(_strSearch, StringComparison.OrdinalIgnoreCase) < 0)
                    continue;

                GUILayout.BeginHorizontal();
                if (GUILayout.Button(_types[i].Name))
                {
                    var asset = ScriptableObject.CreateInstance(_types[i]);
                    ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
                        asset.GetInstanceID(),
                        ScriptableObject.CreateInstance<EndNameEdit>(),
                        string.Format("{0}.asset", _names[i]),
                        AssetPreview.GetMiniThumbnail(asset),
                        null);

                    Close();
                }
                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();

            if (!_focused)
            {
                GUI.FocusControl("SearchBar");
                _focused = true;
            }
        }
    }
}