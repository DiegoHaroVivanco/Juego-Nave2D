
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    //Este script se encargara de hacer que el proyectil se mueva y se destryua
    Rigidbody2D rb;
    public int TipoProyectil;
    [Range(1,25)]
    public float velocidad = 15f;
    public GameObject explosion;
    float timer = 6.5f;

    GameObject player;
    CircleCollider2D playercol;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playercol = player.GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
   
        if (TipoProyectil == 1)
        {
            Destroy(this.gameObject, 5);
        }
    }

    private void Update()
    {

        if (TipoProyectil == 2)
        {
            if (timer < 0.1f)
            {
                playercol.isTrigger = false;
                Destroy(this.gameObject, 0);
            }
            else
            {
                timer -= Time.deltaTime;
                playercol.isTrigger = true;
            }
        }

        if (TipoProyectil == 1)
        {
            rb.velocity = 1 * transform.up * velocidad;
        }
        else if (TipoProyectil == 2)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direc = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
            transform.up = direc;
            rb.velocity = 1 * transform.up * (velocidad / 2);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemigo")
        {
            if (TipoProyectil == 1)
            {
                Enemigo en = collision.gameObject.GetComponent<Enemigo>();
                if (en.vida > 1)
                {
                    GameObject aux = Instantiate(explosion, transform.position, transform.rotation);
                    Destroy(aux, 1);
                }
                Destroy(this.gameObject, 0);
            }
            else if (TipoProyectil == 2)
            {
                Enemigo en = collision.gameObject.GetComponent<Enemigo>();
                en.DestruirEnemigo();
            }
        }
    }
}
