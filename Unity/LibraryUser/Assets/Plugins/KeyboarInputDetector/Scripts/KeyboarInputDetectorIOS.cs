using System;
using System.Runtime.InteropServices;
using AOT;

namespace KeyboarInputDetector
{
    public class KeyboardInputDetectorIOS : IKeyboardInputDetector
    {

        [DllImport("__Internal")]
        private static extern IntPtr keyboarInputDetector_init();

        [DllImport("__Internal")]
        private static extern void keyboarInputDetector_startDetection(IntPtr detector, string str);

        [DllImport("__Internal")]
        private static extern void keyboarInputDetector_stopDetection(IntPtr detector);

        [DllImport("__Internal")]
        private static extern void keyboarInputDetector_registerOnKeyboarInput(IntPtr detector, OnKeyboarInputHandler handler);

        [DllImport("__Internal")]
        private static extern void keyboarInputDetector_release(IntPtr detector);


        private IntPtr detector;

        public KeyboardInputDetectorIOS()
        {
            detector = keyboarInputDetector_init();
        }

        ~KeyboardInputDetectorIOS()
        {
            keyboarInputDetector_release(detector);
        }

        public void StartDetection(string str)
        {
            keyboarInputDetector_startDetection(detector, str);
            keyboarInputDetector_registerOnKeyboarInput(detector, HandlerOnKeyboardInput);
        }

        public void StopDetection()
        {
            keyboarInputDetector_stopDetection(detector);
        }

        public event OnKeyboardInputDelegate OnKeyboardInput
        {
            add { onKeyboardInput += value; }
            remove  { onKeyboardInput -= value; }
        }
        private static event OnKeyboardInputDelegate onKeyboardInput;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OnKeyboarInputHandler(string input);

        [MonoPInvokeCallback(typeof(OnKeyboarInputHandler))]
        private static void HandlerOnKeyboardInput(string input)
        {
            if (onKeyboardInput != null)
                onKeyboardInput(input);
        }
    }
}
