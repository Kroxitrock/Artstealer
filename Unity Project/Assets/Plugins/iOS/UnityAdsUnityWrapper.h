//
//  UnityAdsUnityWrapper.h
//  Copyright (c) 2016 Unity Technologies. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UnityAds/UnityAds.h>

extern UIViewController* UnityGetGLViewController();

@interface UnityAdsUnityWrapper : NSObject <UnityAdsDelegate> {
}

- (id)initWithGameId:(NSString*)gameId testModeOn:(bool)testMode withGameObjectName:(NSString*)gameObjectName;

@end
