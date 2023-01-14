using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Bounce.Framework.Editor
{
    /// <summary>
    /// Helper class for instantiating ScriptableObjects.
    /// </summary>
    public class ScriptableObjectFactory
    {
        [MenuItem("Assets/Create/ScriptableObject", false, 0)]
        public static void Create()
        {
            var assembly = GetAssembly();

            // Get all classes derived from ScriptableObject
            var allScriptableObjects = (from t in assembly.GetTypes()
                                        where t.IsSubclassOf(typeof(ScriptableObject))
                                        select t).ToArray();

            // Show the selection window.
            var window = EditorWindow.GetWindow<ScriptableObjectWindow>(true, "Create a new ScriptableObject", true);
            window.ShowPopup();

            window.Types = allScriptableObjects;
        }

        /// <summary>
        /// Returns the assembly that contains the script code for this project (currently hard coded)
        /// </summary>
        private static Assembly GetAssembly()
        {
            return Assembly.Load(new AssemblyName("Assembly-CSharp"));
        }
    }
}