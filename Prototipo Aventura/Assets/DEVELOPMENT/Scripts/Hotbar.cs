using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    public Transform Mano,Building;
    public RawImage EspadaI;
    public RawImage HachaI;
    public RawImage LanzaI;
    public RawImage PuertaI;
    public RawImage ParedI;

    public GameObject OEspada,OLanza,OHacha,OPuerta,OPared;
    public GameObject Omano;
    public bool espada, hacha,lanza,puerta,pared = false;

    private IInventory inventory;
    void Start()
    {
        inventory = GetComponent<IInventory>();
        EspadaI.gameObject.SetActive(false);
        HachaI.gameObject.SetActive(false);
        LanzaI.gameObject.SetActive(false);
        PuertaI.gameObject.SetActive(false);
        ParedI.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && espada)
        {
            if (Omano != null)
            {
                Destroy(Omano);
            }
            Omano = Instantiate(OEspada, Mano);
            //print("atack");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && hacha)
        {
            if (Omano != null)
            {
                Destroy(Omano);
            }
            Omano = Instantiate(OHacha, Mano);
            //print("atack");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && lanza)
        {
            if (Omano != null)
            {
                Destroy(Omano);
            }
            Omano = Instantiate(OLanza, Mano);
            //print("atack");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && puerta)
        {
            if (Omano != null)
            {
                Destroy(Omano);
            }
            Omano = Instantiate(OPuerta, Building);
            //print("atack");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && pared)
        {
            if (Omano != null)
            {
                Destroy(Omano);
            }
            Omano = Instantiate(OPared, Building);
            //print("atack");
        }

    }
    public void AddEspada()
    {
        EspadaI.gameObject.SetActive(true);
        espada = true;
    }
    public void AddHacha() 
    { 
        HachaI.gameObject.SetActive(true);
        hacha = true;
    }
    public void AddLanza()
    {
        LanzaI.gameObject.SetActive(true);
        lanza = true;
    }
    public void AddPuerta()
    {
        PuertaI.gameObject.SetActive(true);
        puerta = true;
    }
    public void AddPared()
    {
        ParedI.gameObject.SetActive(true);
        pared = true;
    }
    public void RemovePuerta()
    {
        
        inventory.Remove(OPuerta.GetComponent<IObject>().name, 1);
        if (inventory.Find("Puerta") == null)
        {
            PuertaI.gameObject.SetActive(false);
            puerta = false;
            Destroy(Omano);
            
        }
    }

    public void RemovePared()
    {

        inventory.Remove(OPared.GetComponent<IObject>().name, 1);
        if (inventory.Find("Pared") == null)
        {
            ParedI.gameObject.SetActive(false);
            pared = false;
            Destroy(Omano);

        }
    }
}
