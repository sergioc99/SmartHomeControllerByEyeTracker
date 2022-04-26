using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation4 : MonoBehaviour
{
    public Transform[] views;
    public Transform currentView;
    public Camera camera;
    public GameObject[] newItemsView0;
    public GameObject[] newItemsView3;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
        {
            if (camera.transform.position + "" == views[3].position + "")
            {
                currentView.position = views[0].position;
                currentView.eulerAngles = views[0].eulerAngles;
                foreach (GameObject gameObject in newItemsView0)
                {
                    gameObject.SetActive(true);
                }

            }
            else if (camera.transform.position + "" == views[0].position + "")
            {
                currentView.position = views[3].position;
                currentView.eulerAngles = views[3].eulerAngles;
                foreach (GameObject gameObject in newItemsView3)
                {
                    gameObject.SetActive(true);
                }
            }
        }
    }
}
