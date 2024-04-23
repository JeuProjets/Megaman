using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreerEnnemis : MonoBehaviour
{
    public GameObject ennemiACreer; //La roue dentelée à dupliquer
    public GameObject personnage; //Pour la position de Megaman

    public float limiteGauche; //Déterminer la zone de reproduction
    public float limiteDroite;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DupliqueRoue", 0f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DupliqueRoue()
    {
        if (personnage.transform.position.x > limiteGauche && personnage.transform.position.x < limiteDroite)
        {
            GameObject copie = Instantiate(ennemiACreer);
            copie.SetActive(true);
            copie.transform.position = new Vector3(Random.Range(personnage.transform.position.x - 8f, personnage.transform.position.x + 8f), 8f, 0f);
        }
    }

}
