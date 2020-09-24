using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
/// <summary>
/// This class represents a backend of a backback object. ID also should be recieved from a sever I guess.
/// </summary>
public class Backpack
{
    public string id;
    public string name;
    public List<AttachableObject> items = new List<AttachableObject>();

    [SerializeField]
    private float maxLoad;
    [SerializeField]
    private float load = 0;
    public static BackPackEditEvent EditEvent;

    public Backpack(string name, float maxLoad, NetworkPost networkPost)
    {
        if (EditEvent == null)
            EditEvent = new BackPackEditEvent();
        this.name = name;
        this.maxLoad = maxLoad;
    }

    /// <summary>
    /// Adds an AttachebleObject to the items list and invokes backback edit event
    /// </summary>
    /// <returns>
    /// Return true if item was added and false if not
    /// </returns>
    public bool AddItem(AttachableObject item)
    {
        if(load + item.weight <= maxLoad && !items.Exists(x => x.type == item.type))
        {
            load += item.weight;
            items.Add(item);
            EditEvent.Invoke(JsonUtility.ToJson(new BeckpackEdit(this, item, "add")));
            return true;
        }
        return false;
    }

    /// <summary>
    /// Removes an AttachebleObject from the items list and invokes backback edit event
    /// </summary>
    /// <returns>
    /// Return true if item was removed and false if not
    /// </returns>
    public bool RemoveItem(string type)
    {
        AttachableObject item = items.Find(x => x.type == type);
        if (item != null)
        {
            load -= item.weight;
            items.Remove(item);
            EditEvent.Invoke(JsonUtility.ToJson(new BeckpackEdit(this, item, "remove")));
            return true;
        }
        return false;
    }
}
