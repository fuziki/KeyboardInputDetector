
namespace KeyboardInputDetector
{
    public delegate void OnKeyboardInputDelegate(string input);
    public interface IKeyboardInputDetector
    {
        void StartDetection(string str);
        void StopDetection();
        event OnKeyboardInputDelegate OnKeyboardInput;
    }
}
