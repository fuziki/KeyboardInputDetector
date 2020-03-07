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

KeyboardInputDetector* keyboardInputDetector_init() {
    KeyboardInputDetector* detector = [KeyboardInputDetector alloc];
    CFRetain((CFTypeRef)detector);
    return detector;
}

void keyboardInputDetector_startDetection(KeyboardInputDetector* detector) {
    [detector startDetectionWithKeys: @"wedcxzaqufhrytjnlvog"];
}

void keyboardInputDetector_stopDetection(KeyboardInputDetector* detector) {
    [detector stopDetection];
}

void keyboardInputDetector_registerOnKeyboardInput(KeyboardInputDetector* detector, OnKeyboardInputHandler handler) {
    [detector onKeyInputWithHandler: ^(NSString* str) {
        handler([str UTF8String]);
    }];
}

void keyboardInputDetector_release(KeyboardInputDetector* detector) {
    CFRelease((CFTypeRef)detector);
}
