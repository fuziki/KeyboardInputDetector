using System;
using System.Runtime.InteropServices;
using AOT;

namespace KeyboardInputDetector
{
    public class KeyboardInputDetectorIOS : IKeyboardInputDetector
    {

        [DllImport("__Internal")]
        private static extern IntPtr keyboardInputDetector_init();

        [DllImport("__Internal")]
        private static extern void keyboardInputDetector_startDetection(IntPtr detector, string str);

        [DllImport("__Internal")]
        private static extern void keyboardInputDetector_stopDetection(IntPtr detector);

        [DllImport("__Internal")]
        private static extern void keyboardInputDetector_registerOnKeyboardInput(IntPtr detector, OnKeyboardInputHandler handler);

        [DllImport("__Internal")]
        private static extern void keyboardInputDetector_release(IntPtr detector);


        private IntPtr detector;

        public KeyboardInputDetectorIOS()
        {
            detector = keyboardInputDetector_init();
        }

        ~KeyboardInputDetectorIOS()
        {
            keyboardInputDetector_release(detector);
        }

        public void StartDetection(string str)
        {
            keyboardInputDetector_startDetection(detector, str);
            keyboardInputDetector_registerOnKeyboardInput(detector, HandlerOnKeyboardInput);
        }

        public void StopDetection()
        {
            keyboardInputDetector_stopDetection(detector);
        }

        public event OnKeyboardInputDelegate OnKeyboardInput
        {
            add { onKeyboardInput += value; }
            remove  { onKeyboardInput -= value; }
        }
        private static event OnKeyboardInputDelegate onKeyboardInput;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OnKeyboardInputHandler(string input);

        [MonoPInvokeCallback(typeof(OnKeyboardInputHandler))]
        private static void HandlerOnKeyboardInput(string input)
        {
            if (onKeyboardInput != null)
                onKeyboardInput(input);
        }
    }
}
