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
        if (collisionsBalle.gameObject.tag == "mechant")
        {

            Destroy(collisionsBalle.gameObject, 0.3f);

        }
        Destroy(gameObject, 0.15f);
    }
}
