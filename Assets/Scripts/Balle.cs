using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collisionsBalle)
    {
        //Collision de la balle avec l'abeille
        if (collisionsBalle.gameObject.name == "Abeille")
        {
            //On détruie l'abeille
            Destroy(collisionsBalle.gameObject, 0.4f);
        }
        //On détruie la balle
        Destroy(gameObject, 0.15f);
    }
}
