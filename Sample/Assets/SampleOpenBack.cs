using UnityEngine;
using UnityEngine.UI;
using OpenBackUnity;
using System.Collections;

public class SampleOpenBack : MonoBehaviour
{
    public Text versionText = null;
    public Text appCodeText = null;
    public Button requestButton = null;
    private readonly OpenBack openBack = OpenBack.SharedInstance;

    private void Start()
    {
        StartCoroutine(InitCoroutine());        
    }

    private void Update()
    {
        appCodeText.text = "App Code:\n" + openBack.AppCode?.ToUpper();
    }

    IEnumerator InitCoroutine()
    {
        yield return new WaitForEndOfFrame();

        versionText.text = "v" + openBack.SdkVersion;
        appCodeText.text = "App Code:\n" + openBack.AppCode?.ToUpper();
#if UNITY_ANDROID
        requestButton.gameObject.SetActive(false);
#endif
        openBack.DebugLogLevel = OpenBackLogLevel.Verbose;
        Debug.Log("Debug Level: " + openBack.DebugLogLevel);
    }

    public void StartOpenBack()
    {
        Debug.Log("App Code: " + openBack.AppCode);
        openBack.Start();
        Debug.Log("Started: " + openBack.Started);
    }

    public void StopOpenBack()
    {
        openBack.Stop();
        Debug.Log("Started: " + openBack.Started);
    }

    public void SetSegment1()
    {
        openBack.SetCustomSegment(OpenBackSegment.CustomSegment1, "Bob");
        Debug.Log("Segment 1: " + openBack.GetCustomSegmentAsString(OpenBackSegment.CustomSegment1));
    }

    public void SetSegment2()
    {
        openBack.SetCustomSegment(OpenBackSegment.CustomSegment2, 42);
        Debug.Log("Segment 2: " + openBack.GetCustomSegmentAsLong(OpenBackSegment.CustomSegment2));
    }

    public void SetSegment3()
    {
        openBack.SetCustomSegment(OpenBackSegment.CustomSegment3, 1.13);
        Debug.Log("Segment 3: " + openBack.GetCustomSegmentAsDouble(OpenBackSegment.CustomSegment3));
    }

    public void SetAttributes()
    {
        openBack.SetAttribute("name", "Jane");
        openBack.SetAttribute(OpenBack.UserAge, 33);
        openBack.SetAttribute("cash", 1234.56789);

        Debug.Log("Name: " + openBack.GetAttributeAsString("name"));
        Debug.Log("Age: " + openBack.GetAttributeAsLong(OpenBack.UserAge));
        Debug.Log("Cash: " + openBack.GetAttributeAsDouble("cash"));        
    }

    public void LogGoal()
    {
        openBack.LogGoal("test", 1, 12.3);
        openBack.LogGoal("test", 2, 23.4, "EUR");
    }

    public void SignalEvent()
    {
        openBack.SignalEvent("hey", 3);
    }

    public void CancelEvent()
    {
        openBack.CancelEvent("hey");
    }

    public void CheckMessages()
    {
        openBack.CheckMessagesNow();
    }

    public void LoadMessages()
    {
        openBack.ReloadMessagesNow();
    }

    public void RequestAuth()
    {
    #if UNITY_IOS
        OpenBackIOSHelper.RequestNotificationAuthorization();
    #endif
    }
}