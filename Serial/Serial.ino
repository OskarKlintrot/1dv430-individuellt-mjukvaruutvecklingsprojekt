//Based on sketches from http://arduino.cc/en/reference/random,
//http://arduino.cc/en/Tutorial/BlinkWithoutDelay and http://arduino.cc/en/Serial/read

// for incoming serial data
String incomingSerial;

//Delay
bool sendState = LOW;
unsigned long previousMillis = 0;
const long interval = 1000;           // interval at which to blink (milliseconds)

//Random
long randNumber;

void setup(){
  
  Serial.begin(9600);
  
  //if analog input pin 0 is unconnected, random analog
  //noise will cause the call to randomSeed() to generate
  //different seed numbers each time the sketch runs.
  //randomSeed() will then shuffle the random function.
  randomSeed(analogRead(0));

  // initialize digital pin 13 as an output.
  pinMode(13, OUTPUT);
  pinMode(2, OUTPUT);
  pinMode(3, OUTPUT);
  pinMode(4, OUTPUT);
  pinMode(5, OUTPUT);
  pinMode(6, OUTPUT);
  pinMode(7, OUTPUT);
  pinMode(8, OUTPUT);
}

void loop() {

  // send data only when you receive data:
  if (Serial.available() > 0) {
    incomingSerial = Serial.readStringUntil('#');
    
    incomingSerial.trim();
  
    Serial.print("I received: ");
    Serial.println(incomingSerial);
    
    // actions based on incomming serial
    if (incomingSerial == "13H")
        digitalWrite(13, HIGH);
    else if (incomingSerial == "13L")
        digitalWrite(13, LOW);
        
    if (incomingSerial == "1H")
        digitalWrite(2, HIGH);
    else if (incomingSerial == "1L")
        digitalWrite(2, LOW);
        
    if (incomingSerial == "2H")
        digitalWrite(3, HIGH);
    else if (incomingSerial == "2L")
        digitalWrite(3, LOW);

    if (incomingSerial == "3H")
        digitalWrite(4, HIGH);
    else if (incomingSerial == "3L")
        digitalWrite(4, LOW);

    if (incomingSerial == "4H")
        digitalWrite(5, HIGH);
    else if (incomingSerial == "4L")
        digitalWrite(5, LOW);

    if (incomingSerial == "5H")
        digitalWrite(6, HIGH);
    else if (incomingSerial == "5L")
        digitalWrite(6, LOW);

    if (incomingSerial == "6H")
        digitalWrite(7, HIGH);
    else if (incomingSerial == "6L")
        digitalWrite(7, LOW);

    if (incomingSerial == "7H")
        digitalWrite(8, HIGH);
    else if (incomingSerial == "7L")
        digitalWrite(8, LOW);
  }
 

  // delay without delay
  unsigned long currentMillis = millis();
  if(currentMillis - previousMillis > interval) {
    previousMillis = currentMillis;   

    if (sendState == LOW)
      sendState = HIGH;
    else
      sendState = LOW;

    // print a random number from 17 to 24
     randNumber = random(17, 25);
     
     //Send the number via serial to pc
     Serial.println(randNumber);
  }
}
