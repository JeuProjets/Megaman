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
            //Enl�ve le collider
            GetComponent<Collider2D>().enabled = false;
            //Enl�ve la v�locit�
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //Enl�ve la v�locit� angulaire
            GetComponent<Rigidbody2D>().angularVelocity = 0;
            //Enl�ve la gravit�
            GetComponent<Rigidbody2D>().gravityScale = 0;
            //D�truit objet
            Destroy(gameObject, 0.5f);
        }
    }
}
