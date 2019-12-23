using UnityEditor;

namespace EditorTool
{
	public class EditorToolMenu
	{
        [MenuItem("Tools/ILRuntime Window")]
        public static void ExecuteBuildDLL()
        {
            var window = (ILRuntimeWindow)EditorWindow.GetWindow(typeof(ILRuntimeWindow), false, "ILRuntime Window");
            window.Show();
        }
    }
}

