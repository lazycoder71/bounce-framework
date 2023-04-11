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
        [MenuItem("Assets/Create/Scriptable Object - Assembly-CSharp", false, 0)]
        public static void CreateAssemblyCSharp()
        {
            Create("Assembly-CSharp");
        }

        [MenuItem("Assets/Create/Scriptable Object - Bounce.Framework", false, 0)]
        public static void CreateBounceFramework()
        {
            Create("Bounce.Framework");
        }

        public static void Create(string assemblyName)
        {
            var assembly = GetAssembly(assemblyName);

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
        private static Assembly GetAssembly(string name)
        {
            return Assembly.Load(new AssemblyName(name));
        }
    }
}