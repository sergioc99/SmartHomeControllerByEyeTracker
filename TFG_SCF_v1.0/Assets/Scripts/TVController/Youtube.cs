using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;

public class Youtube : MonoBehaviour
{
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
        {
            runYoutube();
        }
    }

    public void runYoutube()
    {

        string sURL = "http://localhost:8000/service/tv/youtube";

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
