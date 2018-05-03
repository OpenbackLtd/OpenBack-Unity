using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections;
using UnityEditor.iOS.Xcode;
using System.IO;

public class OpenBackPluginPostProcessBuild
{
	// Configurable OpenBack options
	public const string kOBKConfigAppCode = "OBKApplicationID";
	public const string kOBKConfigAdvertisingId = "OBKConfigAdvertisingID";
	public const string kOBKConfigUserInfo = "OBKConfigUserInfo";
	public const string kOBKConfigLogLevel = "OBKConfigLogLevel";
	public const string kOBKConfigNotificationSound = "OBKConfigNotificationSound";
	public const string kOBKConfigEnableAlertNotifications = "OBKEnableAlertNotifications";
	public const string kOBKConfigEnableInAppNotifications = "OBKEnableInAppNotifications";
	public const string kOBKConfigEnableRemoteNotifications = "OBKEnableRemoteNotifications";
	public const string kOBKConfigEnableProximity = "OBKEnableProximity";
	public const string kOBKConfigEnableMotionCoprocessor = "OBKEnableMotionCoprocessor";
	public const string kOBKConfigEnableMicrophone = "OBKEnableMicrophone";
	public const string kOBKConfigEnableLocationAlways = "OBKEnableLocation";
	public const string kOBKConfigEnableLocationWhenInUse = "OBKEnableLocationWhenInUse";
	public const string kOBKConfigRequestAlertNotificationsAuthorization = "OBKRequestAlertNotificationsAuthorization";
	public const string kOBKConfigRequestMotionCoprocessorAuthorization = "OBKRequestMotionCoprocessorAuthorization";
	public const string kOBKConfigRequestMicrophoneAuthorization = "OBKRequestMicrophoneAuthorization";
	public const string kOBKConfigRequestLocationAlwaysAuthorization = "OBKRequestLocationAlwaysAuthorization";
	public const string kOBKConfigRequestLocationWhenInUseAuthorization = "OBKRequestLocationWhenInUseAuthorization";
	public const string kOBKConfigMinimumBackgroundFetchInterval = "OBKMinimumFetchInterval";

	// OpenBack log level
	public enum LogLevel
	{
		NONE = 0, ERROR = 1, WARNING = 2,
		INFO = 3, DEBUG = 4, VERBOSE = 5
	}

	static void UpdateInfoPList (string pathToBuiltProject)
	{
		// Get plist
		string plistPath = pathToBuiltProject + "/Info.plist";
		PlistDocument plist = new PlistDocument ();
		plist.ReadFromString (File.ReadAllText (plistPath));
		// Get root
		PlistElementDict rootDict = plist.root;

		// Background Modes - Required
		PlistElementArray bgModes = rootDict.CreateArray ("UIBackgroundModes");
		bgModes.AddString ("remote-notification");
		bgModes.AddString ("fetch");

		// LSApplicationQueriesSchemes - Optional - Example for instagram
		//	PlistElementArray queriesSchemes = rootDict.CreateArray("LSApplicationQueriesSchemes");
		//	queriesSchemes.AddString ("instagram");

		// NSAppTransportSecurity - Optional - Example to allow any HTTP request
		//	PlistElementDict ats = rootDict.CreateArray("NSAppTransportSecurity");
		//	ats.SetBoolean ("NSAllowsArbitraryLoads", true);

		// Write back to file
		File.WriteAllText (plistPath, plist.WriteToString ());
	}

	static void CreateOpenBackConfig (string pathToBuiltProject)
	{
		PlistDocument plist = new PlistDocument ();
		PlistElementDict rootDict = plist.root;

		// Set the app code - REQUIRED
		rootDict.SetString(kOBKConfigAppCode, "YOUR_APP_CODE_HERE"); 
		// Set the debug log level (useful to make sure everything is running fine)
		rootDict.SetInteger(kOBKConfigLogLevel, (int)LogLevel.NONE);
		// Set the background fetch interval - 0 means UIApplicationBackgroundFetchIntervalMinimum
		rootDict.SetInteger(kOBKConfigMinimumBackgroundFetchInterval, 0);
        // Enable Alert Notifications - requires kOBKConfigRequestAlertNotificationsAuthorization (true is the default value)
		rootDict.SetBoolean(kOBKConfigEnableAlertNotifications, true);
		// Enable Notifications and In App Messages when in the foreground (true is the default value)
        rootDict.SetBoolean(kOBKConfigEnableInAppNotifications, true);
		// Enable remote push notifications (true is the default value)
    	rootDict.SetBoolean(kOBKConfigEnableRemoteNotifications, true);
		// Disable use of proximity sensor (false is the default value)
		rootDict.SetBoolean(kOBKConfigEnableProximity, false);
		// Disable use of motion coprocessor - requires kOBKConfigRequestMotionCoprocessorAuthorization (false is the default value)
		rootDict.SetBoolean(kOBKConfigEnableMotionCoprocessor, false);
		// Disable use of microphone - requires kOBKConfigRequestMicrophoneAuthorization (false is the default value)
        rootDict.SetBoolean(kOBKConfigEnableMicrophone, false);
		// Disable use of location always - requires kOBKConfigRequestLocationAlwaysAuthorization (false is the default value)
    	rootDict.SetBoolean(kOBKConfigEnableLocationAlways, false);
		// Disable use of location when in use - requires kOBKConfigRequestLocationWhenInUseAuthorization (false is the default value)
    	rootDict.SetBoolean(kOBKConfigEnableLocationWhenInUse, false);
		// Request permissions for alert notifications if kOBKConfigEnableAlertNotifications is enabled
		rootDict.SetBoolean(kOBKConfigRequestAlertNotificationsAuthorization, true);
		// Request permissions for motion coprocessor if kOBKConfigEnableMotionCoprocessor is enabled
		rootDict.SetBoolean(kOBKConfigRequestMotionCoprocessorAuthorization, true);
		// Request permissions for microphone if kOBKConfigEnableMicrophone is enabled
		rootDict.SetBoolean(kOBKConfigRequestMicrophoneAuthorization, true);
		// Request permissions for location always if kOBKConfigEnableLocationAlways is enabled
		rootDict.SetBoolean(kOBKConfigRequestLocationAlwaysAuthorization, true);
		// Request permissions for location when in use if kOBKConfigEnableLocationWhenInUse is enabled
		rootDict.SetBoolean(kOBKConfigRequestLocationWhenInUseAuthorization, true);
    
		// Write back to file
		string plistPath = pathToBuiltProject + "/OpenBackConfig.plist";		
		File.WriteAllText (plistPath, plist.WriteToString ());

		// Get project
		string projectPath = pathToBuiltProject + "/Unity-iPhone.xcodeproj/project.pbxproj";
		PBXProject project = new PBXProject ();
		project.ReadFromFile (projectPath);
		// Xcode target in the generated project
		string target = project.TargetGuidByName ("Unity-iPhone");
		// Copy plist from the project folder to the build folder
		project.AddFileToBuild (target, project.AddFile("OpenBackConfig.plist", "OpenBackConfig.plist"));

		// Write PBXProject object back to the file
		project.WriteToFile (projectPath);
	}
		
	[PostProcessBuildAttribute(1)]
	public static void OnPostprocessBuild(BuildTarget buildTarget, string pathToBuiltProject) 
	{
		if ( buildTarget == BuildTarget.iOS )
		{
			UpdateInfoPList (pathToBuiltProject);
			CreateOpenBackConfig (pathToBuiltProject);
		}
	}
}
