using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Physicall front for the backpack
/// </summary>
public class BackPackMono : MonoBehaviour
{
    public string backpackName;
    public float maxLoad;

    public Transform meleePosition;
    public Transform rangedPosition;
    public Transform miscPosition;

    public NetworkPost networkPost;

    private Backpack backpack;

    void Awake()
    {
        backpack = new Backpack(backpackName, maxLoad, networkPost);
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Adds item physically and adding it io a backend backpack
    /// </summary>
    public void AddItem(AttachableItemMono item)
    {
        if(backpack.AddItem(item.item))
        {
            AttachItem(item);
        }
    }

    /// <summary>
    /// Removes item physically and remioves it from a backend backpack
    /// </summary>
    public void RemoveItem(string type)
    {
        if(backpack.RemoveItem(type))
        {
            DettachItem(type);
        }
    }

    /// <summary>
    /// Adds item to a correct position
    /// </summary>
    void AttachItem(AttachableItemMono item)
    {
        switch (item.type)
        {
            case "melee":
                item.AttachToBackpack(meleePosition);
                break;
            case "ranged":
                item.AttachToBackpack(rangedPosition);
                break;
            case "misc":
                item.AttachToBackpack(miscPosition);
                break;
            default:
                break;
        }
    }

    void DettachItem(string type)
    {
        switch (type)
        {
            case "melee":
                meleePosition.GetChild(0).GetComponent<AttachableItemMono>().DettachFromBackpack();
                break;
            case "ranged":
                rangedPosition.GetChild(0).GetComponent<AttachableItemMono>().DettachFromBackpack();
                break;
            case "misc":
                miscPosition.GetChild(0).GetComponent<AttachableItemMono>().DettachFromBackpack();
                break;
            default:
                break;
        }
    }

    public List<string> GetAttachedITems()
    {
        List<string> items = new List<string>();
        foreach(AttachableObject current in backpack.items)
        {
            items.Add(current.type);
        }
        return items;
    }
}
