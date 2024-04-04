using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeplacementsMegaman : MonoBehaviour
{
    float vitesseX;      //vitesse horizontale actuelle 
    public float vitesseXMax;   //vitesse horizontale Maximale désirée
    float vitesseY;      //vitesse verticale 
    public float vitesseSaut;   //vitesse de saut désirée

    public bool megamanCollision;

    public bool estMort;

    private bool peutAttaquer = true;
    public float vitesseMaximale;

    public AudioClip sonMort;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // déplacement vers la gauche
        if (Input.GetKey("left"))
        {
            vitesseX = -vitesseXMax;
            GetComponent<SpriteRenderer>().flipX = true;

        }
        else if (Input.GetKey("right"))   //déplacement vers la droite
        {
            vitesseX = vitesseXMax;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            vitesseX = GetComponent<Rigidbody2D>().velocity.x;  //mémorise vitesse actuelle en X
        }

        Physics2D.OverlapCircle(transform.position, 0.2f);

        // sauter l'objet à l'aide la touche "w"
        if (Input.GetKeyDown("up") && Physics2D.OverlapCircle(transform.position, 0.2f))
        {
            vitesseY = vitesseSaut;
            GetComponent<Animator>().SetBool("saut", true);
            megamanCollision = false;
        }
        else
        {
            vitesseY = GetComponent<Rigidbody2D>().velocity.y;  //vitesse actuelle verticale
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            peutAttaquer = false;
            GetComponent<Animator>().SetBool("attaque", true);

            GetComponent<Animator>().SetBool("saut", false);
            Invoke("possibiliteAttaque",2f);

        }

        if (peutAttaquer)
        {
            vitesseX *= 5;
        }

        //Applique les vitesses en X et Y
        GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY);


        //**************************Gestion des animaitons de course et de repos********************************
        //Active l'animation de course si la vitesse de déplacement n'est pas 0, sinon le repos sera jouer par Animator
        if (vitesseX > 0.9f || vitesseX < -0.9f)
        {
            GetComponent<Animator>().SetBool("marche", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("marche", false);

        }
        if(vitesseX < 0.9f)
        {
            GetComponent<Animator>().SetBool("repos", true);
        }
        
        if (megamanCollision == true)
        {
            GetComponent<Animator>().SetBool("saut", false);
        }
    }
    void OnCollisionEnter2D(Collision2D collisionsMegaman)
    {
        megamanCollision = true;


        if (Physics2D.OverlapCircle(transform.position, 0.2f))
        {
            GetComponent<Animator>().SetBool("saut", false);
        }


        if (collisionsMegaman.gameObject.name == "RoueDentelee")
        {
            estMort = true;
            GetComponent<Animator>().SetBool("mort", true);
            GetComponent<AudioSource>().PlayOneShot(sonMort);
            Invoke("RelancerJeu", 2f);
        }
    }

    private void RelancerJeu()
    {
        SceneManager.LoadScene("Megaman1");
    }
    private void possibiliteAttaque()
    {
        GetComponent<Animator>().SetBool("attaque", false);
        peutAttaquer = true;
    }

}


