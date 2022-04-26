using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;

public class LightController : MonoBehaviour
{
    public List<Light> lightList;
    public Light lamp1;
    public string xknxDevice;
    private float time = 0;

    // Update is called once per frame
    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
        {
            changeStatusLight();
        }
    }

    public void changeStatusLight()
    {
        string sURL = "";
        if (lamp1.enabled) {
            sURL = "http://localhost:8000/service/luces?luz=" + xknxDevice + "&switch=" + "false";
            foreach(Light l in lightList)
            {
                l.enabled = false;
            }
            lamp1.enabled = false;
        }
        else
        {
            sURL = "http://localhost:8000/service/luces?luz=" + xknxDevice + "&switch=" + "true";
            foreach (Light l in lightList)
            {
                l.enabled = true;
            }
            lamp1.enabled = true;
        }


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

    private void Update()
    {
        if (time <= 0)
        {
            time = 300;
            string sURL = "http://localhost:8000/service/status/luces?luz=" + xknxDevice;

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
                    lamp1.enabled = bool.Parse(sLine);
                }
                    
            }

        }
        else
        {
            time -= Time.deltaTime;
        }
    }
}
