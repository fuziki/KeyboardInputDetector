//
//  ViewController.swift
//  ExampleIOS
//
//  Created by fuziki on 2019/08/28.
//  Copyright © 2019 fuziki.factory. All rights reserved.
//

import UIKit

class ViewController: UIViewController {
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view.
        print("start")
        KeyboarInputDetectorViewController.keyCommandString = "wedcxzaq" + "ufhrytjn" + "lvog"

        
//        let vc = UIStoryboard(name: "KeyInputDetector", bundle: nil).instantiateInitialViewController()!
//        let vc = KeyboarInputDetectorViewController()
//        vc.view.frame = CGRect(x: 0, y: 0, width: 0, height: 0)
//        vc.view.backgroundColor = .clear
//        addChild(vc)
//        self.view.addSubview(vc.view)

        let vc = KeyboarInputDetectorViewController()
        self.view.addSubview(vc)
    }
}


class KeyboarInputDetectorViewController: UIView {
    static var keyCommandString: String = ""
    private var onKeyInputHandler: ((String) -> Void)? = nil
    
    override func becomeFirstResponder() -> Bool {
        return true
    }
    
    override var keyCommands: [UIKeyCommand]? {
        return KeyboarInputDetectorViewController.keyCommandString.map({ (c: Character) -> UIKeyCommand in
            return UIKeyCommand(input: String(c), modifierFlags: [], action: #selector(handlerKeyInput(command:)))
        })
    }
    
    @objc private func handlerKeyInput(command: UIKeyCommand) {
        print("\(command)")
        if let str = command.input {
            onKeyInputHandler?(str)
        }
    }
    
    public func onKeyInput(handler: @escaping (String) -> Void) {
        onKeyInputHandler = handler
    }
}


