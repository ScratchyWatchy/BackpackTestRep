using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DTO for backpack edits
/// </summary>

[Serializable]
public class BeckpackEdit
{
    [SerializeField]
    Backpack backpack;
    [SerializeField]
    AttachableObject item;
    [SerializeField]
    string action;

    public BeckpackEdit(Backpack backpack, AttachableObject item, string action)
    {
        this.backpack = backpack;
        this.item = item;
        this.action = action;
    }
}
