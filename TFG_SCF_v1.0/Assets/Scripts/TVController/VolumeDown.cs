using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;

public class VolumeDown : MonoBehaviour
{
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
        {
            runVolumeDown();
        }
    }

    public void runVolumeDown()
    {
        string sURL = "http://localhost:8000/service/tv/volume/down";

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
                Debug.Log(sLine);
        }
    }
}
