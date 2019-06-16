using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Minijuego7 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    // Objects spawn time.
    private float spawnTime = 2f;
    // Timer control flow
    private float timer = 0.0f;


    // Cube
    public GameObject plate;
    // Cylinder
    public GameObject bag;

    //Places
    public GameObject trashPlace;
    public GameObject platePlace;
    private string _musicName = "Sounds/Minigame";
    public AudioSource audioSource;
    public AudioClip bgMusic;

    public static GameObject itemBeingDragged;

    private enum Things
    {
        plate,
        bag
    };

    void Start()
    {
        PlayMusic();
        //Debug.Log(trashPlace);
        //Debug.Log(platePlace);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnTime)
        {
            // Reset timer
            timer = 0.0f;
            // Spawn Object
            SpawnObject();
        } 
        //InvokeRepeating("SpawnObject",spawnTime * Time.deltaTime, spawnTime * Time.deltaTime);
    }

    void PlayMusic(){
        audioSource = GetComponent<AudioSource>();
        bgMusic = Resources.Load<AudioClip>(_musicName);
        audioSource.clip = bgMusic;
        audioSource.Play(0);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException ();
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException ();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException ();
    }
    void SpawnObject(){

        if (Random.Range(0f, 1f) >= 0.5)
        {
            Instantiate(plate, new Vector3(0,0,-1), Quaternion.identity);
        } else
        {
            Instantiate(bag, new Vector3(0,0,-1), Quaternion.identity);
        }

        
        /* GameObject spawnObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        spawnObject.transform.position = new Vector3(0,0,-1);*/
    }
}
