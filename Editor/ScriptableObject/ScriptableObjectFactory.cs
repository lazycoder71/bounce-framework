using System.Collections.Generic;
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
        [MenuItem("Assets/Create/Scriptable Object", false, 0)]
        public static void CreateAssembly()
        {
            Create("Assembly-CSharp", "Bounce.Framework", "Game");
        }

        public static void Create(params string[] assemblyNames)
        {
            List<System.Type> allScriptableObjects = new List<System.Type>();

            foreach (string assemblyName in assemblyNames)
            {
                var assembly = GetAssembly(assemblyName);

                if (assembly == null)
                    continue;

                allScriptableObjects.AddRange((from t in assembly.GetTypes()
                                               where t.IsSubclassOf(typeof(ScriptableObject))
                                               select t).ToArray());
            }

            // Show the selection window.
            var window = EditorWindow.GetWindow<ScriptableObjectWindow>(true, "Create a new ScriptableObject", true);
            window.ShowPopup();

            window.Types = allScriptableObjects.ToArray();
        }

        /// <summary>
        /// Returns the assembly that contains the script code for this project (currently hard coded)
        /// </summary>
        private static Assembly GetAssembly(string name)
        {
            try
            {
                return Assembly.Load(new AssemblyName(name));
            }
            catch
            {
                return null;
            }
        }
    }
}