using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation3 : MonoBehaviour
{
    public Transform[] views;
    public Transform currentView;
    public Camera camera;
    public GameObject[] newItemsView2;
    public GameObject[] disappearedItemsView2;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
        {
            if (camera.transform.position + "" == views[3].position + "")
            {
                currentView.position = views[2].position;
                currentView.eulerAngles = views[2].eulerAngles;
                foreach (GameObject gameObject in newItemsView2)
                {
                    gameObject.SetActive(true);
                }

                foreach (GameObject gameObject in disappearedItemsView2)
                {
                    gameObject.SetActive(false);
                }

            }
        }
    }
}
