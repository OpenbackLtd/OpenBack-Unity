using System;
using UnityEditor;
using UnityEngine;

namespace OpenBack.Editor
{
    [Serializable]    
    public class OBKConfig
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

        [SerializeField]
        public string IOSAppCode { get; set; }
        [SerializeField]
        public string IOSAppGroup { get; set; }
        [SerializeField]
        public LogLevel IOSLogLevel { get; set; }
        [SerializeField]
        public bool IOSEnableAutoStart { get; set; }
        [SerializeField]
        public bool IOSClearBadgeCount { get; set; }
        [SerializeField]
        public bool IOSEnableCoreMotion { get; set; }
        [SerializeField]
        public bool IOSEnableMicrophone { get; set; }
        [SerializeField]
        public bool IOSEnableProximity { get; set; }
        [SerializeField]
        public bool IOSEnableSwizzle { get; set; }
        [SerializeField]
        public string IOSNotificationLargeIcon { get; set; }
        [SerializeField]
        public string IOSNotificationSound { get; set; }

        [SerializeField]
        public string AndroidAppCode { get; set; }
        [SerializeField]
        public LogLevel AndroidLogLevel { get; set; }
        [SerializeField]
        public bool AndroidEnableAutoStart { get; set; }
        [SerializeField]
        public string AndroidNotificationIcon { get; set; }
        [SerializeField]
        public string AndroidNotificationAccent { get; set; }
        [SerializeField]
        public string AndroidNotificationLargeIcon { get; set; }
        [SerializeField]
        public string AndroidNotificationSound { get; set; }
        [SerializeField]
        public string AndroidNotificationChannel { get; set; }
        [SerializeField]
        public string AndroidNotificationChannelDescription { get; set; }

        public OBKConfig()
        {

        }
    }
}
