using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Removes an item from backback when button is released
/// </summary>
public class PullOutButton: MonoBehaviour
{
    public string slot;

    public void OnRelease(BackPackMono backpack)
    {
        backpack.RemoveItem(slot);
    }

    public void AssignSlot()
    {

    }
}
