using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    private KeyboarInputDetector.KeyboardInputDetector detector;

	// Use this for initialization
	void Start () {
        detector = new KeyboarInputDetector.KeyboardInputDetector();
        detector.StartDetection("wedcxzaqufhrytjnlvog");
        detector.OnKeyboardInput += OnOnKeyboardInput;
    }

    void OnOnKeyboardInput(string input)
    {
        Debug.Log(input);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
