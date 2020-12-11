#import <OpenBack/OpenBack.h>
#import <UserNotifications/UserNotifications.h>

#ifdef __cplusplus
extern "C" {
#endif
    
    // Utils
    
    static char* convertNSStringToCString(const NSString* nsString) {
        if (nsString == NULL) {
            return NULL;
        }
        
        const char* nsStringUtf8 = [nsString UTF8String];
        char* cString = (char*)malloc(strlen(nsStringUtf8) + 1);
        strcpy(cString, nsStringUtf8);
        return cString;
    }
    
    static NSString* convertCStringToNSString(const char* string) {
        if (string != NULL) {
            return [NSString stringWithUTF8String:string];
        }
        return nil;
    }
    
    // Configuration
    
    const char* OpenBack_AppCode() {
        return convertNSStringToCString(OpenBack.appCode);
    }
    
    void OpenBack_SetAppCode(const char * appCode) {
        OpenBack.appCode = convertCStringToNSString(appCode);
    }
    
    bool OpenBack_AutoStart() {
        return OpenBack.autoStart;
    }
    
    void OpenBack_SetAutoStart(bool autoStart) {
        OpenBack.autoStart = autoStart;
    }
    
    bool OpenBack_GdprForgetUser() {
        return OpenBack.gdprForgetUser;
    }
    
    void OpenBack_SetGdprForgetUser(bool gdprForgetUser) {
        OpenBack.gdprForgetUser = gdprForgetUser;
    }
    
    bool OpenBack_CoppaCompliant() {
        return OpenBack.coppaCompliant;
    }
    
    void OpenBack_SetCoppaCompliant(bool coppaCompliant) {
        OpenBack.coppaCompliant = coppaCompliant;
    }
    
    bool OpenBack_HipaaCompliant() {
        return OpenBack.hipaaCompliant;
    }
    
    void OpenBack_SetHipaaCompliant(bool hipaaCompliant) {
        OpenBack.hipaaCompliant = hipaaCompliant;
    }
    
    int OpenBack_DebugLogLevel() {
        return (int)OpenBack.debugLogLevel;
    }
    
    void OpenBack_SetDebugLogLevel(int debugLogLevel) {
        OpenBack.debugLogLevel = debugLogLevel;
    }
    
    const char * OpenBack_SdkVersion () {
        return convertNSStringToCString(OpenBack.sdkVersion);
    }
    
    // Runtime
    
    bool OpenBack_Start() {
        return [OpenBack start];
    }
    
    void OpenBack_Stop() {
        [OpenBack stop];
    }
    
    bool OpenBack_Started() {
        return OpenBack.isStarted;
    }
    
    void OpenBack_ResetAll() {
        [OpenBack resetAll];
    }
    
    // Custom Segments
    
    void OpenBack_SetCustomSegment_Long(int segment, long value) {
        [OpenBack setValue:@(value) forCustomSegment:segment];
    }
    
    void OpenBack_SetCustomSegment_Double(int segment, double value) {
        [OpenBack setValue:@(value) forCustomSegment:segment];
    }
    
    void OpenBack_SetCustomSegment_String(int segment, char* value) {
        [OpenBack setValue:convertCStringToNSString(value) forCustomSegment:segment];
    }
    
    long OpenBack_GetCustomSegment_Long(int segment) {
        id value = [OpenBack getCustomSegment:segment];
        if ([value isKindOfClass:[NSString class]] || [value isKindOfClass:[NSNumber class]]) {
            return [value longValue];
        }
        return 0;
    }
    
    double OpenBack_GetCustomSegment_Double(int segment) {
        id value = [OpenBack getCustomSegment:segment];
        if ([value isKindOfClass:[NSString class]] || [value isKindOfClass:[NSNumber class]]) {
            return [value doubleValue];
        }
        return 0.0;
    }
    
    const char * OpenBack_GetCustomSegment_String(int segment) {
        id value = [OpenBack getCustomSegment:segment];
        if ([value isKindOfClass:[NSString class]]) {
            return convertNSStringToCString(value);
        } else if ([value isKindOfClass:[NSNumber class]]) {
            return convertNSStringToCString([value stringValue]);
        }
        return nil;
    }
    
    void OpenBack_RemoveAllCustomSegments() {
        [OpenBack removeAllCustomSegments];
    }
    
    // Attributes
    
    long OpenBack_GetAttribute_Long(const char * attributeKey) {
        id value = [OpenBack getAttribute:convertCStringToNSString(attributeKey)];
        if ([value isKindOfClass:[NSString class]] || [value isKindOfClass:[NSNumber class]]) {
            return [value longValue];
        }
        return 0;
    }
    
    double OpenBack_GetAttribute_Double(const char * attributeKey) {
        id value = [OpenBack getAttribute:convertCStringToNSString(attributeKey)];
        if ([value isKindOfClass:[NSString class]] || [value isKindOfClass:[NSNumber class]]) {
            return [value doubleValue];
        }
        return 0.0;
    }
    
    const char * OpenBack_GetAttribute_String(const char * attributeKey) {
        id value = [OpenBack getAttribute:convertCStringToNSString(attributeKey)];
        if ([value isKindOfClass:[NSString class]]) {
            return convertNSStringToCString(value);
        } else if ([value isKindOfClass:[NSNumber class]]) {
            return convertNSStringToCString([value stringValue]);
        }
        return nil;
    }
    
    void OpenBack_SetAttribute_Long(const char * attributeKey, long attributeValue) {
        [OpenBack setValue:@(attributeValue) forAttribute:convertCStringToNSString(attributeKey)];
    }
    
    void OpenBack_SetAttribute_Double(const char * attributeKey, double attributeValue) {
        [OpenBack setValue:@(attributeValue) forAttribute:convertCStringToNSString(attributeKey)];
    }
    
    void OpenBack_SetAttribute_String(const char * attributeKey, char * attributeValue) {
        [OpenBack setValue:convertCStringToNSString(attributeValue) forAttribute:convertCStringToNSString(attributeKey)];
    }
    
    void OpenBack_RemoveAllAttributes() {
        [OpenBack removeAllAttributes];
    }
    
    // Goal
    
    void OpenBack_LogGoal(const char * goal, int step, double value, const char * currency) {
        [OpenBack logGoal:convertCStringToNSString(goal) step:step value:value currency:convertCStringToNSString(currency)];
    }
    
    // Events
    
    void OpenBack_SignalEvent(const char * theEvent, long delay) {
        [OpenBack signalEvent:convertCStringToNSString(theEvent) withDelay:delay];
    }
    
    void OpenBack_CancelEvent(const char * theEvent) {
        [OpenBack cancelEvent:convertCStringToNSString(theEvent)];
    }
    
    // Dev Tools
    
    void OpenBack_CheckMessagesNow() {
        [OpenBack checkMessagesNow];
    }
    
    void OpenBack_ReloadMessagesNow() {
        [OpenBack reloadMessagesNow];
    }
    
    // Helpers
    
    void OpenBack_RequestNotificationAuthorization() {
        UNAuthorizationOptions options = UNAuthorizationOptionAlert | UNAuthorizationOptionSound | UNAuthorizationOptionBadge;
        [UNUserNotificationCenter.currentNotificationCenter requestAuthorizationWithOptions:options completionHandler:^(BOOL granted, NSError * _Nullable error) {
                    
        }];
    }

#ifdef __cplusplus
}
#endif


