#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace OpenBack.Editor
{
    public class OBKConfigEditor : EditorWindow
    {
        private OpenBackConfig config;

        private Vector2 scrollPos;
        private bool showIOSAdvanced = false;
        private bool showIOSNotification = false;
        private bool showAndroidAdvanced = false;
        private bool showAndroidNotification = true;

        [MenuItem("Window/OpenBack/Settings", false, 1)]
        public static void Settings()
        {
            GetWindow<OBKConfigEditor>("OpenBack");
        }

        [MenuItem("Window/OpenBack/Integration Guide")]
        public static void Integration()
        {
            Application.OpenURL("https://docs.openback.com/plugins/unity");
        }

        [MenuItem("Window/OpenBack/API Documentation")]
        public static void APIDocumentation()
        {
            Application.OpenURL("https://docs.openback.com/plugins/unity#api-documentation");
        }

        void OnEnable()
        {
            config = OpenBackConfig.Load();
            config.Apply();
        }

        void OnGUI()
        {
            GUILayout.BeginVertical();
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            // IOS Section
            GUILayout.Label("iOS Settings", EditorStyles.boldLabel);            
            EditorGUI.indentLevel++;
            config.IOSAppCode = EditorGUILayout.TextField(new GUIContent("App Code", "Enter your iOS OpenBack app code."), config.IOSAppCode);
            config.IOSAppGroup = EditorGUILayout.TextField(new GUIContent("App Group", "The app group used for OpenBack and app extenstions (e.g. group.com.domain.app)."), config.IOSAppGroup);
            config.IOSLogLevel = (OpenBackConfig.LogLevel)EditorGUILayout.EnumPopup(new GUIContent("Log Level", "The debug log level."), config.IOSLogLevel);
            // Notification
            showIOSNotification = EditorGUILayout.Foldout(showIOSNotification, "Notification");
            if (showIOSNotification)
            {
                EditorGUI.indentLevel++;
                config.IOSClearBadgeCount = EditorGUILayout.Toggle(new GUIContent("Clear Badge Count", "Controls if OpenBack clears the badge number when app is opened (only if badge is enabled)."), config.IOSClearBadgeCount);
                config.IOSNotificationLargeIcon = EditorGUILayout.TextField(new GUIContent("Large Icon", "Default large icon for notification (filename in main bundle or image asset name)."), config.IOSNotificationLargeIcon);
                config.IOSNotificationSound = EditorGUILayout.TextField(new GUIContent("Sound", "Set the default notification sound (filename in main bundle)."), config.IOSNotificationSound);
                EditorGUI.indentLevel--;
            }
            // Advanced
            showIOSAdvanced = EditorGUILayout.Foldout(showIOSAdvanced, "Advanced");
            if (showIOSAdvanced)
            {
                EditorGUI.indentLevel++;
                config.IOSEnableAutoStart = EditorGUILayout.Toggle(new GUIContent("Auto-Start", "Controls if OpenBack starts automatically."), config.IOSEnableAutoStart);
                config.IOSEnableCoreMotion = EditorGUILayout.Toggle(new GUIContent("Enable Core Motion", "Controls if library can use Core Motion Activity manager."), config.IOSEnableCoreMotion);
                config.IOSEnableMicrophone = EditorGUILayout.Toggle(new GUIContent("Enable Microphone", "Controls if OpenBack can use the microphone."), config.IOSEnableMicrophone);
                config.IOSEnableProximity = EditorGUILayout.Toggle(new GUIContent("Enable Proximity", "Controls if OpenBack can use the proximity sensor."), config.IOSEnableProximity);
                config.IOSEnableSwizzle = EditorGUILayout.Toggle(new GUIContent("Use Swizzling", "Controls if swizzling is enabled. If you disable swizzling, you must hook some delegate calls to OpenBack manually (see docs)."), config.IOSEnableSwizzle);
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
            GUILayout.Space(20);

            // Android Section
            GUILayout.Label("Android Settings", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            config.AndroidAppCode = EditorGUILayout.TextField(new GUIContent("App Code", "Enter your Android OpenBack app code."), config.AndroidAppCode);
            config.AndroidLogLevel = (OpenBackConfig.LogLevel)EditorGUILayout.EnumPopup(new GUIContent("Log Level", "The debug log level."), config.AndroidLogLevel);
            // Notification
            showAndroidNotification = EditorGUILayout.Foldout(showAndroidNotification, "Notification");
            if (showAndroidNotification)
            {
                EditorGUI.indentLevel++;
                config.AndroidNotificationIcon = EditorGUILayout.TextField(new GUIContent("Icon", "The resource ID for the notification icon (e.g. @drawable/xxx)."), config.AndroidNotificationIcon);
                config.AndroidNotificationAccent = EditorGUILayout.TextField(new GUIContent("Color Accent", "The resource ID for the notification color accent (e.g. @color/xxx)."), config.AndroidNotificationAccent);
                config.AndroidNotificationLargeIcon = EditorGUILayout.TextField(new GUIContent("Large Icon", "The resource ID for the notification large icon (e.g. @drawable/xxx)."), config.AndroidNotificationLargeIcon);
                config.AndroidNotificationSound = EditorGUILayout.TextField(new GUIContent("Sound", "The resource ID for the notification sound in res/raw (e.g. @raw/xxx)."), config.AndroidNotificationSound);
                config.AndroidNotificationChannel = EditorGUILayout.TextField(new GUIContent("Channel", "The string name of the default notification channel (e.g. @string/xxx or direct value)."), config.AndroidNotificationChannel);
                config.AndroidNotificationChannelDescription = EditorGUILayout.TextField(new GUIContent("Channel Description", "The string description of the default notification channel (e.g. @string/xxx or direct value)"), config.AndroidNotificationChannelDescription);
                EditorGUI.indentLevel--;
            }
            // Advanced
            showAndroidAdvanced = EditorGUILayout.Foldout(showAndroidAdvanced, "Advanced");
            if (showAndroidAdvanced)
            {
                EditorGUI.indentLevel++;
                config.AndroidEnableAutoStart = EditorGUILayout.Toggle(new GUIContent("Auto-Start", "Controls if OpenBack starts automatically."), config.AndroidEnableAutoStart);
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
            GUILayout.Space(20);

            // Save Button
            if (GUILayout.Button("Save")) {
                config.Save();
                config.Apply();                
            }

            EditorGUILayout.EndScrollView();
            GUILayout.EndVertical();
        }        
    }
}
#endif