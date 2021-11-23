using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class IInventory : MonoBehaviour
{
    public List<ISlot> inventory;
    public Transform inventoryUI;
    public ISlotUI slotUIPrefab;
    private StarterAssetsInputs _input;
    private Hotbar hotbar;

    private void Start()
    {
        hotbar = GetComponent<Hotbar>();
        _input = GetComponent<StarterAssetsInputs>();
        UpdateUI();
        inventoryUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        
        inventoryUI.gameObject.SetActive(_input.OpenInv);
        if (_input.OpenInv)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        




    }

    public void Add(IObject item)
    {
        

        if (Find(item.name) == null || !Find(item.name).stackable)
        {
            ISlot newSlot = new ISlot(item.name, item.description, item.quantity, item.stackable);
            inventory.Add(newSlot);
            switch (item.name)
            {
                case "Espada":
                    hotbar.AddEspada();
                    break;
                case "Hacha":
                    hotbar.AddHacha();
                    break;
                case "Lanza":
                    hotbar.AddLanza();
                    break;
                case "Puerta":
                    hotbar.AddPuerta();
                    break;
                case "Pared":
                    hotbar.AddPared();
                    break;
                default:
                    break;
            }
        }
        else
        {
            Find(item.name).quantity += item.quantity;
        }
        UpdateUI();
    }

    public ISlot Find(string name) => inventory.Find((item) => item.name == name);

    public bool Materiales(string name, int quantity)
    {
        ISlot material = Find(name);
        if (material != null && material.quantity >= quantity)
        {
            return true;
        }
        else return false;
    }

    public void Remove(string name, int quantity)
    {
        var slot = Find(name);

        if (slot.quantity - quantity <= 0)
            inventory.Remove(Find(name));
        else 
            slot.quantity -= quantity;

        UpdateUI();
    }

    public void UpdateUI()
    {
        foreach (Transform child in inventoryUI) if (child.gameObject != slotUIPrefab.gameObject) Destroy(child.gameObject);
        foreach (var item in inventory)
        {
            ISlotUI slot = Instantiate(slotUIPrefab.gameObject, inventoryUI).GetComponent<ISlotUI>();
            slot.itemName.text = item.name + " x " + item.quantity;
            slot.delete.onClick.AddListener(() => Remove(item.name, item.quantity));
            slot.removeOne.onClick.AddListener(() => Remove(item.name, 1));
            slot.gameObject.SetActive(true);
        }
    }

    [System.Serializable]
    public class ISlot
    {
        public string name, description;
        public int quantity;
        public bool stackable;

        public ISlot(string name, string description, int quantity, bool stackable)
        {
            this.name = name;
            this.description = description;
            this.quantity = quantity;
            this.stackable = stackable;
        }
    }
}
