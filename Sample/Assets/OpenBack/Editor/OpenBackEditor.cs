using UnityEngine;
using UnityEditor;
using System;

namespace OpenBack.Editor
{
    public class OBKConfigEditor : EditorWindow
    {
        private OBKConfig config;

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
            config = new OBKConfig();
        }

        void OnGUI()
        {
            GUILayout.BeginVertical();
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            // IOS Section
            GUILayout.Label("iOS Settings", EditorStyles.boldLabel);            
            EditorGUI.indentLevel++;
            config.IOSAppCode = EditorGUILayout.TextField(new GUIContent("App Code", "Enter the OpenBack app code"), config.IOSAppCode);
            config.IOSAppGroup = EditorGUILayout.TextField("App Group", config.IOSAppGroup);
            config.IOSLogLevel = (OBKConfig.LogLevel)EditorGUILayout.EnumPopup("Log Level", config.IOSLogLevel);
            // Notification
            showIOSNotification = EditorGUILayout.Foldout(showIOSNotification, "Notification");
            if (showIOSNotification)
            {
                EditorGUI.indentLevel++;
                config.IOSClearBadgeCount = EditorGUILayout.Toggle("Clear Badge Count", config.IOSClearBadgeCount);
                config.IOSNotificationLargeIcon = EditorGUILayout.TextField("Large Icon", config.IOSNotificationLargeIcon);
                config.IOSNotificationSound = EditorGUILayout.TextField("Sound", config.IOSNotificationSound);
                EditorGUI.indentLevel--;
            }
            // Advanced
            showIOSAdvanced = EditorGUILayout.Foldout(showIOSAdvanced, "Advanced");
            if (showIOSAdvanced)
            {
                EditorGUI.indentLevel++;
                config.IOSEnableAutoStart = EditorGUILayout.Toggle("Enable Auto-Start", config.IOSEnableAutoStart);
                config.IOSEnableCoreMotion = EditorGUILayout.Toggle("Enable Core Motion", config.IOSEnableCoreMotion);
                config.IOSEnableMicrophone = EditorGUILayout.Toggle("Enable Microphone", config.IOSEnableMicrophone);
                config.IOSEnableProximity = EditorGUILayout.Toggle("Enable Proximity", config.IOSEnableProximity);
                config.IOSEnableSwizzle = EditorGUILayout.Toggle("Enable Swizzle", config.IOSEnableSwizzle);
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
            GUILayout.Space(20);

            // Android Section
            GUILayout.Label("Android Settings", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            config.AndroidAppCode = EditorGUILayout.TextField(new GUIContent("App Code", "Enter the OpenBack app code"), config.AndroidAppCode);
            config.AndroidLogLevel = (OBKConfig.LogLevel)EditorGUILayout.EnumPopup("Log Level", config.AndroidLogLevel);
            // Notification
            showAndroidNotification = EditorGUILayout.Foldout(showAndroidNotification, "Notification");
            if (showAndroidNotification)
            {
                EditorGUI.indentLevel++;
                config.AndroidNotificationIcon = EditorGUILayout.TextField("Icon", config.AndroidNotificationIcon);
                config.AndroidNotificationAccent = EditorGUILayout.TextField("Color Accent", config.AndroidNotificationAccent);
                config.AndroidNotificationLargeIcon = EditorGUILayout.TextField("Large Icon", config.AndroidNotificationLargeIcon);
                config.AndroidNotificationSound = EditorGUILayout.TextField("Sound", config.AndroidNotificationSound);
                config.AndroidNotificationChannel = EditorGUILayout.TextField("Channel", config.AndroidNotificationChannel);
                config.AndroidNotificationChannelDescription = EditorGUILayout.TextField("Channel Description", config.AndroidNotificationChannelDescription);
                EditorGUI.indentLevel--;
            }
            // Advanced
            showAndroidAdvanced = EditorGUILayout.Foldout(showAndroidAdvanced, "Advanced");
            if (showAndroidAdvanced)
            {
                EditorGUI.indentLevel++;
                config.AndroidEnableAutoStart = EditorGUILayout.Toggle("Enable Auto-Start", config.AndroidEnableAutoStart);
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;

            EditorGUILayout.EndScrollView();
            GUILayout.EndVertical();
        }

        private void CreateSection(string sectionTitle, Action body)
        {
            GUILayout.Label(sectionTitle, EditorStyles.boldLabel);
            GUILayout.BeginVertical();
            EditorGUI.indentLevel++;
            body();
            EditorGUI.indentLevel--;
            GUILayout.EndVertical();
            GUILayout.Space(20);
        }
    }
}