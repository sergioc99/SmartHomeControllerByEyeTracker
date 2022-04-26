using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation2 : MonoBehaviour
{
    public Transform[] views;
    public Transform currentView;
    public Camera camera;
    public GameObject[] newItemsView3;
    public GameObject[] disappearedItemsView3;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
        {
            if (camera.transform.position + "" == views[2].position + "")
            {
                currentView.position = views[3].position;
                currentView.eulerAngles = views[3].eulerAngles;
                foreach (GameObject gameObject in newItemsView3)
                {
                    gameObject.SetActive(true);
                }

                foreach (GameObject gameObject in disappearedItemsView3)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
