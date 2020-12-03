using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class MatrixControler : MonoBehaviour
{

    //1- Création des différentes variables
    public int pixelGreen;
    public int pixelRed;
    public int pixelBlue;

    public int pixelY;
    public int pixelX;


    //2- Création d'une coroutine pour changer la couleur d'un Pixel
    IEnumerator PixelColorSwap()
    {

        UduinoManager.Instance.sendCommand("turnPixelOn", pixelY, pixelX, pixelGreen, pixelRed, pixelBlue);
        yield return new WaitForSeconds(0.1f);

    }


    //3- Création d'une coroutine pour changer la couleur d'un pixel en noire ou aussi l'éteindre
    IEnumerator PixelTurnBlack()
    {

        UduinoManager.Instance.sendCommand("turnPixelOff", pixelY, pixelX);
        yield return new WaitForSeconds(0.1f);

    }
}
