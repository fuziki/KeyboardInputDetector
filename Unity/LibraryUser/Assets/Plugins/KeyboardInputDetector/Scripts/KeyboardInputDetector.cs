
namespace KeyboardInputDetector
{
    public class KeyboardInputDetector : IKeyboardInputDetector
    {
        private IKeyboardInputDetector detector;
        public KeyboardInputDetector()
        {
#if UNITY_EDITOR
            detector = null;
#elif UNITY_IOS
            detector = new KeyboardInputDetectorIOS();
#endif
        }

        public void StartDetection(string str)
        {
            if (detector == null) return;
            detector.StartDetection(str);
        }

        public void StopDetection()
        {
            if (detector == null) return;
            detector.StopDetection();
        }

        public event OnKeyboardInputDelegate OnKeyboardInput
        {
            add
            {
                if (detector == null) return;
                detector.OnKeyboardInput += value;
            }
            remove
            {
                if (detector == null) return;
                detector.OnKeyboardInput -= value;
            }
        }
    }
}