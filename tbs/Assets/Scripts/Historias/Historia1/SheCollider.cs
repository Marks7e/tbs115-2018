using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheCollider : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Yo")
        {
            Debug.Log("Perdiste!");
        }

    }
}
