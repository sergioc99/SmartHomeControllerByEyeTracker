using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class BlindRequestInfo : MonoBehaviour
{
    public List<GameObject> blinds;
    public List<GameObject> cubes;
    private float time = 0;
    private void Update()
    {
        if (time <= 0)
        {
            int realPercent = -10;
            time = 300;
            string sURL = "http://localhost:8000/service/status/persiana";

            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(sURL);

            System.IO.Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();

            System.IO.StreamReader objReader = new System.IO.StreamReader(objStream);

            string sLine = "";
            int i = 0;

            while (sLine != null)
            {
                i++;
                sLine = objReader.ReadLine();
                if (sLine != null)
                {
                    realPercent = int.Parse(sLine);
                }

            }
            
            Debug.Log(realPercent);
            if (realPercent <= 100 && realPercent >= 0)
            {
                for (i = 0; i < 5; i++)
                {
                    blinds[i].active = false;
                    cubes[i].active = false;
                }
            }

            if (realPercent <= 20 && realPercent > 0)
            {
                blinds[0].active = true;
                cubes[0].active = true;
            }
            else if (realPercent <= 40 && realPercent > 21)
            {
                blinds[1].active = true;
                cubes[1].active = true;
            }
            else if (realPercent <= 60 && realPercent > 41)
            {
                blinds[2].active = true;
                cubes[2].active = true;
            }
            else if (realPercent <= 80 && realPercent > 61)
            {
                blinds[3].active = true;
                cubes[3].active = true;
            }
            else if (realPercent <= 100 && realPercent > 81)
            {
                blinds[4].active = true;
                cubes[4].active = true;
            }

        }
        else
        {
            time -= Time.deltaTime;
        }
    }
}
