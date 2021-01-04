#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace OpenBack.Editor
{

    public class OpenBackPreBuild : IPreprocessBuildWithReport {
        public int callbackOrder { get { return 0; } }

        public void OnPreprocessBuild (BuildReport report) {
            BuildTarget target = report.summary.platform;
            if (target == BuildTarget.iOS || target == BuildTarget.Android) {
                OpenBackConfig config = OpenBackConfig.Load();
                config.Apply();
            }
        }
    }
}
#endif