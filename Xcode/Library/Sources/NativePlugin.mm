//
//  NativePlugin.mm
//  PROJECT_NAME
//
//  Created by AUTHOR on YYYY/MM/DD.
//

#include <stdio.h>

//#import IMPORT_SWIFT_HEADER
#import "NativePlugin.h"

int add_one(int num) {
    return (int)[AddOne addWithNum: num];
}

KeyboarInputDetector* keyboarInputDetector_init() {
    KeyboarInputDetector* detector = [KeyboarInputDetector alloc];
    CFRetain((CFTypeRef)detector);
    return detector;
}

void keyboarInputDetector_startDetection(KeyboarInputDetector* detector) {
    [detector startDetectionWithKeys: @"wedcxzaqufhrytjnlvog"];
}

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
