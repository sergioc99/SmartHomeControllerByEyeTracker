from fastapi import FastAPI
import json
import asyncio
import requests
from bravia_tv import BraviaRC

app = FastAPI()
braviarc = ""
#IP TV
ip_address = '192.168.7.228'

@app.get("/connect/tv/step1")
async def conectarTV_step1():
    global braviarc
    global ip_address
    braviarc = BraviaRC(ip_address)

    braviarc.connect('0000', 'my_device_id', 'my device name')
    return "Connected step 1"

@app.get("/connect/tv/step2")
async def conectarTV_step2(PIN:str = ""):
    global braviarc
    global ip_address
    braviarc = BraviaRC(ip_address)

    braviarc.connect(PIN, 'my_device_id', 'my device name')
    return "Connected step 2"


@app.get("/service/luces")
async def luces(luz:str = "1/1/1", switch:str = "false"):
    params = {'m': 'json',
          'r': 'grp',
          'fn': 'write',
          'alias': luz,
          'value': switch}
    url = 'http://remote:LabSmarthome21@192.168.7.206/scada-remote'
    r = requests.get(url=url, params=params)
    
    return r.json()
   
# Find te devuelve mucha información del objeto


@app.get("/service/persiana")
async def persiana(persianaPorcentaje:float = 50):
    params = {'m': 'json',
          'r': 'grp',
          'fn': 'write',
          'alias': '2/3/4',
          'value': persianaPorcentaje}
    url = 'http://remote:LabSmarthome21@192.168.7.206/scada-remote'
    r = requests.get(url=url, params=params)
    
    return r.json()

    


@app.get("/service/tv/turnon")
async def tvOn():
    global braviarc
    if braviarc.is_connected():
        braviarc.turn_on()
    return 
    
@app.get("/service/tv/turnoff")
async def tvOff():
    global braviarc
    if braviarc.is_connected():
        braviarc.turn_off()
    return 

@app.get("/service/tv/netflix")
async def tvNetflix():
    global braviarc
    if braviarc.is_connected():
        braviarc.start_app("Netflix")
    return
    
    
@app.get("/service/tv/youtube")
async def tvYoutube():
    global braviarc
    if braviarc.is_connected():
        braviarc.start_app("YouTube")
    return

    
@app.get("/service/tv/volume/up")
async def tvVolumeUp():
    global braviarc
    if braviarc.is_connected():
        volume = braviarc.get_volume_info()
        volume_final = volume.get('volume')
        if(volume.get('volume') <= 95):
            volume_final = volume.get('volume')/100 + 0.05
        braviarc.set_volume_level(volume_final)
    return
    
    
@app.get("/service/tv/volume/down")
async def tvVolumeUp():
    global braviarc
    if braviarc.is_connected():
        volume = braviarc.get_volume_info()
        volume_final = volume.get('volume')
        if(volume.get('volume') >= 5):
            volume_final = volume.get('volume')/100 - 0.05
        braviarc.set_volume_level(volume_final)
        volume = braviarc.get_volume_info()
    return



"""ACTUALIZACION DE ESTADO"""


@app.get("/service/status/tv")
async def tvOn():
    global braviarc
    if braviarc.is_connected():
        return "TV is already connected"
    return false

@app.get("/service/status/luces")
async def getStatusLuces(luz:str = "1/1/1"):   
    params = {'m': 'json',
          'r': 'grp',
          'fn': 'getvalue',
          'alias': luz}
    url = 'http://remote:LabSmarthome21@192.168.7.206/scada-remote'
    r = requests.get(url=url, params=params)
    
    return r.json()

@app.get("/service/status/persiana")
async def getStatusPersiana():   
    params = {'m': 'json',
          'r': 'grp',
          'fn': 'getvalue',
          'alias': '2/3/4'}
    url = 'http://remote:LabSmarthome21@192.168.7.206/scada-remote'
    r = requests.get(url=url, params=params)
    
    return r.json()

