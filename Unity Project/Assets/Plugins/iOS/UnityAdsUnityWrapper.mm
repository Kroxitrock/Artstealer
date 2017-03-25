//
//  UnityAdsUnityWrapper.m
//  Copyright (c) 2016 Unity Technologies. All rights reserved.
//

#import "UnityAdsUnityWrapper.h"
#import "UnityAppController.h"

static UnityAdsUnityWrapper *unityAds = NULL;

void UnitySendMessage(const char* obj, const char* method, const char* msg);
void UnityPause(bool pause);

extern "C" {
  NSString* UnityAdsCreateNSString (const char* string) {
    return string ? [NSString stringWithUTF8String: string] : [NSString stringWithUTF8String: ""];
  }
  
  char* UnityAdsMakeStringCopy (const char* string) {
    if (string == NULL)
      return NULL;
    char* res = (char*)malloc(strlen(string) + 1);
    strcpy(res, string);
    return res;
  }
}

@interface UnityAdsUnityWrapper () <UnityAdsDelegate>
@property (nonatomic, strong) NSString* gameObjectName;
@property (nonatomic, strong) NSString* gameId;
@end

@implementation UnityAdsUnityWrapper

- (id)initWithGameId:(NSString*)gameId testModeOn:(bool)testMode withGameObjectName:(NSString*)gameObjectName {
  self = [super init];
  
  if (self != nil) {
    self.gameObjectName = gameObjectName;
    self.gameId = gameId;

    [UnityAds initialize:gameId delegate:self testMode:testMode];
  }
  
  return self;
}

- (void)unityAdsReady:(NSString *)placementId {
    NSLog(@"Unity Ads iOS wrapper: Unity Ads ready");
    UnitySendMessage(UnityAdsMakeStringCopy([self.gameObjectName UTF8String]), "onUnityAdsReady", [placementId UTF8String]);
}

- (void)unityAdsDidError:(UnityAdsError)error withMessage:(NSString *)message {
    NSLog(@"Unity Ads iOS wrapper: Unity Ads error");
    // ignore error events
}

- (void)unityAdsDidStart:(NSString *)placementId {
    UnityPause(true);
    NSLog(@"Unity Ads iOS wrapper: Unity Ads start");
    UnitySendMessage(UnityAdsMakeStringCopy([self.gameObjectName UTF8String]), "onUnityAdsStart", [placementId UTF8String]);
}

- (void)unityAdsDidFinish:(NSString *)placementId withFinishState:(UnityAdsFinishState)state {
    UnityPause(false);
    switch(state) {
        case kUnityAdsFinishStateError:
            NSLog(@"Unity Ads iOS wrapper: Unity Ads failed");
            UnitySendMessage(UnityAdsMakeStringCopy([self.gameObjectName UTF8String]), "onUnityAdsFailed", [placementId UTF8String]);
            break;
        case kUnityAdsFinishStateSkipped:
            NSLog(@"Unity Ads iOS wrapper: Unity Ads skipped");
            UnitySendMessage(UnityAdsMakeStringCopy([self.gameObjectName UTF8String]), "onUnityAdsSkipped", [placementId UTF8String]);
            break;
        case kUnityAdsFinishStateCompleted:
            NSLog(@"Unity Ads iOS wrapper: Unity Ads completed");
            UnitySendMessage(UnityAdsMakeStringCopy([self.gameObjectName UTF8String]), "onUnityAdsCompleted", [placementId UTF8String]);
            break;
        default:
            break;
    }
}

extern "C" {
  void UnityAdsInit (const char *gameId, bool testMode, const char *gameObjectName) {
    if(unityAds == NULL) {
      unityAds = [[UnityAdsUnityWrapper alloc] initWithGameId:UnityAdsCreateNSString(gameId) testModeOn:testMode withGameObjectName:UnityAdsCreateNSString(gameObjectName)];
    }
  }

  bool UnityAdsIsSupported() {
    return [UnityAds isSupported];
  }

  const char *UnityAdsGetVersion() {
    return UnityAdsMakeStringCopy([[UnityAds getVersion] UTF8String]);
  }

  bool UnityAdsIsInitialized() {
    return [UnityAds isInitialized];
  }

  bool UnityAdsGetDebugMode() {
    return [UnityAds getDebugMode];
  }

  void UnityAdsSetDebugMode(bool debugMode) {
    [UnityAds setDebugMode:debugMode];
  }

  void UnityAdsShow(const char *placementId) {
    if(placementId != NULL) {
      [UnityAds show:UnityGetGLViewController() placementId:UnityAdsCreateNSString(placementId)];
    } else {
      [UnityAds show:UnityGetGLViewController()];
    }
  }

  bool UnityAdsIsReady(const char *placementId) {
    if(placementId != NULL) {
      return [UnityAds isReady:UnityAdsCreateNSString(placementId)];
    } else {
      return [UnityAds isReady];
    }
  }

  long UnityAdsGetPlacementState(const char *placementId) {
    if(placementId != NULL) {
      return [UnityAds getPlacementState:[NSString stringWithUTF8String:placementId]];
    } else {
      return [UnityAds getPlacementState];
    }
  }

  void UnityAdsSetMetaData(const char *category, const char *data) {
    if(category != NULL && data != NULL) {
      UADSMetaData* metaData = [[UADSMetaData alloc] initWithCategory:[NSString stringWithUTF8String:category]];
      NSDictionary* json = [NSJSONSerialization JSONObjectWithData:[[NSString stringWithUTF8String:data] dataUsingEncoding:NSUTF8StringEncoding] options:0 error:nil];
      for(id key in json) {
        [metaData set:key value:[json objectForKey:key]];
      }
      [metaData commit];
    }
  }
}

@end
