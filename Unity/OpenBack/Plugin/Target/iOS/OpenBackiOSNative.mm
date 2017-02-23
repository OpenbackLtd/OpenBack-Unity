#import <OpenBack/OpenBack.h>
#import "UnityAppController.h"

extern "C" {
    // String Utils
    
    static char* cStringCopy(const char* string)
    {
        if (string == NULL)
            return NULL;
        char* res = (char*)malloc(strlen(string) + 1);
        strcpy(res, string);
        return res;
    }
    
    static NSString* CreateNSString(const char* string)
    {
        if (string != NULL) {
            return [NSString stringWithUTF8String:string];
        }
        return [NSString stringWithUTF8String:""];
    }
    
    const char* _getSdkVersion() {
        return cStringCopy((const char*)&OpenBackVersionString[0]);
    }
    
    bool _setUserInfo(const char *value) {
        NSString *jsonString = CreateNSString(value);
        NSData *objectData = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
        NSDictionary *info = [NSJSONSerialization JSONObjectWithData:objectData options:0 error:nil];
        return [OpenBack setUserInfo:info error:nil];
    }
    
    bool _setStringCustomTrigger(int trigger, const char *value) {
        return [OpenBack setValue:CreateNSString(value) forCustomTrigger:(OBKCustomTriggerType)trigger error:nil];
    }
    
    bool _setIntCustomTrigger(int trigger, int value) {
        return [OpenBack setValue:@(value) forCustomTrigger:(OBKCustomTriggerType)trigger error:nil];
    }
    
    bool _setLongCustomTrigger(int trigger, long value) {
        return [OpenBack setValue:@(value) forCustomTrigger:(OBKCustomTriggerType)trigger error:nil];
    }
    
    bool _setFloatCustomTrigger(int trigger, float value) {
        return [OpenBack setValue:@(value) forCustomTrigger:(OBKCustomTriggerType)trigger error:nil];
    }
    
    bool _setDoubleCustomTrigger(int trigger, double value) {
        return [OpenBack setValue:@(value) forCustomTrigger:(OBKCustomTriggerType)trigger error:nil];
    }
}

@interface OpenBackUnityAppController : UnityAppController

@end

@implementation OpenBackUnityAppController

- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(nullable NSDictionary<UIApplicationLaunchOptionsKey, id> *)launchOptions {
    
    NSDictionary *config = @{
        kOBKConfigAppCode: @"YOUR_APP_CODE_HERE",
        // Core settings
        kOBKConfigLogLevel: @(kOBKLogLevelNone),
        // Enable options
        kOBKConfigEnableAlertNotifications: @(YES),
        kOBKConfigEnableInAppNotifications: @(YES),
        kOBKConfigEnableRemoteNotifications: @(YES),
        kOBKConfigEnableProximity: @(NO),
        kOBKConfigEnableMotionCoprocessor: @(NO),
        kOBKConfigEnableMicrophone: @(NO),
        kOBKConfigEnableLocation: @(NO),
        kOBKConfigEnableLocationWhenInUse: @(NO),
        // Let OpenBack prompt for permissions
        kOBKConfigRequestAlertNotificationsAuthorization: @(YES),
        kOBKConfigRequestMotionCoprocessorAuthorization: @(YES),
        kOBKConfigRequestMicrophoneAuthorization: @(YES),
        kOBKConfigRequestLocationAlwaysAuthorization: @(YES),
        kOBKConfigRequestLocationWhenInUseAuthorization: @(YES)
    };
    
    NSError *error = nil;
    if ([OpenBack setupWithConfig:config error:&error]) {
        error = nil;
        if (![OpenBack start:&error]) {
            NSLog(@"Unable to start OpenBack: %@", error);
        }
    } else {
        NSLog(@"OpenBack configuration error: %@", error);
    }
    return [super application:application didFinishLaunchingWithOptions:launchOptions];
}

@end

IMPL_APP_CONTROLLER_SUBCLASS(OpenBackUnityAppController);