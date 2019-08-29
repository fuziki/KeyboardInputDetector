using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ACGAMController
{

    public class JoyStick
    {
        public enum Direction
        {
            Horizon, Vertical
        }
        private int h = 0;
        private int v = 0;
        public JoyStick()
        {

        }

        public void move(Direction direction, int power)
        {
            if (direction == Direction.Horizon)
            {
                h += power;
            } 
            else
            {
                v += power;
            }
        }

        public Vector2 position
        {
            get
            {
                return new Vector2(v, h);
            }
        }

    }

    public class ACGAMR1
    {

        private Lazy<ACGAMR1> _current = new Lazy<ACGAMR1>(() => new ACGAMR1());

        public ACGAMR1 current
        {
            get { return _current.Value; }
        }

        public JoyStick joyStick = new JoyStick();

        private KeyboarInputDetector.KeyboardInputDetector detector;

        private ACGAMR1()
        {
            detector = new KeyboarInputDetector.KeyboardInputDetector();
            detector.StartDetection("wedcxzaqufhrytjnlvog");
            detector.OnKeyboardInput += OnOnKeyboardInput;
        }

        private void OnOnKeyboardInput(string input)
        {
            foreach(char c in input) {
                this.handleInputChar(c);
            }
        }

        private char lastc = '0';
        private void handleInputChar(char c)
        {
            if (c == lastc) return;
            lastc = c;
            switch(c)
            {
                //wedcxzaq ufhrytjn lvog
                case 'w':
                    joyStick.move(JoyStick.Direction.Vertical, 1);
                    break;
                case 'e':
                    joyStick.move(JoyStick.Direction.Vertical, 1);
                    break;
                case 'd':
                    joyStick.move(JoyStick.Direction.Horizon, -1);
                    break;
                case 'c':
                    joyStick.move(JoyStick.Direction.Horizon, -1);
                    break;
                case 'x':
                    joyStick.move(JoyStick.Direction.Vertical, -1);
                    break;
                case 'z':
                    joyStick.move(JoyStick.Direction.Vertical, 1);
                    break;
                case 'a':
                    joyStick.move(JoyStick.Direction.Horizon, -1);
                    break;
                case 'q':
                    joyStick.move(JoyStick.Direction.Horizon, 1);
                    break;
                //wedcxzaq ufhrytjn lvog
                case 'u':
                    break;
                case 'f':
                    break;
                case 'h':
                    break;
                case 'r':
                    break;
                case 'y':
                    break;
                case 't':
                    break;
                case 'j':
                    break;
                case 'n':
                    break;
                //wedcxzaq ufhrytjn lvog
                case 'l':
                    break;
                case 'v':
                    break;
                case 'o':
                    break;
                case 'g':
                    break;

                default:
                    break;
            }
        }
    }
}
