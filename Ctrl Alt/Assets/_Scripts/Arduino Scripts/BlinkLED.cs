using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class BlinkLED : MonoBehaviour
{

    public int pinNumber; //numéro du pin de la LED

    void Start()
    {

        UduinoManager.Instance.pinMode(pinNumber, PinMode.Output); //fait en sorte que la Pin soit un Output et pas un Input

    }

    IEnumerator TurnOnLED () //Coroutine pour allumer une LED
    {
        
        UduinoManager.Instance.digitalWrite(pinNumber, State.HIGH);
        yield return new WaitForSeconds(0.1f);

    }

    IEnumerator TurnOffLED() //Coroutine pour éteindre une LED
    {

        UduinoManager.Instance.digitalWrite(pinNumber, State.LOW);
        yield return new WaitForSeconds(0.1f);

    }
}
