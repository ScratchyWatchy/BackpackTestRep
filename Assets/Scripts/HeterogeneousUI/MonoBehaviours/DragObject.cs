using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gest a Rigidbody ant an AttachableItemMono from a gameobject and drags it around. Must be attached to a mainCamera
/// </summary>
public class DragObject : MonoBehaviour
{
    public float forceAmount = 100;

    Rigidbody selectedRigidbody;
    Camera targetCamera;
    Vector3 originalScreenTargetPosition;
    Vector3 originalRigidbodyPos;
    float selectionDistance;

    AttachableItemMono item;
    BackPackMono backpack;

    void Start()
    {
        targetCamera = GetComponent<Camera>();
    }

    void Update()
    {
        if (!targetCamera)
            return;

        if (Input.GetMouseButtonDown(0) && !selectedRigidbody)
        {
            //Check if we are hovering over Rigidbody, if so, select it
            selectedRigidbody = GetRigidbodyFromMouseClick();
        }
        if (Input.GetMouseButtonDown(1) && selectedRigidbody)
        {
            //Release selected Rigidbody if there any
            selectedRigidbody = null;
            backpack = GetBackPack();
            if (backpack != null)
            {
                backpack.AddItem(item);
            }
        }
    }

    void FixedUpdate()
    {
        if (selectedRigidbody)
        {
            Vector3 mousePositionOffset = targetCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y - 15f, selectionDistance)) - originalScreenTargetPosition;
            selectedRigidbody.velocity = (originalRigidbodyPos + mousePositionOffset - selectedRigidbody.transform.position) * forceAmount * Time.deltaTime;
        }
    }

    // <summary>
    /// Gets a rigidboys and AttachableItemMono from under the mouse
    /// </summary>
    /// <returns>
    /// Returns a Rigidbody that was under a mouse or null if there wasnt any and sets item to thsts gameobject AttachableItemMono component
    /// </returns>
    Rigidbody GetRigidbodyFromMouseClick()
    {
        RaycastHit hitInfo = new RaycastHit();
        Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);
        bool hit = Physics.Raycast(ray, out hitInfo);
        if (hit)
        {
            if (hitInfo.collider.gameObject.GetComponent<Rigidbody>() && hitInfo.collider.gameObject.tag == "Draggable")
            {
                selectionDistance = Vector3.Distance(ray.origin, hitInfo.point);
                originalScreenTargetPosition = targetCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance));
                originalRigidbodyPos = hitInfo.collider.transform.position;
                item = hitInfo.collider.gameObject.GetComponent<AttachableItemMono>();
                return hitInfo.collider.gameObject.GetComponent<Rigidbody>();
            }
        }

        return null;
    }


    /// <summary>
    /// Gets a backpack under the mouse
    /// </summary>
    /// <returns>
    /// Returns a backpack object that was under a mouse or null if there wasnt any
    /// </returns>
    BackPackMono GetBackPack()
    {
        RaycastHit hitInfo = new RaycastHit();
        Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);
        bool hit = Physics.Raycast(ray, out hitInfo);
        if (hit)
        {
            if (hitInfo.collider.gameObject.GetComponent<BackPackMono>())
            {
                return hitInfo.collider.gameObject.GetComponent<BackPackMono>();
            }
        }

        return null;
    }

    public void GiveItem(Rigidbody rb, AttachableItemMono item)
    {
        selectedRigidbody = rb;
        this.item = item;
    }    
}
