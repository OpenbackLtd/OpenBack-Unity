#if UNITY_EDITOR && UNITY_IOS
using System;
using System.IO;
using OpenBack.Editor;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;

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

    [PostProcessBuildAttribute(1)]
    private static void OnPostProcessBuild(BuildTarget target, string buildPath) {
        // App plist
		string plistPath = buildPath + "/Info.plist";
		PlistDocument plist = new PlistDocument ();
		plist.ReadFromString (File.ReadAllText (plistPath));
		PlistElementDict rootDict = plist.root;

        // Background Modes
		PlistElementArray bgModes = rootDict.CreateArray ("UIBackgroundModes");
		bgModes.AddString ("remote-notification");
		bgModes.AddString ("fetch");
		bgModes.AddString ("processing");

        // Background Task
		PlistElementArray bgTasks = rootDict.CreateArray ("BGTaskSchedulerPermittedIdentifiers");
		bgTasks.AddString ("com.openback.bgtask.refresh");
		bgTasks.AddString ("com.openback.bgtask.check");

        // Allowed deep links
        //PlistElementArray queriesSchemes = rootDict.CreateArray("LSApplicationQueriesSchemes");
        //queriesSchemes.AddString("instagram");

        // OpenBack Settings
        OpenBackConfig config = OpenBackConfig.Load();
        var openback = rootDict.CreateDict("OpenBack");
        if (!String.IsNullOrEmpty(config.IOSAppCode)) {
		    openback.SetString("AppCode", config.IOSAppCode);
        }
        if (!String.IsNullOrEmpty(config.IOSAppGroup)) {
            AddAppGroup(buildPath, config.IOSAppGroup);
		    openback.SetString("AppGroup", config.IOSAppGroup);
        }
        openback.SetInteger("DebugLevel", (int)config.IOSLogLevel);
        openback.SetBoolean("EnableAutoStart", config.IOSEnableAutoStart);
        openback.SetBoolean("ClearBadgeCount", config.IOSEnableAutoStart);
        openback.SetBoolean("EnableCoreMotionActivity", config.IOSEnableCoreMotion);
        openback.SetBoolean("EnableMicrophone", config.IOSEnableMicrophone);
        openback.SetBoolean("EnableProximity", config.IOSEnableProximity);
        openback.SetBoolean("EnableSwizzle", config.IOSEnableSwizzle);
        if (!String.IsNullOrEmpty(config.IOSNotificationLargeIcon)) {
		    openback.SetString("LargeIcon", config.IOSNotificationLargeIcon);
        }
        if (!String.IsNullOrEmpty(config.IOSNotificationSound)) {
		    openback.SetString("NotificationSound", config.IOSNotificationSound);
        }

        File.WriteAllText (plistPath, plist.WriteToString ());
    }

    private static void AddAppGroup(string buildPath, string appGroup) {
        string projectPath = PBXProject.GetPBXProjectPath(buildPath);
        var project = new PBXProject();
        project.ReadFromString(File.ReadAllText(projectPath));
        
        #if UNITY_2019_3_OR_NEWER
            string targetGuid = project.GetUnityMainTargetGuid();
            var manager = new ProjectCapabilityManager(projectPath, "Entitlements.entitlements", null, targetGuid);
        #else
            string targetGuid = project.TargetGuidByName("Unity-iPhone");
            var manager = new ProjectCapabilityManager(projectPath, "Entitlements.entitlements", targetGuid);
        #endif
        manager.AddAppGroups(new string[] { appGroup });
        manager.WriteToFile();
    }
}
#endif