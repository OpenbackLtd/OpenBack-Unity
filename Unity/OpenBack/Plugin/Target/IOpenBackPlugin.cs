using System;

namespace OpenBackUnity
{
	public interface IOpenBackPlugin
	{
		bool setUserInfo (OpenBackUserInfo userInfo);
		bool setCustomTrigger (OpenBackTrigger trigger, string value);
		bool setCustomTrigger (OpenBackTrigger trigger, int value);
		bool setCustomTrigger (OpenBackTrigger trigger, long value);
		bool setCustomTrigger (OpenBackTrigger trigger, float value);
		bool setCustomTrigger (OpenBackTrigger trigger, double value);

		void coppaCompliant (bool compliant);
		bool gdprForgetUser (bool forgetUser);
		bool logGoal (string goal, int step, double value);

        void triggerEvent(string eventName, long delay);
		void cancelEvent(string eventName);

		void changeAppCode (string appCode);

		string getSdkVersion ();
	}
}

