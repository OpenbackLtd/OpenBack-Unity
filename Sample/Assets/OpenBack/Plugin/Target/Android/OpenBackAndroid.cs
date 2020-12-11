#if UNITY_ANDROID

using System;
using UnityEngine;

namespace OpenBackUnity {

	class OpenBackAndroid : IOpenBackPlugin {
        private AndroidJavaClass openBack;

        public OpenBackAndroid () {
			AndroidJNI.AttachCurrentThread();  

			using (AndroidJavaClass unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			using (AndroidJavaObject activity = unityClass.GetStatic<AndroidJavaObject>("currentActivity"))
			using (AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext")) {
				openBack = new AndroidJavaClass("com.openback.OpenBack");
				openBack.CallStatic("initialize", context);                
			}

			AndroidJNI.DetachCurrentThread();
		}
		
		// Configuration

		public string AppCode {
			get { return openBack.CallStatic<string>("getAppCode"); }
			set { openBack.CallStatic("setAppCode", value); }
        }

		public bool AutoStart {
			get { return openBack.CallStatic<bool>("isAutoStartEnabled"); }
			set { openBack.CallStatic("setAutoStart", value); }
        }

		public bool GdprForgetUser {
			get { return openBack.CallStatic<bool>("isGDPRForgetUserEnabled"); }
			set { openBack.CallStatic("setGDPRForgetUser", value); }
        }

		public bool CoppaCompliant {
			get { return openBack.CallStatic<bool>("isCOPPACompliant"); }
			set { openBack.CallStatic("setCOPPACompliant", value); }
        }

		public bool HipaaCompliant {
			get { return openBack.CallStatic<bool>("isHIPAACompliant"); }
			set { openBack.CallStatic("setHIPAACompliant", value); }
        }

		public OpenBackLogLevel DebugLogLevel {
			get {
				return (OpenBackLogLevel)(OpenBackLogLevel.Verbose - openBack.CallStatic<OpenBackLogLevel>("getDebugLogLevel"));
			}
			set { openBack.CallStatic("setDebugLogLevel", OpenBackLogLevel.Verbose - value); }
        }

		public string SdkVersion {
			get { return openBack.GetStatic<string> ("SDK_VERSION"); }
		}

		// Runtime

		public bool Start() {
			return openBack.CallStatic<bool>("start");
		}

		public void Stop() {
			openBack.CallStatic<bool>("stop");
		}

		public bool Started {
			get { return openBack.CallStatic<bool>("isStarted"); }
		}

		public void ResetAll() {
			openBack.CallStatic("resetAll");
		}

		// Custom Segments

		public void SetCustomSegment(OpenBackSegment segment, long value) {
            using (AndroidJavaObject param = new AndroidJavaObject("java.lang.Long", value)) {
				openBack.CallStatic<bool>("setCustomSegment", (int)segment + 1, param);
			}
        }

		public void SetCustomSegment(OpenBackSegment segment, double value) {
			using (AndroidJavaObject param = new AndroidJavaObject("java.lang.Double", value)) {
				openBack.CallStatic<bool>("setCustomSegment", (int)segment + 1, param);
			}
        }

		public void SetCustomSegment(OpenBackSegment segment, string value) {
			openBack.CallStatic<bool>("setCustomSegment", (int)segment + 1, value);
        }

		public long GetCustomSegmentAsLong(OpenBackSegment segment) {
            return openBack.CallStatic<long>("getCustomSegmentAsLong", (int)segment + 1);
        }

		public double GetCustomSegmentAsDouble(OpenBackSegment segment) {
            return openBack.CallStatic<double>("getCustomSegmentAsDouble", (int)segment + 1);
        }

		public string GetCustomSegmentAsString(OpenBackSegment segment) {
            return openBack.CallStatic<string>("getCustomSegmentAsString", (int)segment + 1);
        }

        public void RemoveAllCustomSegments() {
            openBack.CallStatic("removeAllCustomSegments");
        }

		// Attributes

		public void SetAttribute(string attributeKey, long attributeValue) {
			using (AndroidJavaObject param = new AndroidJavaObject("java.lang.Long", attributeValue)) {
				openBack.CallStatic("setAttribute", attributeKey, param);
			}
        }

		public void SetAttribute(string attributeKey, double attributeValue) {
			using (AndroidJavaObject param = new AndroidJavaObject("java.lang.Double", attributeValue)) {
				openBack.CallStatic("setAttribute", attributeKey, param);
			}
        }

		public void SetAttribute(string attributeKey, string attributeValue) {
            openBack.CallStatic("setAttribute", attributeKey, attributeValue);
        }

		public long GetAttributeAsLong(string attributeKey) {
            return openBack.CallStatic<long>("getAttributeAsLong", attributeKey);
        }

		public double GetAttributeAsDouble(string attributeKey) {
            return openBack.CallStatic<double>("getAttributeAsDouble", attributeKey);
        }

		public string GetAttributeAsString(string attributeKey) {
            return openBack.CallStatic<string>("getAttributeAsString", attributeKey);
        }

		public void RemoveAllAttributes() {
            openBack.CallStatic("removeAllAttributes");
        }

		// Goals

		public void LogGoal(string goal, int step, double value, string currency = null) {
            openBack.CallStatic<bool>("logGoal", goal, step, value, currency);
        }

		// Event Signal

		public void SignalEvent(string theEvent, long delay) {
            openBack.CallStatic("signalEvent", theEvent, delay);
        }
        
		public void CancelEvent(string theEvent) {
            openBack.CallStatic("cancelEvent", theEvent);
        }

		// Developer Tools

		public void CheckMessagesNow() {
            openBack.CallStatic("checkMessagesNow");
        }
        
		public void ReloadMessagesNow() {
            openBack.CallStatic("reloadMessagesNow");
        }
	}
}

#endif