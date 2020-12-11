using System.Collections.Generic;

namespace OpenBackUnity
{
	public interface IOpenBackPlugin
	{
		// Configuration
		string AppCode { get; set; }
		bool AutoStart { get; set; }
		bool GdprForgetUser { get; set; }
		bool CoppaCompliant { get; set; }
		bool HipaaCompliant { get; set; }
		OpenBackLogLevel DebugLogLevel { get; set; }
		string SdkVersion { get; }

		// Runtime
		bool Start();
		void Stop();
		bool Started { get; }
		void ResetAll();
		
		// Custom Segments
		void SetCustomSegment(OpenBackSegment segment, long value);
		void SetCustomSegment(OpenBackSegment segment, double value);
		void SetCustomSegment(OpenBackSegment segment, string value);
		long GetCustomSegmentAsLong(OpenBackSegment segment);
		double GetCustomSegmentAsDouble(OpenBackSegment segment);
		string GetCustomSegmentAsString(OpenBackSegment segment);
		void RemoveAllCustomSegments();

		// Attributes
		void SetAttribute(string attributeKey, long attributeValue);
		void SetAttribute(string attributeKey, double attributeValue);
		void SetAttribute(string attributeKey, string attributeValue);
		long GetAttributeAsLong(string attributeKey);
		double GetAttributeAsDouble(string attributeKey);
		string GetAttributeAsString(string attributeKey);
		void RemoveAllAttributes();

		// Goals
		void LogGoal(string goal, int step, double value, string currency = null);

		// Event Signal
		void SignalEvent(string theEvent, long delay);
		void CancelEvent(string theEvent);

		// Developer Tools
		void CheckMessagesNow();
		void ReloadMessagesNow();
	}
}

