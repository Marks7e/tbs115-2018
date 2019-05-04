using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IconGetterGame5 : MonoBehaviour, IDropHandler
{
    /*public GameStatus gameStatusModel;
    public AudioSource audioSource;
    public int waitingTime = 3;
    public DependencyInjector dependecyInjector;

    void Start()
    {
        dependecyInjector = new DependencyInjector();
        GetInitializeMusic();
    }*/

    public void OnDrop(PointerEventData eventData)
    {
        Transform droppedIcon;
        droppedIcon = IconDragger.draggedIcon;

        if (this.gameObject.CompareTag(droppedIcon.tag))
        {
            if (transform.childCount > 0)
            {
                IconDragger.draggedIcon = transform.GetChild(0);
            }
            else
            {
                IconDragger.draggedIcon = null;
            }
            droppedIcon.SetParent(transform, false);
            //Bar.slots[GetComponent<Slot>().id] = 1;
        }
        else
        {
            if (transform.childCount > 0)
            {
                IconDragger.draggedIcon = transform.GetChild(0);
            }
            else
            {
                IconDragger.draggedIcon = null;
            }
            //Bar.slots[GetComponent<Slot>().id] = 0;
            droppedIcon.SetParent(transform, false);
            //LoseGame();
        }
    }

    /*void GetInitializeMusic()
    {
        audioSource = GameObject.Find("Barra").GetComponent<AudioSource>();
    }

    void LoseGame()
    {
        dependecyInjector.UpdateLevelTimesPlayed(4);
        audioSource.Stop();
        gameStatusModel = new GameStatus();
        gameStatusModel.PlayerNeedToRepeatGame(audioSource, waitingTime, 4);
    }*/
}
