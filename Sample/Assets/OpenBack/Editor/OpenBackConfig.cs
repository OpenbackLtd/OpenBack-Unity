#if UNITY_EDITOR
using System;
using System.IO;
using System.Xml;
using UnityEngine;

#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif

namespace OpenBack.Editor
{
    [Serializable]    
    public class OpenBackConfig
    {
        public enum LogLevel
        {
            None = 0,
            Error = 1,
            Warning = 2,
            Info = 3,
            Debug = 4,
            Verbose = 5
        }

        public string IOSAppCode;
        public string IOSAppGroup;
        public LogLevel IOSLogLevel;
        public bool IOSEnableAutoStart;
        public bool IOSClearBadgeCount;
        public bool IOSEnableCoreMotion;
        public bool IOSEnableMicrophone;
        public bool IOSEnableProximity;
        public bool IOSEnableSwizzle;
        public string IOSNotificationLargeIcon;
        public string IOSNotificationSound;

        public string AndroidAppCode;
        public LogLevel AndroidLogLevel;
        public bool AndroidEnableAutoStart;
        public string AndroidNotificationIcon;
        public string AndroidNotificationAccent;
        public string AndroidNotificationLargeIcon;
        public string AndroidNotificationSound;
        public string AndroidNotificationChannel;
        public string AndroidNotificationChannelDescription;

        public OpenBackConfig() {
            // Default iOS values
            IOSLogLevel = LogLevel.Error;
            IOSEnableAutoStart = true;
            IOSEnableSwizzle = true;
            // Default Android values
            AndroidLogLevel = LogLevel.Error;
            AndroidEnableAutoStart = true;
        }

        // Save the settings in the current project
        private static readonly string configFile = "ProjectSettings/OpenBackConfig.json";

        public static OpenBackConfig Load() {
            OpenBackConfig config = null;
            if (File.Exists(configFile)) {
                try {
                    StreamReader reader = new StreamReader(configFile);
                    var json = reader.ReadToEnd();
                    config = JsonUtility.FromJson<OpenBackConfig>(json);
                    reader.Close();
                } catch (Exception e) { 
                    Debug.Log("Unable to load OpenBack config: " + e);
                }
            }
            return config ?? new OpenBackConfig();
        }

        public void Save() {
            try {
                StreamWriter writer = new StreamWriter(configFile);
                var json = JsonUtility.ToJson(this);
                writer.Write(json);
                writer.Close();
            } catch (Exception e) { 
                Debug.Log("Unable to save OpenBack config: " + e);
            }
        }

        public void Apply() {
            // iOS settings are setup post build
            #if UNITY_ANDROID
            UpdateAndroidManifest();
            #endif
        }

        private void UpdateAndroidManifest() {
            string manifestPath = "Assets/Plugins/Android/OpenBackUnity/AndroidManifest.xml";
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            using (XmlWriter xmlWriter = XmlWriter.Create(manifestPath, settings)) {
                xmlWriter.WriteStartDocument ();

                // Manifest
                xmlWriter.WriteStartElement("manifest");
                xmlWriter.WriteAttributeString("xmlns", "android", null, "http://schemas.android.com/apk/res/android");
                xmlWriter.WriteAttributeString("package", "com.openback.unity");

                // Application
                xmlWriter.WriteStartElement("application");

                // App Code
                if (!String.IsNullOrEmpty(AndroidAppCode)) {
                    xmlWriter.WriteStartElement("meta-data");
                    xmlWriter.WriteAttributeString("android", "name", null, "com.openback.APP_CODE");
                    xmlWriter.WriteAttributeString("android", "value", null, AndroidAppCode);
                    xmlWriter.WriteEndElement (); // </meta-data>
                }
                // Log Level
                xmlWriter.WriteStartElement("meta-data");
                xmlWriter.WriteAttributeString("android", "name", null, "com.openback.LOG_LEVEL");
                xmlWriter.WriteAttributeString("android", "value", null, (5 - AndroidLogLevel).ToString("D"));
                xmlWriter.WriteEndElement (); // </meta-data>
                // Auto Start
                xmlWriter.WriteStartElement("meta-data");
                xmlWriter.WriteAttributeString("android", "name", null, "com.openback.AUTO_START");
                xmlWriter.WriteAttributeString("android", "value", null, AndroidEnableAutoStart ? "true" : "false");
                xmlWriter.WriteEndElement (); // </meta-data>
                // Notification Icon
                if (!String.IsNullOrEmpty(AndroidNotificationIcon)) {
                    xmlWriter.WriteStartElement("meta-data");
                    xmlWriter.WriteAttributeString("android", "name", null, "com.openback.notification.ICON");
                    xmlWriter.WriteAttributeString("android", "resource", null, AndroidNotificationIcon);
                    xmlWriter.WriteEndElement (); // </meta-data>
                }
                // Notification Accent
                if (!String.IsNullOrEmpty(AndroidNotificationAccent)) {
                    xmlWriter.WriteStartElement("meta-data");
                    xmlWriter.WriteAttributeString("android", "name", null, "com.openback.notification.ACCENT_COLOR");
                    xmlWriter.WriteAttributeString("android", "resource", null, AndroidNotificationAccent);
                    xmlWriter.WriteEndElement (); // </meta-data>
                }
                // Notification Large Icon
                if (!String.IsNullOrEmpty(AndroidNotificationLargeIcon)) {
                    xmlWriter.WriteStartElement("meta-data");
                    xmlWriter.WriteAttributeString("android", "name", null, "com.openback.notification.LARGE_ICON");
                    xmlWriter.WriteAttributeString("android", "resource", null, AndroidNotificationLargeIcon);
                    xmlWriter.WriteEndElement (); // </meta-data>
                }
                // Notification Sound
                if (!String.IsNullOrEmpty(AndroidNotificationSound)) {
                    xmlWriter.WriteStartElement("meta-data");
                    xmlWriter.WriteAttributeString("android", "name", null, "com.openback.notification.SOUND");
                    xmlWriter.WriteAttributeString("android", "value", null, AndroidNotificationSound);
                    xmlWriter.WriteEndElement (); // </meta-data>
                }
                // Notification Channel
                if (!String.IsNullOrEmpty(AndroidNotificationChannel)) {
                    xmlWriter.WriteStartElement("meta-data");
                    xmlWriter.WriteAttributeString("android", "name", null, "com.openback.notification.CHANNEL_NAME");
                    xmlWriter.WriteAttributeString("android", "value", null, AndroidNotificationChannel);
                    xmlWriter.WriteEndElement (); // </meta-data>
                }
                // Notification Channel Description
                if (!String.IsNullOrEmpty(AndroidNotificationChannelDescription)) {
                    xmlWriter.WriteStartElement("meta-data");
                    xmlWriter.WriteAttributeString("android", "name", null, "com.openback.notification.CHANNEL_DESCRIPTION");
                    xmlWriter.WriteAttributeString("android", "value", null, AndroidNotificationChannelDescription);
                    xmlWriter.WriteEndElement (); // </meta-data>
                }

                xmlWriter.WriteEndElement (); // </application>
                xmlWriter.WriteEndElement (); // </manifest>
                xmlWriter.WriteEndDocument ();                
            }
        }
    }
}
#endif