using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionCam : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;

    // Start is called before the first frame update
    void Start()
    {
        //La caméra 1 est celle par défault
        cam1.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        //Gestion des caméras selon la touche appuyée sur le clavier
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cam1.SetActive(true);
            cam2.SetActive(false);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cam1.SetActive(false);
            cam2.SetActive(true);

        }

    }
}
