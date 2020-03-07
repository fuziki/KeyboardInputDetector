//
//  NativePlugin.h
//  PROJECT_NAME
//
//  Created by AUTHOR on YYYY/MM/DD.
//

#ifndef NativePlugin_h
#define NativePlugin_h

#import <KeyboardInputDetector/KeyboardInputDetector-Swift.h>

typedef void (*OnKeyboardInputHandler) (const char* input);

#ifdef __cplusplus
extern "C" {
#endif
    int add_one(int num);

    KeyboardInputDetector* keyboardInputDetector_init();
    void keyboardInputDetector_startDetection(KeyboardInputDetector* detector);
    void keyboardInputDetector_stopDetection(KeyboardInputDetector* detector);
    void keyboardInputDetector_registerOnKeyboardInput(KeyboardInputDetector* detector, OnKeyboardInputHandler handler);
    void keyboardInputDetector_release(KeyboardInputDetector* detector);

#ifdef __cplusplus
}
#endif

#endif /* NativePlugin_h */
