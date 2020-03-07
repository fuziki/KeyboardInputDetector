//
//  ViewController.swift
//  PROJECT_NAME
//
//  Created by AUTHOR on YYYY/MM/DD.
//

import UIKit
import KeyboardInputDetector

class ViewController: UIViewController {
    
    var detector: KeyboardInputDetector!
    
    var aa: OnKeyboardInputHandler?

    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view.
        
        let a = add_one(33)
        print("a: \(a)")
    }
    
    override func viewDidAppear(_ animated: Bool) {
        detector = keyboardInputDetector_init()
        detector.startDetection(keys: "wedcxzaqufhrytjnlvog")
        keyboardInputDetector_registerOnKeyboardInput(detector, { (c) in
            print("c: \(String(describing: c?.pointee))")
        })
    }
}
