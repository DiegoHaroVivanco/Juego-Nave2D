using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controlador : MonoBehaviour
{
    float minX, minY,maxX, maxY;
    //Este script se encargara de controlar el respawn de los enemigos, los timers, etc...
    public GameObject enemigo;
    float TimerRespawn;
    float timer1;
    int minutos;
    public float TiempoDeRespawn;

    [Header("GUI Texts")]
    public Text AtaqueEspecial;
    public Text AtaqueNormal;
    public Text txtTiempo;
    public Text score;
    public Text vida;
    public Text Nombre;



    GameObject player;
    Disparar disp;
    StatsPlayer sPlayer;

    private void Start()
    {
        minutos = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        sPlayer = player.GetComponent<StatsPlayer>();
        disp = player.GetComponent<Disparar>();
    }

    private void Update()
    {
        timer1 += Time.deltaTime;
        int vidaAux = sPlayer.vida;
        int scoreAux = sPlayer.score;
        string nameAux = sPlayer.Nombre;
       // vida.text = "VIDA: <b><size=10><color=red>" + vidaAux.ToString() + "</color></size></b><b><size=10><color=white>/</color></size></b>" + "<b><size=10><color=red>3</color></size></b>";
        vida.text = "VIDA: <b><size=10><color=red>" + vidaAux.ToString() + "/3</color></size></b>";
        score.text = "SCORE: <b><size=10><color=orange>" + scoreAux.ToString() + "</color></size></b>";
        Nombre.text = "USERNAME: <b><size=10><color=orange>" + nameAux.ToString() + "</color></size></b>";


        if (TimerRespawn <= 0.1f)
        {
            RespawnEnemigo();
            TimerRespawn = TiempoDeRespawn;
        }
        else
        {
            TimerRespawn -= Time.deltaTime;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            RespawnEnemigo();
        }

        float aux = disp.tiempoDisparo2;
        int aux2 = ((int) disp.tiempoDisparo2) + 1;
   

        if(aux < disp.tiempoRetardo2)
        {
            AtaqueEspecial.text = "ATAQUE ESPECIAL: EN <b><size=8><color=orange>" + aux2.ToString()+ "</color></size></b> ..." ;
        }
        else
        {
            AtaqueEspecial.text = "ATAQUE ESPECIAL:<b><size=8><color=orange> LISTO! </color></size></b>";
        }

        float auxx = disp.tiempoDisparo;
        int auxx2 = ((int)disp.tiempoDisparo) + 1;

        if (auxx < disp.tiempoRetardo)
        {
            if(disp.tiempoRetardo > 0.5f)
            {
                AtaqueNormal.text = "ATAQUE NORMAL: EN <b><size=8><color=black>" + auxx2.ToString() + "</color></size></b> ...";
            }
            else
            {
                AtaqueNormal.text = "ATAQUE NORMAL: <b><size=8><color=white> LISTO! </color></size></b>";
            }

        }
        else
        {
            AtaqueNormal.text = "ATAQUE NORMAL: <b><size=8><color=white> LISTO! </color></size></b>";
        }

        ControladorTiempoTexto();

    }

    private void ControladorTiempoTexto()
    {
        if (Mathf.Round(timer1) >= 60) //Si llega a 60
        {
            timer1 = 0; //se tiene que reiniciar a 0 
            minutos = minutos + 1; //sumar 1 minuto
        }

        if (minutos == 0)
        {
            if (Mathf.Round(timer1) < 10)
            {
                txtTiempo.text = "<b><size=8><color=white> 00:0" + Mathf.Round(timer1).ToString() + "</color></size></b>";
            }
            else
            {
                txtTiempo.text = "<b><size=8><color=white> 00:" + Mathf.Round(timer1).ToString() + "</color></size></b>";
            }

        }
        else
        {
            if (Mathf.Round(timer1) < 10)
            {
                txtTiempo.text = "<b><size=8><color=white> 0" + minutos + ":0" + Mathf.Round(timer1).ToString() + "</color></size></b>";
            }
            else
            {
                txtTiempo.text = "<b><size=8><color=white> 0" + minutos + ":" + Mathf.Round(timer1).ToString() + "</color></size></b>";
            }
        }
    }

    public void RespawnEnemigo()
    {
       int randomSpawn = Random.Range(0, 4);
        float randomX;
        float randomY;


        switch (randomSpawn)
        {
            case 0:
                minX = -10;
                maxX = 10;
                minY = 8;
                maxY = 12;
          
                 randomX = Random.Range(minX, maxX);
                 randomY = Random.Range(minY, maxY);
                break;

            case 1:
                minX = -10;
                maxX = 10;
                minY = -8;
                maxY = -12;

                randomX = Random.Range(minX, maxX);
                 randomY = Random.Range(minY, maxY);
                break;

            case 2:
                minX = -16;
                maxX = -12;
                minY = -6;
                maxY = 6;

                randomX = Random.Range(minX, maxX);
                 randomY = Random.Range(minY, maxY);
                break;

            default:
                minX = 12;
                maxX = 16;
                minY = -6;
                maxY = +6;

                randomX = Random.Range(minX, maxX);
                randomY = Random.Range(minY, maxY);
                break;
        }

        Vector3 randomPos = new Vector3(randomX, randomY, 0);

        Instantiate(enemigo, randomPos, Quaternion.identity);
    }
}
