using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class BlinkLED : MonoBehaviour
{


    void Start()
    {

    }

    public void TurnOnLED (pinNumber) //Coroutine pour allumer une LED
    {

        UduinoManager.Instance.pinMode(pinNumber, PinMode.Output);

        UduinoManager.Instance.digitalWrite(pinNumber, State.HIGH);

    }

    public void TurnOffLED() //Coroutine pour éteindre une LED
    {

        UduinoManager.Instance.digitalWrite(pinNumber, State.LOW);

    }
}
