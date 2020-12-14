using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class ReadSouffleur : MonoBehaviour
{

    //1- Variables
    public int soufflePin;
    public int aspirationPin;
    public bool souffle = false;
    public bool aspiration = false;
    
    //2- Initialisation des pins
    void Start()
    {

        UduinoManager.Instance.pinMode(soufflePin, PinMode.Input);
        UduinoManager.Instance.pinMode(aspirationPin, PinMode.Input);

    }

    //3- Varification de l'état du souffleur
    void Update()
    {

        if (UduinoManager.Instance.digitalRead(soufflePin) > 0)
        {

            souffle = true;

        }else if (UduinoManager.Instance.digitalRead(aspirationPin) > 0)
        {

            aspiration = true;

        }else if (UduinoManager.Instance.digitalRead(soufflePin) <= 0)
        {

            souffle = false;

        }
        else if (UduinoManager.Instance.digitalRead(aspirationPin) <= 0)
        {

            aspiration = false;

        }

        GameEvents.Instance.SouffleurExhaled(souffle);
    }

}
