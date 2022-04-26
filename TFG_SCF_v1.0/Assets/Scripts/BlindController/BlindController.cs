using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;
using System.Net;

public class BlindController : MonoBehaviour
{
    public GameObject nextBlind;
    public GameObject nextCube;
    public GameObject previousBlind;
    public GameObject previousCube;
    public int percent;
    public GameObject currentBlind;
    public GameObject currentCube;

    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0)){
            if (percent != 90)
            {
                changeStatusBlindRaise();
            }
        }else if (Input.GetMouseButtonDown(1))
        {
            if (percent != 10)
            {
                changeStatusBlindPD();
            }
        }
    }

    public void changeStatusBlindRaise()
    {
        currentBlind.SetActive(false);
        currentCube.SetActive(false);
        nextBlind.SetActive(true);
        nextCube.SetActive(true);
        string sURL = "http://localhost:8000/service/persiana?persianaPorcentaje=" + (percent+20);

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

    public void changeStatusBlindPD()
    {
        currentBlind.SetActive(false);
        currentCube.SetActive(false);
        previousBlind.SetActive(true);
        previousCube.SetActive(true);
        string sURL = "http://localhost:8000/service/persiana?persianaPorcentaje=" + (percent-20);

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
