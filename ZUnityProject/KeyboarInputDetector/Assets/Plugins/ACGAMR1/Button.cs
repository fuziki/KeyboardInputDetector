using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button {
    public enum State {
        Press, Release
    }
    private bool value = false;

    public void Update(State state)
    {
        value = state == State.Press;
    }

    public bool isPressed
    {
        get
        {
            return value;
        }
    }
}
