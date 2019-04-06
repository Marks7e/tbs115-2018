using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public Rigidbody2D rb;
	public int movespeed = 2;
	public int jumpHeight = 5;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
		/* Update one jump only later */
		if(Input.GetKey(KeyCode.LeftArrow)){
			rb.velocity = new Vector2(-movespeed, rb.velocity.y);
		}

		if(Input.GetKey(KeyCode.RightArrow)){
			rb.velocity = new Vector2(movespeed, rb.velocity.y);
		}

		if(Input.GetKey(KeyCode.Space)){
			rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
		}
	}
}
