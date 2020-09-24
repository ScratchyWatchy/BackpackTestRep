using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/// <summary>
/// Inventory UI manger. Must have a graphic Raycaster assigned. InventoryParent contains UI elements displayed for a backpack
/// </summary>
public class Inventory : MonoBehaviour
{
    public RectTransform InventoryParent;
    public Camera cam;

    private PullOutButton button;

    private BackPackMono pack;

    public GraphicRaycaster gr;
    private PointerEventData ped;

    void Start()
    {
        ped = new PointerEventData(null);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            pack = GetBackpackFromMouseClick();
            if (pack != null)
            {
                DrawUI();
            }
        }
        if(Input.GetMouseButtonUp(0) && pack!= null)
        {
            button = GetButton();
            if (button != null)
            {
                button.OnRelease(pack);
            }
            pack = null;
            ClearUI();
        }
    }

    /// <summary>
    /// Draws the UI for a backpack, only for attached items
    /// </summary>
    void DrawUI()
    {
        InventoryParent.gameObject.SetActive(true);
        List<string> currentITems = pack.GetAttachedITems();
        List<PullOutButton> allButtons = InventoryParent.GetComponentsInChildren<PullOutButton>(true).ToList<PullOutButton>();
        foreach(string current in currentITems)
        {
            foreach(PullOutButton currentButton in allButtons)
            {
                if(currentButton.slot == current)
                {
                    currentButton.gameObject.SetActive(true);
                }
            }
        }
        InventoryParent.position = Input.mousePosition;
    }

    /// <summary>
    /// Hides the backpack UI
    /// </summary>
    void ClearUI()
    {
        int children = InventoryParent.childCount;
        for(int i = 0; i < children; i++)
        {
            if(InventoryParent.GetChild(i).GetComponent<PullOutButton>() != null)
            {
                InventoryParent.GetChild(i).gameObject.SetActive(false);
            }
        }
        InventoryParent.gameObject.SetActive(false);
    }

    /// <summary>
    /// Gets a button under the mouse
    /// </summary>
    /// <returns>
    /// Returns an Item retriver button
    /// </returns>
    PullOutButton GetButton()
    {
        ped.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(ped, results);
        if (results.Count() != 0 && results.Exists(x => x.gameObject.TryGetComponent(out PullOutButton button) == true))
        {
            return results.First(x => x.gameObject.TryGetComponent(out PullOutButton button) == true).gameObject.GetComponent<PullOutButton>();
        }
        return null;
    }

    /// <summary>
    /// Gets a backpack object under the mouse
    /// </summary>
    /// <returns>
    /// Returns BackpackMono
    /// </returns>
    BackPackMono GetBackpackFromMouseClick()
    {
        RaycastHit hitInfo = new RaycastHit();
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        bool hit = Physics.Raycast(ray, out hitInfo);
        if (hit)
        {
            if (hitInfo.collider.gameObject.tag == "Backpack")
            {
                return hitInfo.collider.gameObject.GetComponent<BackPackMono>();
            }
        }

        return null;
    }
}
