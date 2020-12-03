//1- Inclusion de la librairie Uduino
#include <Uduino.h>

//2- Definition des Pins
const int pinAspiration = 50;
const int pinSouffle = 51;

void setup() {
  Serial.begin(9600);
  pinMode(pinAspiration, INPUT);
  pinMode(pinSouffle, INPUT);

}

void loop() {
  // put your main code here, to run repeatedly:

}
