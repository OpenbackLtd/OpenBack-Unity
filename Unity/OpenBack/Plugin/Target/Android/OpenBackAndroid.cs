#if UNITY_ANDROID

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenBackUnity {

	class OpenBackAndroid : IOpenBackPlugin {		
		AndroidJavaObject context;
		AndroidJavaClass openBack;

		public OpenBackAndroid () {
			// Store application context
			AndroidJavaClass unityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity");
			context = activity.Call<AndroidJavaObject> ("getApplicationContext");
			// Store OpenBack class
			openBack = new AndroidJavaClass ("com.openback.OpenBack");
		}

		public string getSdkVersion () {
			return openBack.CallStatic<string> ("getSdkVersion");
		}

		public bool setUserInfo (OpenBackUserInfo userInfo) {
			// Convert User Info
			AndroidJavaObject user = new AndroidJavaObject ("com.openback.UserInfoExtra");
			user.Set ("AddressLine1", userInfo.AddressLine1 ?? "");
			user.Set ("AddressLine2", userInfo.AddressLine2 ?? "");
			user.Set ("AdvertisingId", userInfo.AdvertisingId ?? "");
			user.Set ("Age", userInfo.Age ?? "");
			user.Set ("City", userInfo.City ?? "");
			user.Set ("Country", userInfo.Country ?? "");
			user.Set ("CountryCode", userInfo.CountryCode ?? "");
			user.Set ("DateOfBirth", userInfo.DateOfBirth ?? "");
			user.Set ("FirstName", userInfo.FirstName ?? "");
			user.Set ("Gender", userInfo.Gender ?? "");
			user.Set ("OptInUpdates", userInfo.OptInUpdates ?? "");
			user.Set ("PostCode", userInfo.PostCode ?? "");
			user.Set ("Profession", userInfo.Profession ?? "");
			user.Set ("State", userInfo.State ?? "");
			user.Set ("Surname", userInfo.Surname ?? "");
			user.Set ("Title", userInfo.Title ?? "");
			user.Set ("Identity1", userInfo.Identity1 ?? "");
			user.Set ("Identity2", userInfo.Identity2 ?? "");
			user.Set ("Identity3", userInfo.Identity3 ?? "");
			user.Set ("Identity4", userInfo.Identity4 ?? "");
			user.Set ("Identity5", userInfo.Identity5 ?? "");
			// Set the Config
			AndroidJavaObject config = new AndroidJavaObject ("com.openback.OpenBack$Config", context);
			config.Call<AndroidJavaObject> ("setExtraUserInfo", user);
			if (userInfo.Email != null) {
				config.Call<AndroidJavaObject> ("setUserEmail", userInfo.Email);
			}
			if (userInfo.PhoneNumber != null) {
				config.Call<AndroidJavaObject> ("setUserMsisdn", userInfo.PhoneNumber);
			}
			openBack.CallStatic ("update", config);

			return true;
		}

		public bool setCustomTrigger (OpenBackTrigger trigger, string value) {
			openBack.CallStatic ("setCustomTrigger", context, (int)trigger + 1, value);
			return true;
		}

		public bool setCustomTrigger (OpenBackTrigger trigger, int value) {
			openBack.CallStatic ("setCustomTrigger", context, (int)trigger + 1, value);
			return true;
		}

		public bool setCustomTrigger (OpenBackTrigger trigger, long value) {
			openBack.CallStatic ("setCustomTrigger", context, (int)trigger + 1, value);
			return true;
		}

		public bool setCustomTrigger (OpenBackTrigger trigger, float value) {
			openBack.CallStatic ("setCustomTrigger", context, (int)trigger + 1, value);
			return true;
		}

		public bool setCustomTrigger (OpenBackTrigger trigger, double value) {
			openBack.CallStatic ("setCustomTrigger", context, (int)trigger + 1, value);
			return true;
		}

		public bool gdprForgetUser (bool forgetUser) {
			openBack.CallStatic ("gdprForgetUser", context, forgetUser);
			return true;
		}

		public void coppaCompliant (bool compliant) {
			openBack.CallStatic ("coppaCompliant", context, compliant);
		}

		public bool logGoal (string goal, int step, double value) {
			return openBack.CallStatic<bool> ("logGoal", context, goal, step, value);
		}
	}
}

#endif