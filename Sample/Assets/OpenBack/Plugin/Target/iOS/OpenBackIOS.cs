#if UNITY_IOS

using System.Runtime.InteropServices;

namespace OpenBackUnity {

	// OpenBack Unity Interface 
	public class OpenBackIOS : IOpenBackPlugin {

		// Config

		[DllImport("__Internal")]
		private static extern string OpenBack_AppCode();
		[DllImport("__Internal")]
		private static extern void OpenBack_SetAppCode(string appCode);

		public string AppCode {
			get { return OpenBack_AppCode(); }
			set { OpenBack_SetAppCode(value); }
		}

		[DllImport("__Internal")]
		private static extern bool OpenBack_AutoStart();
		[DllImport("__Internal")]
		private static extern void OpenBack_SetAutoStart(bool autoStart);

		public bool AutoStart {
			get { return OpenBack_AutoStart(); }
			set { OpenBack_SetAutoStart(value); }
		}

		[DllImport("__Internal")]
		private static extern bool OpenBack_GdprForgetUser();
		[DllImport("__Internal")]
		private static extern void OpenBack_SetGdprForgetUser(bool gdprForgetUser);

		public bool GdprForgetUser {
			get { return OpenBack_GdprForgetUser(); }
			set { OpenBack_SetGdprForgetUser(value); }
		}

		[DllImport("__Internal")]
		private static extern bool OpenBack_CoppaCompliant();
		[DllImport("__Internal")]
		private static extern void OpenBack_SetCoppaCompliant(bool coppaCompliant);

		public bool CoppaCompliant 		{
			get { return OpenBack_CoppaCompliant(); }
			set { OpenBack_SetCoppaCompliant(value); }
		}

		[DllImport("__Internal")]
		private static extern bool OpenBack_HipaaCompliant();
		[DllImport("__Internal")]
		private static extern void OpenBack_SetHipaaCompliant(bool hipaaCompliant);

		public bool HipaaCompliant {
			get { return OpenBack_HipaaCompliant(); }
			set { OpenBack_SetHipaaCompliant(value); }
		}

		[DllImport("__Internal")]
		private static extern OpenBackLogLevel OpenBack_DebugLogLevel();
		[DllImport("__Internal")]
		private static extern void OpenBack_SetDebugLogLevel(OpenBackLogLevel debugLogLevel);

		public OpenBackLogLevel DebugLogLevel {
			get { return OpenBack_DebugLogLevel(); }
			set { OpenBack_SetDebugLogLevel(value); }
		}

		[DllImport ("__Internal")]
		private static extern string OpenBack_SdkVersion ();

		public string SdkVersion {
			get { return OpenBack_SdkVersion(); }
		}

		// Runtime

		[DllImport("__Internal")]
		private static extern bool OpenBack_Start();

		public bool Start() {
			return OpenBack_Start();
		}

		[DllImport("__Internal")]
		private static extern void OpenBack_Stop();

		public void Stop() {
			OpenBack_Stop();
		}

		[DllImport("__Internal")]
		private static extern bool OpenBack_Started();

		public bool Started {
			get { return OpenBack_Started(); }
		}

		[DllImport("__Internal")]
		private static extern void OpenBack_ResetAll();

		public void ResetAll() {
			OpenBack_ResetAll();
		}
		

		// Custom Segments

		[DllImport("__Internal")]
        private static extern void OpenBack_SetCustomSegment_Long(OpenBackSegment segment, long value);
		
		public void SetCustomSegment(OpenBackSegment segment, long value) {
			OpenBack_SetCustomSegment_Long(segment, value);
        }

		[DllImport("__Internal")]
        private static extern void OpenBack_SetCustomSegment_Double(OpenBackSegment segment, double value);
		
		public void SetCustomSegment(OpenBackSegment segment, double value) {
			OpenBack_SetCustomSegment_Double(segment, value);
        }

		[DllImport("__Internal")]
        private static extern void OpenBack_SetCustomSegment_String(OpenBackSegment segment, string value);

		public void SetCustomSegment(OpenBackSegment segment, string value) {
			OpenBack_SetCustomSegment_String(segment, value);
        }

