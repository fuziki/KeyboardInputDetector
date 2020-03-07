//
//  KeyboardInputDetector.swift
//  KeyboardInputDetector
//
//  Created by fuziki on 2019/08/29.
//  Copyright © 2019 fuziki.factory. All rights reserved.
//

#if !os(macOS)
import Foundation
import UIKit

@objcMembers
public class KeyboardInputDetector: NSObject {
    
    private var unityView: UIView?
    
    private var detectorViewController: KeyboardInputDetectorViewController?
    
    public override init() {
        super.init()
    }
    
    public func startDetection(keys: String) {
        var unityView = UIApplication.shared.keyWindow!.rootViewController
        while let presented = unityView?.presentedViewController {
            unityView = presented
        }
        
        startDetection(unityView: unityView!.view, keys: keys)
    }

    public func startDetection(unityView: UIView, keys: String) {
        self.unityView = unityView
        
        KeyboardInputDetectorViewController.keyCommandString = keys
//        KeyboardInputDetectorViewController.keyCommandString = "wedcxzaq" + "ufhrytjn" + "lvog"
        
        detectorViewController = KeyboardInputDetectorViewController()
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
#endif
