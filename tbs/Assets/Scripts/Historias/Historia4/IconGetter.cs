using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IconGetter : MonoBehaviour, IDropHandler {

	public void OnDrop(PointerEventData eventData)
	{
		Transform droppedIcon;
		droppedIcon = IconDragger.draggedIcon;

		if(this.gameObject.CompareTag(droppedIcon.tag))
		{
			IconDragger.draggedIcon = null;
			droppedIcon.SetParent(transform, false);
		}
		else
		{
			print("Error");
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
