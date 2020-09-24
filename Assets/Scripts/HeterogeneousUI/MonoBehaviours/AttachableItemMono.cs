using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Physicall front for attachable items
/// </summary>
public class AttachableItemMono : MonoBehaviour
{
    public float weight;
    public string itemName;
    public string type;

    [HideInInspector]
    public AttachableObject item;

    private Rigidbody rb;
    private Collider col;

    [HideInInspector]
    public DragObject dragManager;

    void Awake()
    {
        dragManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<DragObject>();
        rb = gameObject.GetComponent<Rigidbody>();
        col = gameObject.GetComponent<Collider>();
        item = new AttachableObject(weight, itemName, type);
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Didables the rigidbody and the collider and attaches to a parent
    /// </summary>
    public void AttachToBackpack(Transform parent)
    {    
        transform.SetParent(parent);
        rb.isKinematic = true;
        col.enabled = false;
        StartCoroutine(SmoothMove(0.2f, parent));
        StartCoroutine(SmoothRotation(0.2f, parent));
    }

    /// <summary>
    /// Enables the rigidbody and the collider and detaches from a parent, then sets an object to the DragObject
    /// </summary>
    public void DettachFromBackpack()
    {
        transform.SetParent(null);
        rb.isKinematic = false;
        col.enabled = true;
        dragManager.GiveItem(rb, this);
    }

    IEnumerator SmoothMove(float time, Transform target)
    {      
        float elapsedTime = 0;

        while (transform.position != target.position)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator SmoothRotation(float time, Transform target)
    {      
        float elapsedTime = 0;

        while (transform.rotation.eulerAngles != target.rotation.eulerAngles)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
