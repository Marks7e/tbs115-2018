using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class IconGetter : MonoBehaviour, IDropHandler {

	public GameStatus gs;
    public AudioSource audioSource;
	public int waitingTime = 3;
	//public AudioClip bgMusic;

	/*void Start(){
		audioSource = GetComponent<AudioSource>();
        bgMusic = Resources.Load<AudioClip>("Sounds/TalkingAbout");
        audioSource.PlayOneShot(bgMusic);
	}*/

	public void OnDrop(PointerEventData eventData)
	{
		Transform droppedIcon;
		droppedIcon = IconDragger.draggedIcon;

		if(this.gameObject.CompareTag(droppedIcon.tag))
		{
			if(transform.childCount > 0)
			{	
				IconDragger.draggedIcon = transform.GetChild(0);
				//Destroy(droppedIcon.gameObject);
			}
			else
			{
				IconDragger.draggedIcon = null;
			}
			droppedIcon.SetParent(transform, false);
			print("Excelente");
			Bar.slots[GetComponent<Slot>().id] = 1;
		}
		else
		{	
			if(transform.childCount > 0)
			{
				IconDragger.draggedIcon = transform.GetChild(0);
				//Destroy(droppedIcon.gameObject);
			}
			else
			{
				IconDragger.draggedIcon = null;
			}
			Bar.slots[GetComponent<Slot>().id] = 0;
			droppedIcon.SetParent(transform, false);
			//audioSource.Stop();
			gs = new GameStatus();
            gs.PlayerNeedToRepeatGame(audioSource, waitingTime);
			print("Error");
			SceneManager.LoadScene("Minijuego 4");
		}
	}
}
