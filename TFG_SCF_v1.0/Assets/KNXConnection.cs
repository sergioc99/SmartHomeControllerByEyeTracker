using UnityEngine;
using System.Net;

public class KNXConnection : MonoBehaviour
{
    void NonStart()
    {
        string sURL;
        sURL = "http://localhost:8000/my-first-api";

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

    // Update is called once  per frame
    void Update()
    {

    }
}
    