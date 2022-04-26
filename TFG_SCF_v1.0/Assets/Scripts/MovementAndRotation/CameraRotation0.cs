using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation0 : MonoBehaviour
{
    public Transform[] views;
    public Transform currentView;
    public Camera camera;
    public GameObject[] newItemsView5;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
        {
            if (camera.transform.position + "" == views[1].position + "")
            {
                currentView.position = views[5].position;
                currentView.eulerAngles = views[5].eulerAngles;
                foreach (GameObject gameObject in newItemsView5)
                {
                    gameObject.SetActive(true);
                }
            }
        }
    }
}
