using UnityEngine;
using UnityEngine.EventSystems;

public class InvnetorySlot : MonoBehaviour, IDropHandler
{
public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItgem draggableItgem = dropped.GetComponent<DraggableItgem>();
            draggableItgem.parentAfterDrag = transform; 
        }

    }
}
