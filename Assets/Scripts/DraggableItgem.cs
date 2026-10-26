using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DraggableItgem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;

    public ShipModTracker tracker;

    public GameObject mod;
    public void OnBeginDrag(PointerEventData eventData)
    {        
        mod = eventData.pointerDrag;
        if (transform.parent.GetComponent<InvnetorySlot>().shipSlot == true)
        {
            tracker.shipmods[transform.parent.GetComponent<InvnetorySlot>().slotNum] = null;
        }

        if (transform.parent.GetComponent<InvnetorySlot>().inventorySlot == true)
        {
            InventoryManager.slots[transform.parent.GetComponent<InvnetorySlot>().slotNum - 1] = false;
        }
        
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;


    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }

}
