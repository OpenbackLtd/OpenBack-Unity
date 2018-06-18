using System;
using UnityEngine;

namespace OpenBackUnity
{
	public enum OpenBackTrigger { 
		CustomTrigger1, CustomTrigger2, CustomTrigger3, CustomTrigger4, CustomTrigger5, 
		CustomTrigger6, CustomTrigger7, CustomTrigger8, CustomTrigger9, CustomTrigger10 
	};
	
	public struct OpenBackUserInfo
	{
		public string AddressLine1;
		public string AddressLine2;
		public string AdvertisingId;
		public string Age;
		public string City;
		public string Country;
		public string CountryCode;
		public string DateOfBirth;
		public string Email;
		public string FirstName;
		public string Gender;
		public string OptInUpdates;
		public string PhoneNumber;
		public string PostCode;
		public string Profession;
		public string State;
		public string Surname;
		public string Title;
		public string Identity1;
		public string Identity2;
		public string Identity3;
		public string Identity4;
		public string Identity5;
	}
	
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

		public string getSdkVersion () {
			if (!Application.isEditor) {
				return plugin.getSdkVersion ();
			}
			return "0.0.0";
		}

		public bool setUserInfo (OpenBackUserInfo userInfo) {
			if (!Application.isEditor) {
				return plugin.setUserInfo (userInfo);
			}
			return true;
		}

		public bool setCustomTrigger (OpenBackTrigger trigger, string value) {
			if (!Application.isEditor) {
				return plugin.setCustomTrigger (trigger, value);
			}
			return true;
		}

		public bool setCustomTrigger (OpenBackTrigger trigger, int value) {
			if (!Application.isEditor) {
				return plugin.setCustomTrigger (trigger, value);
			}
			return true;
		}

		public bool setCustomTrigger (OpenBackTrigger trigger, float value) {
			if (!Application.isEditor) {
				return plugin.setCustomTrigger (trigger, value);
			}
			return true;
		}

		public void coppaCompliant (bool compliant) {
			if (!Application.isEditor) {
				plugin.coppaCompliant (compliant);
			}
		}

		public bool gdprForgetUser (bool forgetUser) {
			if (!Application.isEditor) {
				return plugin.gdprForgetUser (forgetUser);
			}
			return true;
		}

		public bool logGoal (string goal, int step, double value) {
			if (!Application.isEditor) {
				return plugin.logGoal (goal, step, value);
			}
			return true;
		}
	}
}

