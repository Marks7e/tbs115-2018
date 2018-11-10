using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBehavior : MonoBehaviour
{


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            Debug.Log("+1");
        }
    }
}
