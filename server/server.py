import uvicorn
import os
from fastapi import FastAPI
import time
import serial
import asyncio


run_host = "0.0.0.0"
run_port = 7884
max_level = 3
current_level = 0
current_position = 0
api_token = "test-token"
serial_post = "COM5"
baudrate = 9600
delay_time = 0.8

ser = serial.Serial(serial_post, baudrate, timeout=0.1)
app = FastAPI()

is_running = False

def push_level():
	global current_level
	global current_position

	ser.write(str(current_position).encode())
	current_position = (current_position + 1) % 2


@app.get("/set-lamp-level")
async def set_lamp_level(level: int, token: str):
	global current_level
	global max_level
	global is_running
	global api_token

	if token != api_token:
		return {"status": "error", "message": "invalid token"}

	while is_running:
		await asyncio.sleep(0.1)

	ser.read_all()

	is_running = True
	if level > max_level:
		is_running = False
		return {"message": "level too high", "status": "error"}
	if level < 0:
		is_running = False
		return {"message": "level too low", "status": "error"}
	if level > current_level:
		push_cnt = level - current_level
	elif level < current_level:
		push_cnt = level + max_level - current_level + 1
	else:
		is_running = False
		return {"message": "None", "status": "done"}
	print('push_cnt:', push_cnt)
	for i in range(push_cnt):
		push_level()
		await asyncio.sleep(delay_time)
		print('pushed:', i + 1, '/', push_cnt)
	current_level = level
	is_running = False
	return {"message": "None", "status": "done"}


@app.get("/set-current-level")
async def set_current_level(level: int, token: str):
	global current_level

	current_level = level
	return {"message": "None", "status": "done"}


@app.get("/set-current-position")
async def set_current_level(position: int, token: str):
	global current_position

	current_position = position
	return {"message": "None", "status": "done"}


if __name__ == "__main__":
	uvicorn.run(app, host=run_host, port=run_port)
