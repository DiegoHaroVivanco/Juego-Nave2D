using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    GameObject player;
    [Range(1, 15)]
    public float velocidad;
    [Range(0.1f,4)]
    public float fuerza;
    Rigidbody2D rb;
    bool golpeado = false;
    public float timerGolpeado = 0.2f;

    public GameObject explosionMuerte;
    public AudioClip Golpe = null;
    public AudioClip Explosion = null;
    public AudioClip MuereXProyectil = null;
    SpriteRenderer sr;
    Color ColorInicial;
    GameObject proy2;
    GameObject[] proys2;
    bool ExisteProyectil = false;
    bool Aturdido = false;
    public int vida;

    private void Start()
    {
        vida = Random.Range(2, 4);
        velocidad = Random.Range(2, 6);
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        ColorInicial = sr.color;
        player = GameObject.FindGameObjectWithTag("Player");
    }

   public void DestruirEnemigo()
    {
        AudioSource.PlayClipAtPoint(MuereXProyectil, transform.position, 3f);
        Destroy(this.gameObject, 0);
        GameObject aux = Instantiate(explosionMuerte, transform.position, transform.rotation);
    }

    void Update()
    {

        try
        {
            proy2 = GameObject.FindGameObjectWithTag("Proyectil2");
        }
        catch
        {
            return;
        }

        CheckProyectil2();

        if (vida <= 0)
        {
            AudioSource.PlayClipAtPoint(MuereXProyectil, transform.position, 3f);
            Destroy(this.gameObject, 0);
            GameObject aux = Instantiate(explosionMuerte, transform.position, transform.rotation);
            StatsPlayer statsP = player.GetComponent<StatsPlayer>();
            statsP.sumarScore();
        }

        if (golpeado == false)
        {
            rb.velocity = 1 * transform.up * velocidad;
        }
        else
        {
            if (timerGolpeado < 0)
            {
                golpeado = false;
                timerGolpeado = 0.4f;
                sr.color = ColorInicial;
            }
            else
            {
                timerGolpeado -= Time.deltaTime;
            }
        }

        CheckProyectil2();


    }

    void CheckProyectil2()
    {
        proys2 = GameObject.FindGameObjectsWithTag("Proyectil2");

        if (proys2.Length > 0)
        {
            Vector3 proy2Pos = proy2.transform.position;
            Vector2 direc = new Vector2(proy2Pos.x - transform.position.x, proy2Pos.y - transform.position.y);
            transform.up = direc;
            Aturdido = true;
            sr.color = Color.yellow;
        }
        else
        {
            if (golpeado == false)
            {
                sr.color = ColorInicial;
            }

            Rotar();
            Aturdido = false;
        }
    }

    void Rotar()
    {

            Vector3 playerPos = player.transform.position;
            Vector2 direc = new Vector2(playerPos.x - transform.position.x, playerPos.y - transform.position.y);
            transform.up = direc;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Aturdido == false)
        {
            if (collision.gameObject.tag == "Proyectil")
            {
                //  rb.AddRelativeForce(transform.forward * (fuerza / Time.deltaTime));
                AudioSource.PlayClipAtPoint(Golpe, transform.position, 3f);
                sr.color = Color.red;
                vida--;
                rb.AddRelativeForce(collision.gameObject.transform.up, ForceMode2D.Impulse);
                golpeado = true;
            }

            if (collision.gameObject.tag == "Player")
            {
                Destroy(this.gameObject, 0);
                GameObject aux = Instantiate(explosionMuerte, transform.position, transform.rotation);
                AudioSource.PlayClipAtPoint(Explosion, transform.position, 3f);
                StatsPlayer statsP = player.GetComponent<StatsPlayer>();
                statsP.restarVida();
            }
        }
        else
        {
            if (collision.gameObject.tag == "Proyectil")
            {
                AudioSource.PlayClipAtPoint(Golpe, transform.position, 3f);
                DestruirEnemigo();
                StatsPlayer statsP = player.GetComponent<StatsPlayer>();
                statsP.sumarScore();
            }
        }
    }
}
