using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public int velocidad;

    private void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Cursor.visible = false;
        transform.position = Input.mousePosition;
        rotar();
    }

    void rotar()
    {
        transform.Rotate(new Vector3(0, 0, 1) * velocidad * Time.deltaTime);
    }
}
