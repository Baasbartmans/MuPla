#include "ESP8266WiFi.h"
#include "ESP8266WebServer.h"
 
ESP8266WebServer server(80);

const char *ssid = "Jacguy";
const char *password = "12345678";

int buttonOnePin = 5;
int buttonTwoPin = 4;

String sendback;
char *intStr = "";

//MVC controllers
void ButtonOne() {
  server.send(200, "text/plain", digitalRead(buttonOnePin) == LOW ? "true" : "false");
}

void ButtonTwo() {
  server.send(200, "text/plain", digitalRead(buttonTwoPin) == LOW ? "true" : "false");
}
 
void HandleRootPath() {
  server.send(200, "text/plain", "Page not found");
}
 
void setup() {   
  WiFi.begin(ssid, password);

  //define paths
  server.on("/buttonOne", ButtonOne);
  server.on("/buttonTwo", ButtonTwo);
  server.on("/", HandleRootPath);
  
  server.begin();

  pinMode(buttonOnePin, INPUT);
  pinMode(buttonTwoPin, INPUT);
}
 
void loop() {
  server.handleClient();
}
