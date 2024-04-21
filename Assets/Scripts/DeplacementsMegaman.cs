using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeplacementsMegaman : MonoBehaviour
{

    //VARIABLES
    float vitesseX;      //vitesse horizontale actuelle 
    public float vitesseXMax;   //vitesse horizontale Maximale d�sir�e
    float vitesseY;      //vitesse verticale 
    public float vitesseSaut;   //vitesse de saut d�sir�e

    //si Megaman a une collision avec un objet
    public bool megamanCollision;

    //si Megaman est mort
    public bool estMort;

    //Si Megaman est en attaque, false au d�part
    public bool enAttaque = false;

    //Vitesse max pour le dash
    public float vitesseMaximale;

    //Son de mort
    public AudioClip sonMort;

    public AudioClip sonPerdu;

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

        Physics2D.OverlapCircle(transform.position, 0.2f);

        // Saut
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
        //Attaque/dash qui s'active avec espace
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //On met la variable d'attaque � true
            enAttaque = true;

            //L'animation d'attaque est � vrai
            GetComponent<Animator>().SetBool("attaque", true);

            //On d�sactive l'option de sauter
            GetComponent<Animator>().SetBool("saut", false);

            //Appel de la fonction qui permet d'attaquer encore avec un d�lai
            Invoke("possibiliteAttaque",0.5f);

        }

        //On augmente la vitesse de Megaman pour l'attaque, avec une vitesse Max
        if (enAttaque && Mathf.Abs(vitesseX) <= vitesseXMax)
        {
            vitesseX *= 5;
        }


        //Applique les vitesses en X et Y
        GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY);


        //**************************Gestion des animaitons de course, de repos et de saut********************************
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


    //**************************Gestion des collisions********************************
    void OnCollisionEnter2D(Collision2D collisionsMegaman)
    {
        //Variable de collision est � vrai
        megamanCollision = true;

        
        if (Physics2D.OverlapCircle(transform.position, 0.2f))
        {
            //On emp�che l'option de sauter
            GetComponent<Animator>().SetBool("saut", false);
        }
        //Collision ROUES
        if (collisionsMegaman.gameObject.name == "RoueDentelee")
        {
            //Enregistre qu'il est mort
            estMort = true;

            //Animation mort
            GetComponent<Animator>().SetBool("mort", true);

            //Son mort
            GetComponent<AudioSource>().PlayOneShot(sonMort);

            //Appel la fonction qui permet de relancer le jeu avec un d�lai
            Invoke("RelancerJeu", 2f);

        }
        //Collision ABEILLES
        if (collisionsMegaman.gameObject.name == "Abeille")
        {
            //Si il �tait en train d'attaquer
            if (enAttaque)
            {
                //Animation de collision sur l'ABEILLE
                collisionsMegaman.gameObject.GetComponent<Animator>().SetTrigger("collision");

                //On d�truit l'abeille
                Destroy(collisionsMegaman.gameObject, 2f);
            }


            else
            {
                //Enregistre qu'il est mort
                estMort = true;

                //Animation mort
                GetComponent<Animator>().SetBool("mort", true);

                //Son mort
                GetComponent<AudioSource>().PlayOneShot(sonMort);


                //Appel la fonction qui permet de relancer le jeu avec un d�lai
                Invoke("RelancerJeu", 2f);
            }
            
        }
        if(collisionsMegaman.gameObject.name == "Trophee")
        {
            estMort = false;
            Destroy(collisionsMegaman.gameObject);
            Invoke("RelancerJeu", 2f);
        }



    }

    void OnTriggerEnter2D(Collider2D collisionsMegaman)
    {
        if (collisionsMegaman.gameObject.name == "MeurtVide")
        {
            estMort = true;
            Invoke("RelancerJeu", 2f);
        }
    }
    //Fonction qui permet de relancer le jeu 
    private void RelancerJeu()
    {
        if (estMort)
        {
            SceneManager.LoadScene("finaleMort");
        }
        else
        {
            SceneManager.LoadScene("finaleGagne");
        }
    }
    

    //Fonction qui permet � Megaman d'attaquer encore
    private void possibiliteAttaque()
    {
        GetComponent<Animator>().SetBool("attaque", false);
        enAttaque = false;
    }

}


