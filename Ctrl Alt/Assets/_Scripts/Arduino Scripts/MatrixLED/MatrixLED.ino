// 1.1- Ajout de la librairie Adafruit GFX
#include <Adafruit_GFX.h>
#include <Adafruit_GrayOLED.h>
#include <Adafruit_SPITFT.h>
#include <Adafruit_SPITFT_Macros.h>
#include <gfxfont.h>

//1.2- Ajout de la librairie Adafruit BuIO
#include <Adafruit_BusIO_Register.h>
#include <Adafruit_I2CDevice.h>
#include <Adafruit_I2CRegister.h>
#include <Adafruit_SPIDevice.h>

//1.3- Ajout de la librairie RGB Matrix Panel
#include <gamma.h>
#include <RGBmatrixPanel.h>

//1.4- Ajout de la librairie Uduino
#include<Uduino.h>

//1.5- Initialisation de la board Uduino
Uduino uduino("BoardMega");

//1.6- Definition des PINs
#define CLK 11 
#define OE   9
#define LAT 10
#define A   A0
#define B   A1
#define C   A2
#define D   A3

RGBmatrixPanel matrix(A, B, C, D, CLK, LAT, OE, false);

//2- Démarrage
void setup() {

  //On démarre le tout
  Serial.begin(9600);
  matrix.begin();

  //On créer les fonction Arduino pour les appeller depuis Unity
  uduino.addCommand("turnPixelOn",turnPixelOn);
  uduino.addCommand("turnPixelOff",turnPixelOff);
  
}

//3- Fonction pour allumer UN SEUL pixel
void turnPixelOn (){

  //3.1- Recuperation des paramètres
  int y = atoi(uduino.getParametre(0));
  int x = atoi(uduino.getParametre(1));
  int gOriginal = atoi(uduino.getParametre(2));
  int rOriginal = atoi(uduino.getParametre(3));
  int bOriginal = atoi(uduino.getParametre(4));

  //3.2- Changement d'échelle des couleurs
  g = map(gOriginal, 0, 255, 0, 7);
  r = map(gOriginal, 0, 255, 0, 7);
  b = map(gOriginal, 0, 255, 0, 7);

  //3.3- Affichage du Pixel
  matrix.drawPixel(x, y, matrix.Color333(r, g, b));
  
}

//4- FOnction pour étindre UN SEUL pixel
void turnPixelOff (){

  //4.1- Recuperation des paramètres
  int y = atoi(uduino.getParametre(0));
  int x = atoi(uduino.getParametre(1));

  //3.2- Extinction du pixel
  matrix.drawPixel(x, y, matrix.Color333(0, 0, 0));
  
}
