using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class BlinkLED : MonoBehaviour
{
    enum Outputs { Pause, Altitude1, Altitude2, Altitude3, Rappel, Essence1, Essence2, Essence3, ConfirmationAltitude, ConfirmationEssence, ConfirmationVitesse }

    [SerializeField] Outputs myOutput;
    [SerializeField] [Range(0, 53)] private int pinNumber = 0;
    [Header("Optionnel")]
    [SerializeField] [Range(0, 53)] private int pinNumber2 = 0;
    [SerializeField] [Range(0, 53)] private int pinNumber3 = 0;
    [SerializeField] [Range(0, 53)] private int pinNumber4 = 0;
    [SerializeField] [Range(0, 53)] private int pinNumber5 = 0;

    bool LED1on = false;
    bool LED2on = false;
    bool LED3on = false;
    bool LED4on = false;
    bool LED5on = false;


    private void OnEnable()
    {
        SetupLED();
    }

    private void OnDisable()
    {
        DisableLED();
    }

    void SetupLED()
    {
        switch (myOutput)
        {
            case Outputs.Pause:
                GameEvents.Instance.OnTurnOnPause += TurnOnLED;
                GameEvents.Instance.OnTurnOffPause += TurnOffLED;
                break;
            case Outputs.Altitude1:
                GameEvents.Instance.OnTurnOnAltitude1 += TurnOnLED;
                GameEvents.Instance.OnTurnOffAltitude1 += TurnOffLED;
                break;
            case Outputs.Altitude2:
                GameEvents.Instance.OnTurnOnAltitude2 += TurnOnLED;
                GameEvents.Instance.OnTurnOffAltitude2 += TurnOffLED;
                break;
            case Outputs.Altitude3:
                GameEvents.Instance.OnTurnOnAltitude3 += TurnOnLED;
                GameEvents.Instance.OnTurnOffAltitude3 += TurnOffLED;
                break;
            case Outputs.Rappel:
                GameEvents.Instance.OnTurnOnRappel += TurnOnLED;
                GameEvents.Instance.OnTurnOffRappel += TurnOffLED;
                break;
            case Outputs.Essence1:
                GameEvents.Instance.OnTurnOnEssence1 += TurnOnLED;
                GameEvents.Instance.OnTurnOffEssence1 += TurnOffLED;
                break;
            case Outputs.Essence2:
                GameEvents.Instance.OnTurnOnEssence2 += TurnOnLED;
                GameEvents.Instance.OnTurnOffEssence2 += TurnOffLED;
                break;
            case Outputs.Essence3:
                GameEvents.Instance.OnTurnOnEssence3 += TurnOnLED;
                GameEvents.Instance.OnTurnOffEssence3 += TurnOffLED;
                break;
            case Outputs.ConfirmationAltitude:
                GameEvents.Instance.OnTurnOnConfirmationAltitude += TurnOnLED;
                GameEvents.Instance.OnTurnOffConfirmationAltitude += TurnOffLED;
                break;
            case Outputs.ConfirmationEssence:
                GameEvents.Instance.OnTurnOnConfirmationEssence += TurnOnLED;
                GameEvents.Instance.OnTurnOffConfirmationEssence += TurnOffLED;
                break;
            case Outputs.ConfirmationVitesse:
                GameEvents.Instance.OnTurnOnConfirmationEssence += TurnOnLED;
                GameEvents.Instance.OnTurnOffConfirmationEssence += TurnOffLED;
                break;
            default:
                break;
        }
    }

    void DisableLED()
    {
        if (GameEvents.Instance != null)
        {
            switch (myOutput)
            {
                case Outputs.Pause:
                    GameEvents.Instance.OnTurnOnPause -= TurnOnLED;
                    GameEvents.Instance.OnTurnOffPause -= TurnOffLED;
                    break;
                case Outputs.Altitude1:
                    GameEvents.Instance.OnTurnOnAltitude1 -= TurnOnLED;
                    GameEvents.Instance.OnTurnOffAltitude1 -= TurnOffLED;
                    break;
                case Outputs.Altitude2:
                    GameEvents.Instance.OnTurnOnAltitude2 -= TurnOnLED;
                    GameEvents.Instance.OnTurnOffAltitude2 -= TurnOffLED;
                    break;
                case Outputs.Altitude3:
                    GameEvents.Instance.OnTurnOnAltitude3 -= TurnOnLED;
                    GameEvents.Instance.OnTurnOffAltitude3 -= TurnOffLED;
                    break;
                case Outputs.Rappel:
                    GameEvents.Instance.OnTurnOnRappel -= TurnOnLED;
                    GameEvents.Instance.OnTurnOffRappel -= TurnOffLED;
                    break;
                case Outputs.Essence1:
                    GameEvents.Instance.OnTurnOnEssence1 -= TurnOnLED;
                    GameEvents.Instance.OnTurnOffEssence1 -= TurnOffLED;
                    break;
                case Outputs.Essence2:
                    GameEvents.Instance.OnTurnOnEssence2 -= TurnOnLED;
                    GameEvents.Instance.OnTurnOffEssence2 -= TurnOffLED;
                    break;
                case Outputs.Essence3:
                    GameEvents.Instance.OnTurnOnEssence3 -= TurnOnLED;
                    GameEvents.Instance.OnTurnOffEssence3 -= TurnOffLED;
                    break;
                case Outputs.ConfirmationAltitude:
                    GameEvents.Instance.OnTurnOnConfirmationAltitude -= TurnOnLED;
                    GameEvents.Instance.OnTurnOffConfirmationAltitude -= TurnOffLED;
                    break;
                case Outputs.ConfirmationEssence:
                    GameEvents.Instance.OnTurnOnConfirmationEssence -= TurnOnLED;
                    GameEvents.Instance.OnTurnOffConfirmationEssence -= TurnOffLED;
                    break;
                case Outputs.ConfirmationVitesse:
                    GameEvents.Instance.OnTurnOnConfirmationVitesse -= TurnOnLED;
                    GameEvents.Instance.OnTurnOffConfirmationVitesse -= TurnOffLED;
                    break;
                default:
                    break;
            }
        }
    }

    public void TurnOnLED()
    {
        switch (myOutput)
        {
            case Outputs.Pause:
                if (!LED1on)
                {
                    SwitchOnLED(pinNumber);
                    LED1on = true;
                }
                break;
            case Outputs.Altitude1:
                if (!LED1on)
                {
                    SwitchOnLED(pinNumber);
                    LED1on = true;
                }
                break;
            case Outputs.Altitude2:
                if (!LED2on)
                {
                    SwitchOnLED(pinNumber2);
                    LED2on = true;
                }
                break;
            case Outputs.Altitude3:
                if (!LED3on)
                {
                    SwitchOnLED(pinNumber3);
                    LED3on = true;
                }
                break;
            case Outputs.Rappel:
                if (!LED1on)
                {
                    SwitchOnLED(pinNumber);
                    LED1on = true;
                }
                break;
            case Outputs.Essence1:
                if (!LED1on)
                {
                    SwitchOnLED(pinNumber);
                    LED1on = true;
                }
                break;
            case Outputs.Essence2:
                if (!LED2on)
                {
                    SwitchOnLED(pinNumber2);
                    LED2on = true;
                }
                if (!LED3on)
                {
                    SwitchOnLED(pinNumber3);
                    LED3on = true;
                }
                break;
            case Outputs.Essence3:
                if (!LED4on)
                {
                    SwitchOnLED(pinNumber4);
                    LED4on = true;
                }
                if (!LED5on)
                {
                    SwitchOnLED(pinNumber5);
                    LED5on = true;
                }
                break;
            case Outputs.ConfirmationAltitude:
                if (!LED1on)
                {
                    SwitchOnLED(pinNumber);
                    LED1on = true;
                }
                break;
            case Outputs.ConfirmationEssence:
                if (!LED1on)
                {
                    SwitchOnLED(pinNumber);
                    LED1on = true;
                }
                break;
            case Outputs.ConfirmationVitesse:
                if (!LED1on)
                {
                    SwitchOnLED(pinNumber);
                    LED1on = true;
                }
                break;
            default:
                break;
        }
    }
    public void TurnOffLED()
    {
        switch (myOutput)
        {
            case Outputs.Pause:
                if (LED1on)
                {
                    SwitchOffLED(pinNumber);
                    LED1on = false;
                }
                break;
            case Outputs.Altitude1:
                if (LED1on)
                {
                    SwitchOffLED(pinNumber);
                    LED1on = false;
                }
                break;
            case Outputs.Altitude2:
                if (LED2on)
                {
                    SwitchOffLED(pinNumber2);
                    LED2on = false;
                }
                break;
            case Outputs.Altitude3:
                if (LED3on)
                {
                    SwitchOffLED(pinNumber3);
                    LED3on = false;
                }
                break;
            case Outputs.Rappel:
                if (LED1on)
                {
                    SwitchOffLED(pinNumber);
                    LED1on = false;
                }
                break;
            case Outputs.Essence1:
                if (LED1on)
                {
                    SwitchOffLED(pinNumber);
                    LED1on = false;
                }
                break;
            case Outputs.Essence2:
                if (LED2on)
                {
                    SwitchOffLED(pinNumber2);
                    LED2on = false;
                }
                if (!LED3on)
                {
                    SwitchOffLED(pinNumber3);
                    LED3on = false;
                }
                break;
            case Outputs.Essence3:
                if (LED4on)
                {
                    SwitchOffLED(pinNumber4);
                    LED4on = false;
                }
                if (LED5on)
                {
                    SwitchOffLED(pinNumber5);
                    LED5on = false;
                }
                break;
            case Outputs.ConfirmationAltitude:
                if (LED1on)
                {
                    SwitchOffLED(pinNumber);
                    LED1on = false;
                }
                break;
            case Outputs.ConfirmationEssence:
                if (LED1on)
                {
                    SwitchOffLED(pinNumber);
                    LED1on = false;
                }
                break;
            case Outputs.ConfirmationVitesse:
                if (LED1on)
                {
                    SwitchOffLED(pinNumber);
                    LED1on = false;
                }
                break;
            default:
                break;
        }
    }

    public void SwitchOnLED(int pinNumber) //Coroutine pour allumer une LED
    {
        UduinoManager.Instance.pinMode(pinNumber, PinMode.Output);

        UduinoManager.Instance.digitalWrite(pinNumber, State.HIGH);
    }

    public void SwitchOffLED(int pinNumber) //Coroutine pour éteindre une LED
    {

        UduinoManager.Instance.digitalWrite(pinNumber, State.LOW);

    }
}
