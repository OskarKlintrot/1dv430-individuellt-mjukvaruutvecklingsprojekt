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
}

void loop() {
  // send data only when you receive data:
  if (Serial.available() > 0) {
    incomingSerial = Serial.readStringUntil('#');
    
    incomingSerial.trim();
  
    Serial.print("I received: ");
    Serial.println(incomingSerial);
    
    if (incomingSerial == "HIGH")
        digitalWrite(13, HIGH);
    else if (incomingSerial == "LOW")
        digitalWrite(13, LOW);
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
