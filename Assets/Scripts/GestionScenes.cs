using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //Sc�ne d'intro --> Sc�ne de jeu
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Megaman1");
        }

    }
}
