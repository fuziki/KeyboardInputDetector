//
//  NativePlugin.h
//  PROJECT_NAME
//
//  Created by AUTHOR on YYYY/MM/DD.
//

#ifndef NativePlugin_h
#define NativePlugin_h

#import <KeyboarInputDetector/KeyboarInputDetector-Swift.h>

typedef void (*OnKeyboarInputHandler) (const char* input);

#ifdef __cplusplus
extern "C" {
#endif
    int add_one(int num);

    KeyboarInputDetector* keyboarInputDetector_init();
    void keyboarInputDetector_startDetection(KeyboarInputDetector* detector);
    void keyboarInputDetector_stopDetection(KeyboarInputDetector* detector);
    void keyboarInputDetector_registerOnKeyboarInput(KeyboarInputDetector* detector, OnKeyboarInputHandler handler);
    void keyboarInputDetector_release(KeyboarInputDetector* detector);

#ifdef __cplusplus
}
#endif

#endif /* NativePlugin_h */
