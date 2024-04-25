using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionRoueDentelee : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D infoCollision)
    {
        // Si collision avec Megaman
        if (infoCollision.gameObject.name == "MegaMan" || infoCollision.gameObject.tag == "Projectile")
        {
            //Animation d'explosion
            GetComponent<Animator>().enabled = true;
            //Enlève le collider
            GetComponent<Collider2D>().enabled = false;
            //Enlève la vélocité
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //Enlève la vélocité angulaire
            GetComponent<Rigidbody2D>().angularVelocity = 0;
            //Enlève la gravité
            GetComponent<Rigidbody2D>().gravityScale = 0;
            //Détruit objet
            Destroy(gameObject, 0.5f);
        }
    }
}
