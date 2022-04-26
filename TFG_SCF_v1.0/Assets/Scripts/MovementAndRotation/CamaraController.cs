using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public Transform currentView;
    public float transformSpeed;

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transformSpeed);

        transform.eulerAngles = new Vector3(
            Mathf.Lerp(transform.eulerAngles.x, currentView.eulerAngles.x, Time.deltaTime * transformSpeed),
            Mathf.Lerp(transform.eulerAngles.y, currentView.eulerAngles.y, Time.deltaTime * transformSpeed),
            Mathf.Lerp(transform.eulerAngles.z, currentView.eulerAngles.z, Time.deltaTime * transformSpeed)
            );
    }
}
