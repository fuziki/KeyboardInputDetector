# KeyboarInputDetector
![Platform](https://img.shields.io/badge/platform-%20iOS%20%7C%20macOS%20-lightgrey.svg)
![Unity](https://img.shields.io/badge/unity-2018-green.svg)
![Xode](https://img.shields.io/badge/xcode-xcode11-green.svg)

## Created By [UnityPluginXcodeTemplate](https://github.com/fuziki/UnityPluginXcodeTemplate)

## [Download Unity Package](https://github.com/fuziki/KeyboarInputDetector/releases/tag/v0.0.1)

---

## はじめに
良さげなコントローラのUnityのNative Pluginを作りました。
<blockquote class="twitter-tweet"><p lang="ja" dir="ltr">すごく良さげなコントローラがあったので、Unityのネイティブプラグインを作りました！<br>持ちやすいし、色々なとこで使えそう！<br>コードはこちら↓<a href="https://t.co/sc811633zf">https://t.co/sc811633zf</a> <a href="https://t.co/Zv7VqE6dVa">pic.twitter.com/Zv7VqE6dVa</a></p>&mdash; ふじき (@fzkqi) <a href="https://twitter.com/fzkqi/status/1167433531405914112?ref_src=twsrc%5Etfw">August 30, 2019</a></blockquote> <script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>

## 環境
* Unity: 2018.2.5
* Xcode: 10.2.1
* iOS: 12.4
* リポジトリ: https://github.com/fuziki/KeyboarInputDetector

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
[KeyboarInputDetectorViewController.swift](https://github.com/fuziki/KeyboarInputDetector/blob/master/KeyboarInputDetector/KeyboarInputDetectorViewController.swift)

#### step.2 作ったViewControllerをUnityのViewのsubViewに追加する

``` KeyboarInputDetector.swift
        detectorViewController = KeyboarInputDetectorViewController（）
        detectorViewController.view.frame = CGRect(x: 0, y: 0, width: 0, height: 0)
        detectorViewController.view.backgroundColor = .clear
        UnityGetGLViewController().view.addSubview（detectorViewController.view）
```

#### step.3 swiftのフレームワークを呼び出すネイティブプラグインを実装する

``` c++:
#import <KeyboarInputDetector/KeyboarInputDetector-Swift.h>

typedef void (*OnKeyboarInputHandler) (const char* input);
extern "C" {
    KeyboarInputDetector* keyboarInputDetector_init();
    void keyboarInputDetector_startDetection(KeyboarInputDetector* detector, unsigned char* str);
    void keyboarInputDetector_stopDetection(KeyboarInputDetector* detector);
    void keyboarInputDetector_registerOnKeyboarInput(KeyboarInputDetector* detector, OnKeyboarInputHandler handler);
    void keyboarInputDetector_release(KeyboarInputDetector* detector);
}

KeyboarInputDetector* keyboarInputDetector_init() {
    KeyboarInputDetector* detector = [KeyboarInputDetector alloc];
    CFRetain((CFTypeRef)detector);
    return detector;
}

void keyboarInputDetector_startDetection(KeyboarInputDetector* detector, unsigned char* str) {
    [detector startDetectionWithUnityView: UnityGetGLViewController().view
                                     keys: @"wedcxzaqufhrytjnlvog"];
}

void keyboarInputDetector_stopDetection(KeyboarInputDetector* detector) {
    [detector stopDetection];
}

void keyboarInputDetector_registerOnKeyboarInput(KeyboarInputDetector* detector, OnKeyboarInputHandler handler) {
    [detector onKeyInputWithHandler: ^(NSString* str) {
        handler([str UTF8String]);
    }];
}
```
#### step.4 Unityから呼び出す

``` c#:
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
```




