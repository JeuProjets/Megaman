using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeplacementsMegaman : MonoBehaviour
{

    //VARIABLES
    float vitesseX;      //vitesse horizontale actuelle 
    public float vitesseXMax;   //vitesse horizontale Maximale désirée
    float vitesseY;      //vitesse verticale 
    public float vitesseSaut;   //vitesse de saut désirée

    //si Megaman a une collision avec un objet
    public bool megamanCollision;

    //si Megaman est mort
    public bool estMort;

    //Si Megaman est en attaque, false au départ
    public bool enAttaque = false;

    //Vitesse max pour le dash
    public float vitesseMaximale;

    //SONS
    public AudioClip sonMort;

    public AudioClip sonPerdu;
    public AudioClip SonsArme;

    //GAME OBJECTS
    public GameObject BalleOriginale;
    public GameObject balleClone;


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
            //On met la variable d'attaque à true
            enAttaque = true;

            //L'animation d'attaque est à vrai
            GetComponent<Animator>().SetBool("attaque", true);

            //On désactive l'option de sauter
            GetComponent<Animator>().SetBool("saut", false);

            //Appel de la fonction qui permet d'attaquer encore avec un délai
            Invoke("possibiliteAttaque",0.5f);

        }

        //On augmente la vitesse de Megaman pour l'attaque, avec une vitesse Max
        if (enAttaque && Mathf.Abs(vitesseX) <= vitesseXMax)
        {
            vitesseX *= 5;
        }
        //Tire de la balle
        if (Input.GetKeyDown(KeyCode.Return) && enAttaque == false && megamanCollision)
        {
            //On déclenche l’animation appropriée(variable tireBalle de l’animation à true).
            GetComponent<Animator>().SetBool("tireBalle", true);

            //On instancie une balle à partir de la balle d’origine:
            balleClone = Instantiate(BalleOriginale);

            //Rend active
            balleClone.SetActive(true);

            //Son de la balle
            GetComponent<AudioSource>().PlayOneShot(SonsArme);

            //Direction de la balle selon le flip X de Megaman
            //Gauche
            if (GetComponent<SpriteRenderer>().flipX)
            {
                balleClone.transform.position = transform.position + new Vector3(-.6f, 1); 
                balleClone.GetComponent<Rigidbody2D>().velocity = new Vector2(-25, 0);
            }
            //Droite
            else
            {
                balleClone.transform.position = transform.position + new Vector3(.6f, 1);
                balleClone.GetComponent<Rigidbody2D>().velocity = new Vector2(25, 0);
            }
            

        }
        //Arrête le tire
        else if(Input.GetKeyUp(KeyCode.Return))
        {
            GetComponent<Animator>().SetBool("tireBalle", false);
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
        //Variable de collision est à vrai
        megamanCollision = true;

        
        if (Physics2D.OverlapCircle(transform.position, 0.2f))
        {
            //On empêche l'option de sauter
            GetComponent<Animator>().SetBool("saut", false);
        }
        //Collision ROUES
        if (collisionsMegaman.gameObject.name == "RoueDentelee" && enAttaque == false)
        {
            //Enregistre qu'il est mort
            estMort = true;

            //Animation mort
            GetComponent<Animator>().SetBool("mort", true);

            //Son mort
            GetComponent<AudioSource>().PlayOneShot(sonMort);

            //Appel la fonction qui permet de relancer le jeu avec un délai
            Invoke("RelancerJeu", 2f);

        }
        //Collision ABEILLES
        if (collisionsMegaman.gameObject.name == "Abeille")
        {
            //Si il était en train d'attaquer
            if (enAttaque)
            {
                //Animation de collision sur l'ABEILLE
                collisionsMegaman.gameObject.GetComponent<Animator>().SetTrigger("collision");

                //On détruit l'abeille
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


                //Appel la fonction qui permet de relancer le jeu avec un délai
                Invoke("RelancerJeu", 2f);
            }
            
        }
        //Collision trophée = Joueur a gagné
        if(collisionsMegaman.gameObject.name == "Trophee")
        {
            //Variable mort à false
            estMort = false;

            //On détruie le trophée
            Destroy(collisionsMegaman.gameObject);

            //Gestion de la scène 
            Invoke("RelancerJeu", 2f);
        }



    }

    void OnTriggerEnter2D(Collider2D collisionsMegaman)
    {
        //Lorsque Megaman tombe dans le vide
        if (collisionsMegaman.gameObject.name == "MeurtVide")
        {
            //Il est mort
            estMort = true;

            //On relance le jeu
            Invoke("RelancerJeu", 2f);
        }
    }
    //Gestion des scènes
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
    

    //Fonction qui permet à Megaman d'attaquer encore
    private void possibiliteAttaque()
    {
        GetComponent<Animator>().SetBool("attaque", false);
        enAttaque = false;
    }

}


