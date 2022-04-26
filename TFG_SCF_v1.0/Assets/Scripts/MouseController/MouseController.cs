using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {
    public int sizeCursor = 32;
    public Texture2D normalCursor;
    Texture2D activeCursor;

    void Start()
    {
        Cursor.visible = false;
        changeCursor("normal");
    }

    public void changeCursor(string typeCursor)
    {
        if (typeCursor == "normal")
        {
            activeCursor = normalCursor;
        }
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, sizeCursor, sizeCursor), activeCursor);
    }
}