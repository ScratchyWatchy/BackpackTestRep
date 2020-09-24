using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

[Serializable]
/// <summary>
/// This class represents a backend of an attachable object. Id should be recieved from a server I guess. And type should be in a dictionary or list, not a string didnt have tima to add, on TODO
/// </summary>
public class AttachableObject
{
    public float weight;
    public string name;
    public int id;
    public string type;

    public AttachableObject(float weight, string name, string type)
    {
        this.weight = weight;
        this.name = name;
        this.type = type;
    }
}
