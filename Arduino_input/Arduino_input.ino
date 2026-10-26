int INPUT_PIN = 13;
int pedal_counter = 0;
int previous_bike_input;

void setup() {
  pinMode(INPUT_PIN, INPUT);
  Serial.begin(9600, SERIAL_8N1);

  previous_bike_input = digitalRead(INPUT_PIN);
}

void loop() {
  // put your main code here, to run repeatedly:
  int bike_input = digitalRead(INPUT_PIN);

  // TODO check if debouncing required
  // TODO support backwards pedal
  if(bike_input != previous_bike_input){
    pedal_counter++;
    Serial.println(pedal_counter);
    previous_bike_input = bike_input;
  }
}
