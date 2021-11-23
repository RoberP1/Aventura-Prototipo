using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Crafting : MonoBehaviour
{
    public GameObject Player;
    public IInventory inventory;

    public Transform craftingUI;
    public ICraftUI craftUIPrefab;

    public List<IObject> craftables;
    public List<IObject> crafteos;
    //public List<IMateriales> recursos;
    private StarterAssetsInputs _input;

    public Interaction Interaction;
    private bool crafteable;
    void Start()
    {
        
        Player = GameObject.Find("PlayerCapsule");
        inventory = Player.GetComponent<IInventory>();
        craftingUI.gameObject.SetActive(false);
        _input = Player.GetComponent<StarterAssetsInputs>();
        Interaction = Player.GetComponent<Interaction>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( _input.CloseInv)
        {

            craftingUI.gameObject.SetActive(false);
            _input.OpenInv = false;
            Interaction.opencraft = false;
            
        }
    }
    public void UpdateUI()
    {
        ComprobarCrafteables();
        foreach (Transform child in craftingUI) if (child.gameObject != craftUIPrefab.gameObject) Destroy(child.gameObject);
        foreach (var item in craftables)
        {
            //print(item.name);
            ICraftUI slot = Instantiate(craftUIPrefab.gameObject, craftingUI).GetComponent<ICraftUI>();
            slot.itemName.text = item.name;
            slot.craft.onClick.AddListener(() => Craft(item));
            //slot.removeOne.onClick.AddListener(() => Remove(item.name, 1));
            slot.gameObject.SetActive(true);
        }
    }

    private void Craft(IObject item)
    {
        RemoveMateriales(item.crafteo);
        
        inventory.Add(item);
        
        UpdateUI();
    }

    private void RemoveMateriales(List<IQuerry> recursos)
    {
        foreach (var recurso in recursos)
        {
            inventory.Remove(recurso.name, recurso.quantity);
        }
    }

    public void OpenCraft()
    {
        
        craftingUI.gameObject.SetActive(true);
        UpdateUI();
        
        
    }

    private void ComprobarCrafteables()
    {
        foreach (var item in crafteos)
        {
            foreach (var res in item.crafteo)
            {
               crafteable = Check(res.name, res.quantity);
                if (!crafteable) break;
            }
            if (crafteable)
            {
                Add(item);
            }
            else craftables.Remove(item);
        }
        //EspadaCrafteable();
    }

    private bool Check(string name, int quantity)
    {
        if (!inventory.Materiales(name, quantity)) return false;
        return true;
    }

    public void Add(IObject item)
    {
        print(item.name);

        if (Find(item.name) == null)
        {
            
            craftables.Add(item);
        }
        
        //UpdateUI();
    }
    public IObject Find(string name) => craftables.Find((item) => item.name == name);
    public IObject FindCrafteo(string name) => crafteos.Find((item) => item.name == name);

  
}
