//
//  KeyboarInputDetector.swift
//  KeyboarInputDetector
//
//  Created by fuziki on 2019/08/29.
//  Copyright © 2019 fuziki.factory. All rights reserved.
//

import Foundation

@objcMembers
public class KeyboarInputDetector: NSObject {
    
    private var unityView: UIView?
    
    private var detectorViewController: KeyboarInputDetectorViewController?
    
    public override init() {
        super.init()
    }
    
    public func startDetection(unityView: UIView, keys: String) {
        self.unityView = unityView
        
        KeyboarInputDetectorViewController.keyCommandString = keys
//        KeyboarInputDetectorViewController.keyCommandString = "wedcxzaq" + "ufhrytjn" + "lvog"
        
        detectorViewController = KeyboarInputDetectorViewController()
        detectorViewController?.view.frame = CGRect(x: 0, y: 0, width: 0, height: 0)
        detectorViewController?.view.backgroundColor = .clear
        
        unityView.addSubview(detectorViewController!.view)
    }
    
    public func stopDetection() {
        
    }
    
    public func onKeyInput(handler: @escaping (String) -> Void) {
        detectorViewController?.onKeyInput(handler: handler)
    }
}
