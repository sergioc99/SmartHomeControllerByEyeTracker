using System.Net;
using UnityEngine;


public class ConnectionStartTV : MonoBehaviour
{
    public GameObject InputBox;
    void Start()
    {
        InputBox.SetActive(false);
        //We launch the first connection so that the pin appears, before we must assign the ip of the tv.
        string sURL;
        sURL = "http://localhost:8000/connect/tv/step1";
        WebRequest wrGETURL;
        wrGETURL = WebRequest.Create(sURL);

        System.IO.Stream objStream;
        objStream = wrGETURL.GetResponse().GetResponseStream();

        System.IO.StreamReader objReader = new System.IO.StreamReader(objStream);

        string sLine = "";
        while (sLine != null)
        {
            sLine = objReader.ReadLine();
            if (sLine != null)
                Debug.Log(sLine);
        }

        
        sURL = "http://localhost:8000/service/status/tv";
        wrGETURL = WebRequest.Create(sURL);
        objStream = wrGETURL.GetResponse().GetResponseStream();
        objReader = new System.IO.StreamReader(objStream);

        sLine = "";
        while (sLine != null)
        {
            sLine = objReader.ReadLine();
            if (sLine != null)
            {
                if (!sLine.Equals("\"TV is already connected\""))
                {
                    InputBox.SetActive(true);
                }
            }
        }
    }

    public void secondStep(string pin)
    {
        //Let's get the pin and make the connection.
        string sURL = "http://localhost:8000/connect/tv/step2?PIN="+pin;

        WebRequest wrGETURL;
        wrGETURL = WebRequest.Create(sURL);

        System.IO.Stream objStream;
        objStream = wrGETURL.GetResponse().GetResponseStream();

        System.IO.StreamReader objReader = new System.IO.StreamReader(objStream);

        string sLine = "";
        while (sLine != null)
        {
            sLine = objReader.ReadLine();
            if (sLine != null)
                Debug.Log(sLine);
        }
        InputBox.SetActive(false);
    }
}
