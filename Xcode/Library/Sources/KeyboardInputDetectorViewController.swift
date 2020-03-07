//
//  KeyboardInputDetectorViewController.swift
//  KeyboardInputDetector
//
//  Created by fuziki on 2019/08/29.
//  Copyright © 2019 fuziki.factory. All rights reserved.
//

#if !os(macOS)
import UIKit

internal class KeyboardInputDetectorViewController: UIViewController {
    static var keyCommandString: String = ""
    private var onKeyInputHandler: ((String) -> Void)? = nil
    
    override func viewDidAppear(_ animated: Bool) {
        _ = self.becomeFirstResponder()
    }
    
    override var canBecomeFirstResponder: Bool {
        return true
    }
    
    override var keyCommands: [UIKeyCommand]? {
        return KeyboardInputDetectorViewController.keyCommandString.map({ (c: Character) -> UIKeyCommand in
            return UIKeyCommand(input: String(c), modifierFlags: [], action: #selector(handlerKeyInput(command:)))
        })
    }
    
    @objc private func handlerKeyInput(command: UIKeyCommand) {
        if let str = command.input {
            onKeyInputHandler?(str)
        }
    }
    
    public func onKeyInput(handler: @escaping (String) -> Void) {
        onKeyInputHandler = handler
    }
}
#endif
