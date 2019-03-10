using Assets.Scripts.DataPersistence.DependecyInjector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class IconGetter : MonoBehaviour, IDropHandler
{

    public GameStatus gs;
    public AudioSource audioSource;
    public int waitingTime = 3;
    public DependencyInjector di;

    void Start()
    {
        di = new DependencyInjector();
        GetInitializeMusic();
    }

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
            Bar.slots[GetComponent<Slot>().id] = 1;
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
            Bar.slots[GetComponent<Slot>().id] = 0;
            droppedIcon.SetParent(transform, false);
            LoseGame();
        }
    }

    void GetInitializeMusic()
    {
        audioSource = GameObject.Find("Barra").GetComponent<AudioSource>();
    }

    void LoseGame()
    {
        di.UpdateLevelTimesPlayed(3);
        audioSource.Stop();
        gs = new GameStatus();
        gs.PlayerNeedToRepeatGame(audioSource, waitingTime, 4);
    }
}
