using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IObject : MonoBehaviour
{
    public new string name;
    public string description;
    public int quantity;
    public bool stackable;

    public List<IQuerry> crafteo;
}
[System.Serializable]
public class IQuerry
{
    public string name;
    public int quantity;

    public IQuerry(string name, int quantity)
    {
        this.name = name;
        this.quantity = quantity;
    }
}
