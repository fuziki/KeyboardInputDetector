//
//  KeyboarInputDetector.m
//  UnityUser
//
//  Created by fuziki on 2019/08/30.
//  Copyright © 2019 fuziki.factory. All rights reserved.
//

#import <KeyboarInputDetector/KeyboarInputDetector-Swift.h>

typedef void (*OnKeyboarInputHandler) (const char* input);

extern "C" {
    KeyboarInputDetector* keyboarInputDetector_init();
    void keyboarInputDetector_startDetection(KeyboarInputDetector* detector, unsigned char* str);
    void keyboarInputDetector_stopDetection(KeyboarInputDetector* detector);
    void keyboarInputDetector_registerOnKeyboarInput(KeyboarInputDetector* detector, OnKeyboarInputHandler handler);
    void keyboarInputDetector_release(KeyboarInputDetector* detector);
}

KeyboarInputDetector* keyboarInputDetector_init() {
    KeyboarInputDetector* detector = [KeyboarInputDetector alloc];
    CFRetain((CFTypeRef)detector);
    return detector;
}

//uncomment when use in unity
/*
void keyboarInputDetector_startDetection(KeyboarInputDetector* detector, unsigned char* str) {
    [detector startDetectionWithUnityView: UnityGetGLViewController().view
                                     keys: @"wedcxzaqufhrytjnlvog"];
}
*/

void keyboarInputDetector_stopDetection(KeyboarInputDetector* detector) {
    [detector stopDetection];
}

void keyboarInputDetector_registerOnKeyboarInput(KeyboarInputDetector* detector, OnKeyboarInputHandler handler) {
    [detector onKeyInputWithHandler: ^(NSString* str) {
        handler([str UTF8String]);
    }];
}

void keyboarInputDetector_release(KeyboarInputDetector* detector) {
    CFRelease((CFTypeRef)detector);
}
