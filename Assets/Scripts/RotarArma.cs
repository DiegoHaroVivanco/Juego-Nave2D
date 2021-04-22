using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotarArma : MonoBehaviour
{
    //Este script se encargara de rotar el arma del jugador hacia la posicion donde esta el mouse.
    private void Update()
    {
        Rotar();
    }

    void Rotar()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direc = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        transform.up = direc;
    }
}
