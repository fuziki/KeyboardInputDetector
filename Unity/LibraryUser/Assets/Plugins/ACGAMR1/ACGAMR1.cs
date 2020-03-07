using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ACGAMController
{ 
    public class ACGAMR1
    {
        private static Lazy<ACGAMR1> _current = new Lazy<ACGAMR1>(() => new ACGAMR1());

        public static ACGAMR1 current
        {
            get { return _current.Value; }
        }

        public JoyStick joyStick = new JoyStick();
        public Button buttonA = new Button();
        public Button buttonB = new Button();
        public Button buttonC = new Button();
        public Button buttonD = new Button();

        public Button triggerReturn = new Button();
        public Button triggerOk = new Button();

        private KeyboardInputDetector.KeyboardInputDetector detector;

        private ACGAMR1()
        {
            detector = new KeyboardInputDetector.KeyboardInputDetector();
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
                    joyStick.Update(JoyStick.Direction.Up, JoyStick.State.Press);
                    break;
                case 'e':
                    joyStick.Update(JoyStick.Direction.Up, JoyStick.State.Release);
                    break;
                case 'd':
                    joyStick.Update(JoyStick.Direction.Right, JoyStick.State.Press);
                    break;
                case 'c':
                    joyStick.Update(JoyStick.Direction.Right, JoyStick.State.Release);
                    break;
                case 'x':
                    joyStick.Update(JoyStick.Direction.Douwn, JoyStick.State.Press);
                    break;
                case 'z':
                    joyStick.Update(JoyStick.Direction.Douwn, JoyStick.State.Release);
                    break;
                case 'a':
                    joyStick.Update(JoyStick.Direction.Left, JoyStick.State.Press);
                    break;
                case 'q':
                    joyStick.Update(JoyStick.Direction.Left, JoyStick.State.Release);
                    break;
                //wedcxzaq ufhrytjn lvog
                case 'u':
                    buttonA.Update(Button.State.Press);
                    break;
                case 'f':
                    buttonA.Update(Button.State.Release);
                    break;
                case 'h':
                    buttonB.Update(Button.State.Press);
                    break;
                case 'r':
                    buttonB.Update(Button.State.Release);
                    break;
                case 'y':
                    buttonC.Update(Button.State.Press);
                    break;
                case 't':
                    buttonC.Update(Button.State.Release);
                    break;
                case 'j':
                    buttonD.Update(Button.State.Press);
                    break;
                case 'n':
                    buttonD.Update(Button.State.Release);
                    break;
                //wedcxzaq ufhrytjn lvog
                case 'l':
                    triggerReturn.Update(Button.State.Press);
                    break;
                case 'v':
                    triggerReturn.Update(Button.State.Release);
                    break;
                case 'o':
                    triggerOk.Update(Button.State.Press);
                    break;
                case 'g':
                    triggerOk.Update(Button.State.Release);
                    break;

                default:
                    break;
            }
        }
    }
}
