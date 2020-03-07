# KeyboardInputDetector
![Platform](https://img.shields.io/badge/platform-%20iOS%20-lightgrey.svg)
![Unity](https://img.shields.io/badge/unity-2018-green.svg)
![Xode](https://img.shields.io/badge/xcode-xcode11-green.svg)

## Created By [UnityPluginXcodeTemplate](https://github.com/fuziki/UnityPluginXcodeTemplate)

## [Download Unity Package](https://github.com/fuziki/KeyboardInputDetector/releases/tag/v0.0.1)

---

## はじめに
良さげなコントローラのUnityのNative Pluginを作りました。
<blockquote class="twitter-tweet"><p lang="ja" dir="ltr">すごく良さげなコントローラがあったので、Unityのネイティブプラグインを作りました！<br>持ちやすいし、色々なとこで使えそう！<br>コードはこちら↓<a href="https://t.co/sc811633zf">https://t.co/sc811633zf</a> <a href="https://t.co/Zv7VqE6dVa">pic.twitter.com/Zv7VqE6dVa</a></p>&mdash; ふじき (@fzkqi) <a href="https://twitter.com/fzkqi/status/1167433531405914112?ref_src=twsrc%5Etfw">August 30, 2019</a></blockquote> <script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>

## 環境
* Unity: 2018.2.5
* Xcode: 10.2.1
* iOS: 12.4
* リポジトリ: https://github.com/fuziki/KeyboardInputDetector

## 目的
**良さげなコントローラがあったので、Unityで使ってみる**
持ちやすくて、操作がしやすそうなコントローラがあったので、Unityで使いたいなと思いました。
という訳で、とりあえずBluetooth接続したら、こう↓でした。

<blockquote class="twitter-tweet"><p lang="ja" dir="ltr">これ、実はキーボードなんですよね…（） <a href="https://t.co/BDzfET8531">pic.twitter.com/BDzfET8531</a></p>&mdash; ふじき (@fzkqi) <a href="https://twitter.com/fzkqi/status/1168166473547665408?ref_src=twsrc%5Etfw">September 1, 2019</a></blockquote> <script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>

という訳で、今回の目的がこちら
**~~良さげなコントローラがあったので、Unityで使ってみる~~**
**↓**
**UnityのiOSアプリで、キーボードを出さずに、キーボードの入力を受け取る**

## UnityのiOSアプリで、キーボードを出さずに、キーボードの入力を受け取る
### 試したこと
#### TouchScreenKeyboard.hideInput
[TouchScreenKeyboard.hideInput](https://docs.unity3d.com/ScriptReference/TouchScreenKeyboard-hideInput.html)を使って、キーボードを非表示にしてキー入力を受け取る
→失敗
※類似APIも期待した動作になりませんでした、、、

### UIResponder.keyCommands
[UIResponder.keyCommands](https://developer.apple.com/documentation/uikit/uiresponder/1621141-keycommands)にUIKeyCommandを使って、キー入力を受け取る。
→成功

## コントローラの入力を解析する
| input | On Press | On Release |
|:-----------|:------------:|:------------:|
| JoyStick.up | w | e|
| JoyStick.Right | d | c|
| JoyStick.Down | x | z |
| JoyStick.Left | a | q |
| Button.A | u | f |
| Button.B | h | r |
| Button.C | y | t |
| Button.D | j | n |
| Trigger.Return | l | v |
| Trigger.OK | o | g |
**→これらの入力を検出し、通知することで、コントローラとして利用できる**


## UIResponder.keyCommands
### UIResponder.keyCommandsを設定する
キー入力を受け取る度に、keyCommandsが探索され、入力の組み合わせと合致するkeyCommandsを設定したUIKeyCommandのactionに通知されます。
mapを使って文字列からUIKeyCommand配列を作っています。

``` ViewController.swift
    override var keyCommands: [UIKeyCommand]? {
        return "wedcxzaqufhrytjnlvog".map({ (c: Character) -> UIKeyCommand in
            return UIKeyCommand(input: String(c), modifierFlags: [], action: #selector(handlerKeyInput(command:)))
        })
    }
```

UIResponderのbecomeFirstResponder()を使って、キー入力が自分に来るようにします。

``` ViewController.swift
    override func viewDidAppear(_ animated: Bool) {
        _ = self.becomeFirstResponder()
    }
```

### UIResponder.keyCommandsをUnityで利用する。
ネイティブプラグインを作ります。
#### step.1 指定されたキー入力を受け取り、通知するViewControllerを実装する
[KeyboardInputDetectorViewController.swift](https://github.com/fuziki/KeyboardInputDetector/blob/master/KeyboardInputDetector/KeyboardInputDetectorViewController.swift)

#### step.2 作ったViewControllerをUnityのViewのsubViewに追加する

``` KeyboardInputDetector.swift
        detectorViewController = KeyboardInputDetectorViewController（）
        detectorViewController.view.frame = CGRect(x: 0, y: 0, width: 0, height: 0)
        detectorViewController.view.backgroundColor = .clear
        UnityGetGLViewController().view.addSubview（detectorViewController.view）
```

#### step.3 swiftのフレームワークを呼び出すネイティブプラグインを実装する

``` c++:
#import <KeyboardInputDetector/KeyboardInputDetector-Swift.h>

typedef void (*OnKeyboardInputHandler) (const char* input);
extern "C" {
    KeyboardInputDetector* KeyboardInputDetector_init();
    void KeyboardInputDetector_startDetection(KeyboardInputDetector* detector, unsigned char* str);
    void KeyboardInputDetector_stopDetection(KeyboardInputDetector* detector);
    void KeyboardInputDetector_registerOnKeyboardInput(KeyboardInputDetector* detector, OnKeyboardInputHandler handler);
    void KeyboardInputDetector_release(KeyboardInputDetector* detector);
}

KeyboardInputDetector* KeyboardInputDetector_init() {
    KeyboardInputDetector* detector = [KeyboardInputDetector alloc];
    CFRetain((CFTypeRef)detector);
    return detector;
}

void KeyboardInputDetector_startDetection(KeyboardInputDetector* detector, unsigned char* str) {
    [detector startDetectionWithUnityView: UnityGetGLViewController().view
                                     keys: @"wedcxzaqufhrytjnlvog"];
}

void KeyboardInputDetector_stopDetection(KeyboardInputDetector* detector) {
    [detector stopDetection];
}

void KeyboardInputDetector_registerOnKeyboardInput(KeyboardInputDetector* detector, OnKeyboardInputHandler handler) {
    [detector onKeyInputWithHandler: ^(NSString* str) {
        handler([str UTF8String]);
    }];
}
```
#### step.4 Unityから呼び出す

``` c#:
namespace KeyboardInputDetector
{
    public class KeyboardInputDetectorIOS : IKeyboardInputDetector
    {

        [DllImport("__Internal")]
        private static extern IntPtr KeyboardInputDetector_init();

        [DllImport("__Internal")]
        private static extern void KeyboardInputDetector_startDetection(IntPtr detector, string str);

        [DllImport("__Internal")]
        private static extern void KeyboardInputDetector_stopDetection(IntPtr detector);

        [DllImport("__Internal")]
        private static extern void KeyboardInputDetector_registerOnKeyboardInput(IntPtr detector, OnKeyboardInputHandler handler);

        [DllImport("__Internal")]
        private static extern void KeyboardInputDetector_release(IntPtr detector);


        private IntPtr detector;

        public KeyboardInputDetectorIOS()
        {
            detector = KeyboardInputDetector_init();
        }

        ~KeyboardInputDetectorIOS()
        {
            KeyboardInputDetector_release(detector);
        }

        public void StartDetection(string str)
        {
            KeyboardInputDetector_startDetection(detector, str);
            KeyboardInputDetector_registerOnKeyboardInput(detector, HandlerOnKeyboardInput);
        }

        public void StopDetection()
        {
            KeyboardInputDetector_stopDetection(detector);
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
```




