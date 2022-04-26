using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation1 : MonoBehaviour
{
    public Transform[] views;
    public Transform currentView;
    public Camera camera;
    public GameObject[] disappearedItems;

    public GameObject[] newItemsView1;
    public GameObject[] newItemsView2;

    public GameObject[] disappearedItemsView1;

    public GameObject[] disappearedItemsView2;

    private void Start()
    {
        foreach (GameObject gameObject in disappearedItems)
        {
            gameObject.SetActive(false);
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
        {
            if (camera.transform.position + "" == views[1].position + "")
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
            else if (camera.transform.position + "" == views[2].position + "")
            {
                currentView.position = views[1].position;
                currentView.eulerAngles = views[1].eulerAngles;

                foreach (GameObject gameObject in newItemsView1)
                {
                    gameObject.SetActive(true);
                }

                foreach (GameObject gameObject in disappearedItemsView1)
                {
                    gameObject.SetActive(false);
                }

            }
        }
    }
}
