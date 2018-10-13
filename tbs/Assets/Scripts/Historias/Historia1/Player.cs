using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public Rigidbody2D rb;
	public int movespeed = 2;
	public int jumpHeight = 5;
	//public Transform groundCheck;
	//public float groundCheckRadius;
	//public LayerMask whatIsGround;
	//private bool onGround = true;
	//private Animator anim;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		//anim = GetComponent<Animator>();
	}
	
	void FixedUpdate () {
		/* Update one jump only later */
		///onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius,whatIsGround);
		if(Input.GetKey(KeyCode.LeftArrow)){
			rb.velocity = new Vector2(-movespeed, rb.velocity.y);
			//anim.SetBool("Run",true);
			//Page 125 112 animaciones reflejadas.
		}

		if(Input.GetKey(KeyCode.RightArrow)){
			rb.velocity = new Vector2(movespeed, rb.velocity.y);
			//anim.SetBool("Run",true);
		}

		//Debug.Log("En tierra firme? " + onGround);


		if(Input.GetKey(KeyCode.Space)){
			//Debug.Log("En tierra firme? " + onGround);
			rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
		}
	}
}
