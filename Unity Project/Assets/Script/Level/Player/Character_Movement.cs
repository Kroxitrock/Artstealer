using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Movement : MonoBehaviour {
	
	private float jump;
	private bool canJump;
	public float moveSpeed;
	public float jumpHeight;
	
	// Update is called once per frame
	protected float OnMouseDown()
	{
		if (Input.GetMouseButtonDown(0))
		{
			return (float)1;
		}
		return (float)0;
	}
	void OnCollisionStay2D(Collision2D col)
	{
		canJump = true;
	}
	void Update()
	{
		if (canJump == true)
		{
			jump = OnMouseDown();
			canJump = false;
		}

		GetComponent<Rigidbody2D>().AddForce(new Vector2(moveSpeed, jumpHeight * jump), ForceMode2D.Impulse);
		
		
	}
}