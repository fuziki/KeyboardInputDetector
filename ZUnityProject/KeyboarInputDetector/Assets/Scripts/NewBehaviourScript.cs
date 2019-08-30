using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    private ACGAMController.ACGAMR1 acgmR1;

    // Use this for initialization
    void Start () {
        acgmR1 = ACGAMController.ACGAMR1.current;
    }

    private bool rot = false;

    // Update is called once per frame
    void Update () {
        Vector2 v = acgmR1.joyStick.ReadValue() * 0.1f;
        this.transform.position += new Vector3(v.y, v.x, 0);

        if (acgmR1.buttonA.isPressed)
            this.GetComponent<Renderer>().material.color = Color.red;
        else if (acgmR1.buttonB.isPressed)
            this.GetComponent<Renderer>().material.color = Color.blue;
        else if (acgmR1.buttonC.isPressed)
            this.GetComponent<Renderer>().material.color = Color.green;
        else if (acgmR1.buttonD.isPressed)
            this.GetComponent<Renderer>().material.color = Color.yellow;
        else this.GetComponent<Renderer>().material.color = Color.white;

        if (acgmR1.triggerReturn.isPressed) rot = true;
        if (acgmR1.triggerOk.isPressed) rot = false;

        if (rot)
        {
            this.transform.Rotate(new Vector3(-1.5f, 0.8f, 3.8f));
        }
    }
}
