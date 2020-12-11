//1- Les Librairies
#include <Stepper.h>
#include <Uduino.h>

//2- Nombre de cran pour le steppper
const int stepsPerRevolution = 260;

Stepper myStepper(stepPerRevolution, 25, 26, 27, 28);

//3- On initialise le tout
void setup() {

  Serial.begin(9600);
  myStepper.setSpeed(100);

  //3.1- Ajout des commande Uduino
  uduino.addCommand("increaseOne", increaseOneStep);
  uduino.addCommand("decreaseOne", decreaseOneStep);

}

//4- Ajout de la commande d'augmentation
void increaseOneStep() {

  myStepper.step(stepsPerRevolution);
  
}

//4- Ajout de la commande d'augmentation
void increaseOneStep() {

  myStepper.step(-stepsPerRevolution);
  
}

void loop() {
  // put your main code here, to run repeatedly:

}
