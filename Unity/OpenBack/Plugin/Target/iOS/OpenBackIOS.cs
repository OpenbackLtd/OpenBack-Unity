#if UNITY_IOS

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace OpenBackUnity {

	// OpenBack Unity Interface 
	public class OpenBackIOS : IOpenBackPlugin {

		[DllImport ("__Internal")]
		private static extern string _getSdkVersion ();

		public string getSdkVersion () {
			return _getSdkVersion ();
		}

		[DllImport ("__Internal")]
		private static extern bool _setUserInfo (string jsonInfo);

		public bool setUserInfo (OpenBackUserInfo userInfo) {
			Dictionary<string, string> info = new Dictionary<string, string> ();
			info.Add ("addressLine1", userInfo.AddressLine1 ?? "");
			info.Add ("addressLine2", userInfo.AddressLine2 ?? "");
			info.Add ("advertisingId", userInfo.AdvertisingId ?? "");
			info.Add ("age", userInfo.Age ?? "");
			info.Add ("city", userInfo.City ?? "");
			info.Add ("country", userInfo.Country ?? "");
			info.Add ("countryCode", userInfo.CountryCode ?? "");
			info.Add ("dateOfBirth", userInfo.DateOfBirth ?? "");
			info.Add ("emailAddress", userInfo.Email ?? "");				
			info.Add ("firstName", userInfo.FirstName ?? "");
			info.Add ("gender", userInfo.Gender ?? "");
			info.Add ("phoneNumber", userInfo.PhoneNumber ?? "");
			info.Add ("postCode", userInfo.PostCode ?? "");
			info.Add ("profession", userInfo.Profession ?? "");
			info.Add ("state", userInfo.State ?? "");
			info.Add ("surname", userInfo.Surname ?? "");
			info.Add ("title", userInfo.Title ?? "");
			info.Add ("identity1", userInfo.Identity1 ?? "");
			info.Add ("identity2", userInfo.Identity2 ?? "");
			info.Add ("identity3", userInfo.Identity3 ?? "");
			info.Add ("identity4", userInfo.Identity4 ?? "");
			info.Add ("identity5", userInfo.Identity5 ?? "");

			string infoString = JsonUtility.ToJson (info);
			return _setUserInfo (infoString);
		}

		[DllImport ("__Internal")]
		private static extern bool _setStringCustomTrigger(int trigger, string value);

		public bool setCustomTrigger (OpenBackTrigger trigger, string value) {
			return _setStringCustomTrigger ((int)trigger, value);
		}
			
		[DllImport ("__Internal")]
		private static extern bool _setIntCustomTrigger(int trigger, int value);

		public bool setCustomTrigger (OpenBackTrigger trigger, int value) {
			return _setIntCustomTrigger ((int)trigger, value);
		}

		[DllImport ("__Internal")]
		private static extern bool _setLongCustomTrigger(int trigger, long value);

		public bool setCustomTrigger (OpenBackTrigger trigger, long value) {
			return _setLongCustomTrigger ((int)trigger, value);
		}

		[DllImport ("__Internal")]
		private static extern bool _setFloatCustomTrigger(int trigger, float value);

		public bool setCustomTrigger (OpenBackTrigger trigger, float value) {
			return _setFloatCustomTrigger ((int)trigger, value);
		}

		[DllImport ("__Internal")]
		private static extern bool _setDoubleCustomTrigger(int trigger, double value);

		public bool setCustomTrigger (OpenBackTrigger trigger, double value) {
			return _setDoubleCustomTrigger ((int)trigger, value);
		}

		[DllImport ("__Internal")]
		private static extern bool _gdprForgetUser (bool forgetUser);

		public bool gdprForgetUser (bool forgetUser) {
			return _gdprForgetUser (forgetUser);
		}

		[DllImport ("__Internal")]
		private static extern void _coppaCompliant (bool compliant);

		public void coppaCompliant (bool compliant) {
			_coppaCompliant (compliant);
		}

		[DllImport ("__Internal")]
		private static extern bool _logGoal (string goal, int step, double value);

		public bool logGoal (string goal, int step, double value) {
			return _logGoal (goal, step, value);
		}
	}
}

#endif