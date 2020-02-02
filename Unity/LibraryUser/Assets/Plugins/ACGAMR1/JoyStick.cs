using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACGAMController
{ 
    public class JoyStick
    {
        public enum Direction
        {
            Up, Douwn, Right, Left
        }
        public enum State
        {
            Press, Release
        }

        private int up = 0;
        private int douwn = 0;
        private int right = 0;
        private int left = 0;

        public void Update(Direction direction, State state)
        {
            switch(direction)
            {
                case Direction.Up:
                    up = state == State.Press ? 1 : 0; 
                    break;
                case Direction.Douwn:
                    douwn = state == State.Press ? 1 : 0;
                    break;
                case Direction.Right:
                    right = state == State.Press ? 1 : 0;
                    break;
                case Direction.Left:
                    left = state == State.Press ? 1 : 0;
                    break;
            }
        }

        public Vector2 ReadValue()
        {
            return new Vector2(up - douwn, right - left);
        }
    }
}

