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
        // Si le terrain est touché alors active l'animation de l'objet et détruit le
        if (infoCollision.gameObject.name == "MegaMan")
        {
            GetComponent<Animator>().enabled = true;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            Destroy(gameObject, 0.5f);
        }
    }
}
