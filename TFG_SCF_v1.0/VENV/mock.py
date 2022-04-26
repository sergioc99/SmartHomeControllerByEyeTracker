from fastapi import FastAPI
import json
import asyncio
import requests
from bravia_tv import BraviaRC

app = FastAPI()

@app.get("/connect/tv/step1")
async def conectarTV_step1():
    return "Connected step 1"

@app.get("/connect/tv/step2")
async def conectarTV_step2(PIN:str = ""):
    return "Connected step 2"


@app.get("/service/luces")
async def luces(luz:str = "1/1/1", switch:str = "false"):
    return ""
   
# Find te devuelve mucha información del objeto


@app.get("/service/persiana")
async def persiana(persianaPorcentaje:float = 50):
    return ""

    


@app.get("/service/tv/turnon")
async def tvOn():
    return ""
    
@app.get("/service/tv/turnoff")
async def tvOff():
    return "" 

@app.get("/service/tv/netflix")
async def tvNetflix():
    return ""
    
    
@app.get("/service/tv/youtube")
async def tvYoutube():
    return ""

    
@app.get("/service/tv/volume/up")
async def tvVolumeUp():
    return ""
    
    
@app.get("/service/tv/volume/down")
async def tvVolumeUp():
    return ""



"""ACTUALIZACION DE ESTADO"""


@app.get("/service/status/tv")
async def tvOn():
    return "TV is already connected"
    

@app.get("/service/status/luces")
async def getStatusLuces(luz:str = "1/1/1"):   
    return ""

@app.get("/service/status/persiana")
async def getStatusPersiana():   
    return ""

