using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPlayer : MonoBehaviour
{
    //Este script se encargara de controlar todos los datos del jugador


    /*Vida
     *Cantidad de proyectiles
     *Score
     *Cantidad de enemigos destruidos
     *Tiempo que lleva con vida 
     * */
    SpriteRenderer back;
    public Color pantallaDorada;
    Color ColorInicial;

    GameObject camera;
    Camera cam;
    Color colorInicialCam;
    SpriteRenderer sr;

    public GameObject Cabeza;
    public int vida = 3;
    public int score = 0;
    public string Nombre;

    float timerGolpeado = 1f;
    bool EnemigoGolpeo = false;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        cam = camera.GetComponent<Camera>();
        colorInicialCam = cam.backgroundColor;
        back = Cabeza.GetComponent<SpriteRenderer>();
        ColorInicial = back.color;
    }

    public void restarVida()
    {
        vida--;

    }

    public void sumarScore()
    {
        score++;
    }

    private void Update()
    {
        CheckProyectil2();
    }


    void CheckProyectil2()
    {
        GameObject proy2 = GameObject.FindGameObjectWithTag("Proyectil2");
        GameObject[] proys2 = GameObject.FindGameObjectsWithTag("Proyectil2");


        cam.backgroundColor = Color.yellow;

        if (proys2.Length > 0)
        {
            back.color = Color.yellow;
            cam.backgroundColor = pantallaDorada;
        }
        else
        {
            back.color = ColorInicial;
            cam.backgroundColor = colorInicialCam;
        }
    }
}
