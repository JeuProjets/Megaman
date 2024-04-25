using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleCamera2 : MonoBehaviour
{   
    //Cible à suivre: Megaman
    public GameObject cible;

    //Limites de la caméra
    public float limiteHaut;
    public float limiteBas;
    public float limiteGauche;
    public float limiteDroite;


    // Update is called once per frame
    void Update()
    {
        //On prend la position de Megaman
        Vector3 positionActuelle = cible.transform.position;

        //Détermine les limites
        if (positionActuelle.x < limiteGauche) positionActuelle.x = limiteGauche;

        if (positionActuelle.x > limiteDroite) positionActuelle.x = limiteDroite;

        if (positionActuelle.y < limiteBas) positionActuelle.y = limiteBas;

        if (positionActuelle.y > limiteHaut) positionActuelle.y = limiteHaut;

        positionActuelle.z = -10;

        transform.position = positionActuelle;

    }
}
