
namespace KeyboarInputDetector
{
    public class KeyboardInputDetector : IKeyboardInputDetector
    {
        private IKeyboardInputDetector detector;
        public KeyboardInputDetector()
        {
            detector = new KeyboardInputDetectorIOS();
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