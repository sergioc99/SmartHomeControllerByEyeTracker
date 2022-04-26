using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRotation6 : MonoBehaviour
{
    public Transform[] views;
    public Transform currentView;
    public Camera camera;
    public Text leyenda;

    public GameObject[] newItemsView4;

    public GameObject[] disappearedItemsView1;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
        {
            if (camera.transform.position + "" == views[1].position + "")
            {
                currentView.position = views[4].position;
                currentView.eulerAngles = views[4].eulerAngles;
                foreach (GameObject gameObject in newItemsView4)
                {
                    gameObject.SetActive(true);
                }
                leyenda.text = "";

            }
            else if (camera.transform.position + "" == views[4].position + "")
            {
                currentView.position = views[1].position;
                currentView.eulerAngles = views[1].eulerAngles;

                foreach (GameObject gameObject in disappearedItemsView1)
                {
                    gameObject.SetActive(false);
                }
                leyenda.text = "Manten cerrado el ojo derecho para hacer [Click derecho]  para: \n\t· Interactuar con las luces\n\t· Interacturar con movimiento\n\t· Bajar persiana\n\nManten cerrado el ojo izquierdo para hacer[Click izquierdo]  para:\n\t· Subir persiana";
            }
        }
    }
}
