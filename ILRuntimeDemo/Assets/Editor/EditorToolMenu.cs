using UnityEditor;

namespace EditorTool
{
	public class EditorToolMenu
	{
        [MenuItem("Tools/Build Hotfix Dll")]
        public static void ExecuteBuildDLL()
        {
            var window = (ILRuntimeBuildWindow)EditorWindow.GetWindow(typeof(ILRuntimeBuildWindow), false, "Build Hotfix Dll");
            window.Show();
        }
    }
}

