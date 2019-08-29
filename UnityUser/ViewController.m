//
//  ViewController.m
//  UnityUser
//
//  Created by fuziki on 2019/08/29.
//  Copyright © 2019 fuziki.factory. All rights reserved.
//

#import "ViewController.h"
#import <KeyboarInputDetector/KeyboarInputDetector-Swift.h>


@interface ViewController ()

@end

@implementation ViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view.
    
    KeyboarInputDetector* detector = [KeyboarInputDetector alloc];
    [detector startDetectionWithUnityView:self.view
                                     keys:@"wedcxzaqufhrytjnlvog"];

    [detector onKeyInputWithHandler: ^(NSString* str) {
        NSLog(@"unity user input: %@", str);
    }];
}

@end




