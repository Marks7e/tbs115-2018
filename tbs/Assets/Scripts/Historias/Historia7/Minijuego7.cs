using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minijuego7 : MonoBehaviour
{
    // Start is called before the first frame update

    // Objects spawn time.
    private float spawnTime = 2f;
    // Timer control flow
    private float timer = 0.0f;


    // Cube
    public GameObject plate;
    // Cylinder
    public GameObject bag;
    // Square
    // public GameObject Square;

    private enum Things
    {
        plate,
        bag
    };

    void Start()
    {
        //GameObject thing = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //thing.transform.position = new Vector3(0,0,-1);
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
