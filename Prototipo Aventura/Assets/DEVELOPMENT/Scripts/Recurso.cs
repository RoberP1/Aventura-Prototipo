using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recurso : MonoBehaviour
{
    GameObject yo;
    public GameObject recurso;
    public int randomLoot;
    
    public float Vida;
    void Start()
    {
        yo = this.gameObject;
    }

 
    void Update()
    {

    }
    public void RecibirDamage(float damage)
    {
        Vida -= damage;
        if (Vida <= 0) 
        {
            for (int i = 0; i < Random.Range(1,randomLoot);i++)
            {
                Instantiate(recurso, yo.transform.position + new Vector3(Random.Range(-4, 4), 5, Random.Range(-4, 4)), yo.transform.rotation);
            }
            
            Destroy(yo);
        }

        
    }
}
