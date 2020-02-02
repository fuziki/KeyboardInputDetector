//
//  ViewController.swift
//  PROJECT_NAME
//
//  Created by AUTHOR on YYYY/MM/DD.
//

import UIKit
import KeyboarInputDetector

class ViewController: UIViewController {
    
    var detector: KeyboarInputDetector!
    
    var aa: OnKeyboarInputHandler?

    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view.
        
        let a = add_one(33)
        print("a: \(a)")
    }
    
    override func viewDidAppear(_ animated: Bool) {
        detector = keyboarInputDetector_init()
        detector.startDetection(keys: "wedcxzaqufhrytjnlvog")
        keyboarInputDetector_registerOnKeyboarInput(detector, { (c) in
            print("c: \(String(describing: c?.pointee))")
        })
    }
}
