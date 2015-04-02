//Based on sketch from http://arduino.cc/en/reference/random
//and sketch from http://arduino.cc/en/Tutorial/BlinkWithoutDelay

//Delay
bool sendState = LOW;
unsigned long previousMillis = 0;
const long interval = 1000;           // interval at which to blink (milliseconds)

//Random
long randNumber;

void setup(){
	Serial.begin(9600);

	// if analog input pin 0 is unconnected, random analog
	// noise will cause the call to randomSeed() to generate
	// different seed numbers each time the sketch runs.
	// randomSeed() will then shuffle the random function.
	randomSeed(analogRead(0));
}

void loop() {
 

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
