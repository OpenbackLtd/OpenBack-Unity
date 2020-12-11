using UnityEngine;
using System.Collections.Generic;

namespace OpenBackUnity
{
	public enum OpenBackSegment { 
		CustomSegment1 = 0, CustomSegment2 = 1, CustomSegment3 = 2, CustomSegment4 = 3, CustomSegment5 = 4, 
		CustomSegment6 = 5, CustomSegment7 = 6, CustomSegment8 = 7, CustomSegment9 = 8, CustomSegment10 = 9
	};

	public enum OpenBackLogLevel { 
		None = 0, Error = 1, Warning = 2, Info = 3, Debug = 4, Verbose = 5
	};
		
	public class OpenBack
	{
		private IOpenBackPlugin plugin = null;

		private static OpenBack sharedInstance = new OpenBack();

		public static OpenBack SharedInstance {
			get {
				return sharedInstance;
			}
		}

		private OpenBack() {
			if (!Application.isEditor) {
				#if UNITY_ANDROID
				plugin = new OpenBackAndroid ();
				#elif UNITY_IOS
				plugin = new OpenBackIOS ();
				#endif
			}
		}

		// Configuration

		public string AppCode {
			get {
				return plugin.AppCode;
			}
			set {
				plugin.AppCode = value;
			}
		}

		public bool AutoStart {
			get {
				return plugin.AutoStart;
			}
			set {
				plugin.AutoStart = value;
			}
		}

		public bool GdprForgetUser {
			get {
				return plugin.GdprForgetUser;
			}
			set {
				plugin.GdprForgetUser = value;
			}
		}

		public bool CoppaCompliant {
			get {
				return plugin.CoppaCompliant;
			}
			set {
				plugin.CoppaCompliant = value;
			}
		}

		public bool HipaaCompliant {
			get {
				return plugin.HipaaCompliant;
			}
			set {
				plugin.HipaaCompliant = value;
			}
		}

		public OpenBackLogLevel DebugLogLevel {
			get {
				return plugin.DebugLogLevel;
			}
			set {
				plugin.DebugLogLevel = value;
			}
		}

		public string SdkVersion {
			get {
				return plugin.SdkVersion;				
			}			
		}

		// Runtime

		public bool Start() {
			return plugin.Start();
		}

		public void Stop() {
			plugin.Stop();
		}

		public bool Started {
			get {
				return plugin.Started;
			}
		}

		public void ResetAll() {
			plugin.ResetAll();
		}

		// Custom Segments

		public void SetCustomSegment(OpenBackSegment segment, long value) {
			plugin.SetCustomSegment(segment, value);
		}

		public void SetCustomSegment(OpenBackSegment segment, double value) {
			plugin.SetCustomSegment(segment, value);
		}

		public void SetCustomSegment(OpenBackSegment segment, string value) {
			plugin.SetCustomSegment(segment, value);
		}

		public long GetCustomSegmentAsLong(OpenBackSegment segment) {
			return plugin.GetCustomSegmentAsLong(segment);
		}

		public double GetCustomSegmentAsDouble(OpenBackSegment segment) {
			return plugin.GetCustomSegmentAsDouble(segment);
		}

		public string GetCustomSegmentAsString(OpenBackSegment segment) {
			return plugin.GetCustomSegmentAsString(segment);
		}
		
		public void RemoveAllCustomSegments() {
			plugin.RemoveAllCustomSegments();
		}

		// Attributes

		public void SetAttribute(string attributeKey, long attributeValue) {
			plugin.SetAttribute(attributeKey, attributeValue);
		}

		public void SetAttribute(string attributeKey, double attributeValue) {
			plugin.SetAttribute(attributeKey, attributeValue);
		}

		public void SetAttribute(string attributeKey, string attributeValue) {
			plugin.SetAttribute(attributeKey, attributeValue);
		}

		public long GetAttributeAsLong(string attributeKey) {
			return plugin.GetAttributeAsLong(attributeKey);
		}

		public double GetAttributeAsDouble(string attributeKey) {
			return plugin.GetAttributeAsDouble(attributeKey);
		}

		public string GetAttributeAsString(string attributeKey) {
			return plugin.GetAttributeAsString(attributeKey);
		}

		public void RemoveAllAttributes() {
			plugin.RemoveAllAttributes();
		}

		// Log Goals
		
		public void LogGoal(string goal, int step, double value, string currency = null) {
			plugin.LogGoal(goal, step, value, currency);
		}

		// Event Signal

		public void SignalEvent(string theEvent, long delay) {
			plugin.SignalEvent(theEvent, delay);
		}

		public void CancelEvent(string theEvent) {
			plugin.CancelEvent(theEvent);
		}

		// Developer Tools

		public void CheckMessagesNow() {
			plugin.CheckMessagesNow();
		}

		public void ReloadMessagesNow() {
			plugin.ReloadMessagesNow();
		}


		// List of Pre-Defined attribute keys
		public const string UserAddressLine1 = "addressLine1";
		public const string UserAddressLine2 = "addressLine2";
		public const string UserAdvertisingId = "advertisingId";
		public const string UserAge = "age";
		public const string UserCity = "city";
		public const string UserCountry = "country";
		public const string UserCountryCode = "countryCode";
		public const string UserDateOfBirth = "dateOfBirth";
		public const string UserEmailAddress = "emailAddress";
		public const string UserFirstName = "firstName";
		public const string UserGender = "gender";
		public const string UserOptInUpdates = "optInUpdates";
		public const string UserPhoneNumber = "phoneNumber";
		public const string UserPostCode = "postCode";
		public const string UserProfession = "profession";
		public const string UserState = "state";
		public const string UserSurname = "surname";
		public const string UserTitle = "title";
		public const string UserIdentity1 = "identity1";
		public const string UserIdentity2 = "identity2";
		public const string UserIdentity3 = "identity3";
		public const string UserIdentity4 = "identity4";
		public const string UserIdentity5 = "identity5";
	}
}