		[DllImport("__Internal")]
		private static extern long OpenBack_GetCustomSegment_Long(OpenBackSegment segment);
		
        public long GetCustomSegmentAsLong(OpenBackSegment segment) {
            return OpenBack_GetCustomSegment_Long(segment);
        }

		[DllImport("__Internal")]
		private static extern double OpenBack_GetCustomSegment_Double(OpenBackSegment segment);
		
		public double GetCustomSegmentAsDouble(OpenBackSegment segment) {
            return OpenBack_GetCustomSegment_Double(segment);
        }

		[DllImport("__Internal")]
		private static extern string OpenBack_GetCustomSegment_String(OpenBackSegment segment);

		public string GetCustomSegmentAsString(OpenBackSegment segment) {
            return OpenBack_GetCustomSegment_String(segment);
        }

        [DllImport("__Internal")]
		private static extern void OpenBack_RemoveAllCustomSegments();

		public void RemoveAllCustomSegments() {
            OpenBack_RemoveAllCustomSegments();
        }

        // Attributes

		[DllImport("__Internal")]
		private static extern void OpenBack_SetAttribute_Long(string attributeKey, long attributeValue);

		public void SetAttribute(string attributeKey, long attributeValue) {
            OpenBack_SetAttribute_Long(attributeKey, attributeValue);
        }

		[DllImport("__Internal")]
		private static extern void OpenBack_SetAttribute_Double(string attributeKey, double attributeValue);

		public void SetAttribute(string attributeKey, double attributeValue) {
            OpenBack_SetAttribute_Double(attributeKey, attributeValue);
        }

		[DllImport("__Internal")]
		private static extern void OpenBack_SetAttribute_String(string attributeKey, string attributeValue);

		public void SetAttribute(string attributeKey, string attributeValue) {
            OpenBack_SetAttribute_String(attributeKey, attributeValue);
        }

		[DllImport("__Internal")]
		private static extern long OpenBack_GetAttribute_Long(string attributeKey);

		public long GetAttributeAsLong(string attributeKey) {
            return OpenBack_GetAttribute_Long(attributeKey);
        }

		[DllImport("__Internal")]
		private static extern double OpenBack_GetAttribute_Double(string attributeKey);

		public double GetAttributeAsDouble(string attributeKey) {
            return OpenBack_GetAttribute_Double(attributeKey);
        }

		[DllImport("__Internal")]
		private static extern string OpenBack_GetAttribute_String(string attributeKey);

		public string GetAttributeAsString(string attributeKey) {
            return OpenBack_GetAttribute_String(attributeKey);
        }

		[DllImport("__Internal")]
		private static extern void OpenBack_RemoveAllAttributes();

		public void RemoveAllAttributes() {
            OpenBack_RemoveAllAttributes();
        }

        // Goals

		[DllImport("__Internal")]
		private static extern void OpenBack_LogGoal(string goal, int step, double value, string currency);

		public void LogGoal(string goal, int step, double value, string currency = null) {
            OpenBack_LogGoal(goal, step, value, currency);
        }

        // Event Signal

		[DllImport("__Internal")]
		private static extern void OpenBack_SignalEvent(string theEvent, long delay);

		public void SignalEvent(string theEvent, long delay) {
            OpenBack_SignalEvent(theEvent, delay);
        }
        
		[DllImport("__Internal")]
		private static extern void OpenBack_CancelEvent(string theEvent);

		public void CancelEvent(string theEvent) {
            OpenBack_CancelEvent(theEvent);
        }

        // Developer Tools

		[DllImport("__Internal")]
		private static extern void OpenBack_CheckMessagesNow();

		public void CheckMessagesNow() {
            OpenBack_CheckMessagesNow();
        }
        
		[DllImport("__Internal")]
		private static extern void OpenBack_ReloadMessagesNow();

		public void ReloadMessagesNow() {
            OpenBack_ReloadMessagesNow();
        }
	}

	public class OpenBackIOSHelper {

		[DllImport("__Internal")]
		private static extern void OpenBack_RequestNotificationAuthorization();

		static public void RequestNotificationAuthorization() {
			OpenBack_RequestNotificationAuthorization();
        }
    }
}

#endif