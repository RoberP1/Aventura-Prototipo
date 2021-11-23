using StarterAssets;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public Camera camera;
    public int interactableLayerIndex;
    RaycastHit hit;
    private Vector3 raycastPos;
    private StarterAssetsInputs _input;
    private TicksManager ticksManager;
    public int attackCDMax;
    public int attackCD;
    public bool Canattack = true;
    private IInventory inventory;
    private Hotbar hotbar;
    public bool opencraft;
    void Start()
    {
        attackCD = 0;
        hotbar = GetComponent<Hotbar>();
        inventory = GetComponent<IInventory>();
        _input = GetComponent<StarterAssetsInputs>();
        ticksManager = FindObjectOfType<TicksManager>();
        TicksManager.OnTick += TimeTickSystem_OnTick;
    }

    // Update is called once per frame
    void Update()
    {
        
        raycastPos = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (_input.interaction && Physics.Raycast(raycastPos, camera.transform.forward,out hit,6f, 1 << interactableLayerIndex))
        {
            //print(hit.collider.name);
            if ( hit.collider.gameObject.GetComponentInParent<IObject>())
            {
                inventory.Add(hit.collider.gameObject.GetComponentInParent<IObject>());
                print(hit.collider.gameObject.GetComponentInParent<IObject>());
                Destroy(hit.collider.gameObject);
            }
            if ( !opencraft &&  hit.collider.gameObject.GetComponentInParent<Crafting>())
            {
                //print("abriendo");
                _input.OpenInv = true;
                opencraft = true;
                hit.collider.gameObject.GetComponentInParent<Crafting>().OpenCraft();
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            
            if (hotbar.Omano == null && Canattack &&Physics.Raycast(raycastPos, camera.transform.forward, out hit, 4, LayerMask.GetMask("Resources")))
            {
                hit.collider.gameObject.GetComponentInParent<Recurso>().RecibirDamage(10);
                CDAttack(15);
                //print(hit.collider);
            }
            else if(hotbar.Omano != null)
            {
                if (hotbar.Omano.GetComponent<IObject>().name == hotbar.OPuerta.GetComponent<IObject>().name)
                {
                    GameObject puerta = Instantiate(hotbar.OPuerta, hotbar.Building);
                    puerta.transform.SetParent(null);
                    hotbar.RemovePuerta();

                }
                if (hotbar.Omano.GetComponent<IObject>().name == hotbar.OPared.GetComponent<IObject>().name)
                {
                    GameObject pared = Instantiate(hotbar.OPared, hotbar.Building);
                    pared.transform.SetParent(null);
                    hotbar.RemovePared();

                }
                if (hotbar.Omano.GetComponent<IObject>().name == hotbar.OEspada.GetComponent<IObject>().name && Canattack && Physics.Raycast(raycastPos, camera.transform.forward, out hit, 6f, LayerMask.GetMask("Resources")))
                {
                    hit.collider.gameObject.GetComponentInParent<Recurso>().RecibirDamage(30);
                    CDAttack(15);
                    //print(hit.collider);
                }
                if (hotbar.Omano.GetComponent<IObject>().name == hotbar.OHacha.GetComponent<IObject>().name && Canattack && Physics.Raycast(raycastPos, camera.transform.forward, out hit, 4f, LayerMask.GetMask("Resources")))
                {
                    hit.collider.gameObject.GetComponentInParent<Recurso>().RecibirDamage(50);
                    CDAttack(25);
                    //print(hit.collider);
                }
                if (hotbar.Omano.GetComponent<IObject>().name == hotbar.OLanza.GetComponent<IObject>().name && Canattack && Physics.Raycast(raycastPos, camera.transform.forward, out hit, 10f, LayerMask.GetMask("Resources")))
                {
                    hit.collider.gameObject.GetComponentInParent<Recurso>().RecibirDamage(20);
                    CDAttack(10);
                    //print(hit.collider);
                }
                
            }
        }
    }

    private void CDAttack(int ticks)
    {
        Canattack = false;
        
        attackCDMax = ticks;
    }

    private void TimeTickSystem_OnTick(object sender,TicksManager.OnTickEventArgs e)
    {
        //print("tick");
        if (!Canattack)
        {
            //print("no ataco");
            attackCD += 1;

            if (attackCD >= attackCDMax)
            {
                Canattack = true;
                attackCD = 0;
            }
        }
    }
}
