using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections;
using UnityEditor.iOS.Xcode;
using System.IO;

public class OpenBackPluginPostProcessBuild
{

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

		// LSApplicationQueriesSchemes - Optional
		//			PlistElementArray queriesSchemes = rootDict.CreateArray("LSApplicationQueriesSchemes");
		//			queriesSchemes.AddString ("instagram");

		// NSAppTransportSecurity - Optional
		//			PlistElementDict ats = rootDict.CreateArray("NSAppTransportSecurity");
		//			ats.SetBoolean ("NSAllowsArbitraryLoads", true);

		// Write back to file
		File.WriteAllText (plistPath, plist.WriteToString ());
	}
		
	[PostProcessBuildAttribute(1)]
	public static void OnPostprocessBuild(BuildTarget buildTarget, string pathToBuiltProject) 
	{
		if ( buildTarget == BuildTarget.iOS )
		{
			UpdateInfoPList (pathToBuiltProject);
			// TODO: Add OpenBack.framework to Embedded Binaries automatically
			// TODO: Add strip-frameworks.sh run script automatically
		}
	}
}
