using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparar : MonoBehaviour
{
    //Este script se encargara de Disparar un proyectil

    public GameObject proyect;
    public AudioClip Explosion = null;
    public GameObject proyect2;
    public AudioClip Explosion2 = null;
    public float volumenDisparo = 3;
    public float tiempoDisparo;
    public float tiempoRetardo;
    public float tiempoDisparo2;
    public float tiempoRetardo2;
    bool puedeDisparar = false;
    bool DisparoProyectil2 = false;

    private void Start()
    {
        tiempoDisparo2 = tiempoRetardo2;
    }

    private void Update()
    {

          if (tiempoDisparo <= 0.1f)
          {
              puedeDisparar = true;
              tiempoDisparo = tiempoRetardo;
          }
          else
          {
             if(puedeDisparar == false)
             {
                 tiempoDisparo -= Time.deltaTime;
             }
          }


        if (tiempoDisparo2 <= 0.1f)
        {
            if (DisparoProyectil2 == true)
            {
                tiempoDisparo2 = tiempoRetardo2;
                DisparoProyectil2 = false;
            }
        }
        else
        {
            if (DisparoProyectil2 == true)
            {
                tiempoDisparo2 -= Time.deltaTime;
            }
        }

        if (puedeDisparar == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(proyect, transform.position, transform.rotation);
                AudioSource.PlayClipAtPoint(Explosion, transform.position, volumenDisparo);
                puedeDisparar = false;
            }
        }

        if (DisparoProyectil2 == false)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Instantiate(proyect2, transform.position, transform.rotation);
                AudioSource.PlayClipAtPoint(Explosion2, transform.position, volumenDisparo);
                DisparoProyectil2 = true;
            }

        }

    }
}

