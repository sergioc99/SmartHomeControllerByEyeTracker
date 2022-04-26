import asyncio
import logging
import time
from fastapi import FastAPI

from xknx.devices import Switch
from xknx import XKNX
from xknx.devices import Sensor
from xknx.devices import Light
from xknx.io import ConnectionConfig, ConnectionType

app = FastAPI()

@app.get("/prueba")
async def prueba():
    """Connect to specific tunnelling server, time a request for a group address value."""
    connection_config = ConnectionConfig(
        connection_type=ConnectionType.TUNNELING,
        gateway_ip="192.168.7.206",
        gateway_port=3671,
        local_ip="192.168.7.12",
        local_port=0,
        route_back=True,
    )
    xknx = XKNX(connection_config=connection_config)

    light = Light(xknx, name="TestLight", group_address_switch="1/1/1", group_address_switch_state="1/1/1")
    await light.set_on()
    await asyncio.sleep(2)
    await light.set_off()
    await light.switch.on()
    print(light.switch)
    return