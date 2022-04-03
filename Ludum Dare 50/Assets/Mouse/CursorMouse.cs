using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMouse : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        var cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);   
        transform.position = cursorPos;
    }
}
