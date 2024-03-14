using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementsMegaman : MonoBehaviour
{
    float vitesseX;      //vitesse horizontale actuelle
    public float vitesseXMax;   //vitesse horizontale Maximale d�sir�e
    float vitesseY;      //vitesse verticale 
    public float vitesseSaut;   //vitesse de saut d�sir�e

    public bool megamanCollision;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // d�placement vers la gauche
        if (Input.GetKey("left"))
        {
            vitesseX = -vitesseXMax;
            GetComponent<SpriteRenderer>().flipX = true;

        }
        else if (Input.GetKey("right"))   //d�placement vers la droite
        {
            vitesseX = vitesseXMax;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            vitesseX = GetComponent<Rigidbody2D>().velocity.x;  //m�morise vitesse actuelle en X
        }

        // sauter l'objet � l'aide la touche "w"
        if (Input.GetKeyDown("up"))
        {
            vitesseY = vitesseSaut;
            GetComponent<Animator>().SetBool("saut", true);
            megamanCollision = false;
        }
        else
        {
            vitesseY = GetComponent<Rigidbody2D>().velocity.y;  //vitesse actuelle verticale
        }


        //Applique les vitesses en X et Y
        GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY);


        //**************************Gestion des animaitons de course et de repos********************************
        //Active l'animation de course si la vitesse de d�placement n'est pas 0, sinon le repos sera jouer par Animator
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
    }
}
