#if UNITY_EDITOR && UNITY_IOS
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;

public class OpenBackPostProcess {
    // Add the Unity-iPhone target to make sure Cocoapods adds dynamic frameworks
    // to the applicaiton and appends it's processing shell to strip unused archs
    // See https://github.com/googlesamples/unity-jar-resolver/issues/405
    [PostProcessBuildAttribute(45)]
    private static void TweakPodfile(BuildTarget target, string buildPath) {
        if (target == BuildTarget.iOS) {
            using (StreamWriter sw = File.AppendText(buildPath + "/Podfile")) {                
                sw.WriteLine("\ntarget 'Unity-iPhone' do\nend");
            }
        }
    }
}
#endif