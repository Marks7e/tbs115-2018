using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IconDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public bool clonable;
	public static Transform draggedIcon;
	public Transform hand;

	public void OnBeginDrag(PointerEventData eventData)
	{
		if(transform.childCount == 0)
		{
			eventData.pointerDrag = null;
			return;
		}

		if(!this.clonable)
		{
			if(this.gameObject.CompareTag("Arbol"))
			{
				draggedIcon = transform.GetChild(0);
				Destroy(transform.gameObject);
				draggedIcon.SetParent(this.hand, false);
			}
			else
			{
				draggedIcon = transform.GetChild(0);
				draggedIcon.SetParent(this.hand, false);
			}
			
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		this.hand.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if(draggedIcon == null) return;

		draggedIcon.SetParent(transform, false);
		draggedIcon = null;
	}
}

