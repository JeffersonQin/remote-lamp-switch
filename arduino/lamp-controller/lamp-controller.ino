// orange
int apin = 8;
// yellow
int bpin = 9;
// pink
int cpin = 10;
// blue
int dpin = 11;
int delaytime = 3;

int loopcount = 48;

void anti_clockwise()
{
	digitalWrite(apin, HIGH);
	delay(delaytime);
	digitalWrite(apin, LOW);

	digitalWrite(bpin, HIGH);
	delay(delaytime);
	digitalWrite(bpin, LOW);

	digitalWrite(cpin, HIGH);
	delay(delaytime);
	digitalWrite(cpin, LOW);

	digitalWrite(dpin, HIGH);
	delay(delaytime);
	digitalWrite(dpin, LOW);
}

void clock_wise()
{
	digitalWrite(dpin, HIGH);
	delay(delaytime);
	digitalWrite(dpin, LOW);

	digitalWrite(cpin, HIGH);
	delay(delaytime);
	digitalWrite(cpin, LOW);

	digitalWrite(bpin, HIGH);
	delay(delaytime);
	digitalWrite(bpin, LOW);

	digitalWrite(apin, HIGH);
	delay(delaytime);
	digitalWrite(apin, LOW);
}

void setup()
{
	pinMode(apin, OUTPUT);
	pinMode(bpin, OUTPUT);
	pinMode(cpin, OUTPUT);
	pinMode(dpin, OUTPUT);
	Serial.begin(9600);
	Serial.println("idle");
}

void loop()
{
	if (Serial.available() > 0)
	{
		int in = Serial.read();
		if (in == '0')
		{
			Serial.println("start rotating anti-clockwise.");
			for (int i = 0; i < loopcount; i++)
			{
				anti_clockwise();
				Serial.println(i);
			}
		}
		else if (in == '1')
		{
			Serial.println("start rotating clock-wise.");
			for (int i = 0; i < loopcount; i++)
			{
				clock_wise();
				Serial.println(i);
			}
		}
		Serial.println("done.");
	}
}