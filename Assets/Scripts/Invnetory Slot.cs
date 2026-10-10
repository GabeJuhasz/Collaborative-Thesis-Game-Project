using UnityEngine;
using UnityEngine.EventSystems;

public class InvnetorySlot : MonoBehaviour, IDropHandler
{

public bool shipSlot;
public bool inventorySlot;
public int slotNum;

public GameObject mod;

public ShipModTracker tracker;
public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        //mod = eventData.pointerDrag;
        if (transform.childCount == 0)
        {
            //GameObject dropped = eventData.pointerDrag;
            DraggableItgem draggableItgem = dropped.GetComponent<DraggableItgem>();
            draggableItgem.parentAfterDrag = transform; 
        }

        if (shipSlot)
        {
           tracker.shipmods[slotNum] = dropped; 
        }

        if (inventorySlot)
        {
            InventoryManager.slots[slotNum - 1] = true;
        }

    }
}
