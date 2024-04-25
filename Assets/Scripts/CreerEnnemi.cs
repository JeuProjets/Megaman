using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreerEnnemis : MonoBehaviour
{
    //La roue dentelée à dupliquer
    public GameObject ennemiACreer;

    //Pour la position de Megaman
    public GameObject personnage; 

    //Déterminer la zone de reproduction
    public float limiteGauche; 
    public float limiteDroite;
    // Start is called before the first frame update
    void Start()
    {
        //On duplique la roue a chaque 3 secondes
        InvokeRepeating("DupliqueRoue", 0f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DupliqueRoue()
    {
        //Si le personnages est dans les limites
        if (personnage.transform.position.x > limiteGauche && personnage.transform.position.x < limiteDroite)
        {
            //On duplique la roue
            GameObject copie = Instantiate(ennemiACreer);
            //On active l'object
            copie.SetActive(true);
            //Donne la position
            copie.transform.position = new Vector3(Random.Range(personnage.transform.position.x - 8f, personnage.transform.position.x + 8f), 8f, 0f);
        }
    }

}
