using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IconDraggerArbol : MonoBehaviour, IBeginDragHandler, IDragHandler{

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
			draggedIcon = transform.GetChild(0);
			Destroy(transform.gameObject);
			draggedIcon.SetParent(this.hand, false);
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		this.hand.position = Input.mousePosition;
	}
}
