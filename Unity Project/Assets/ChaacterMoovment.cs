    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaacterMoovment : MonoBehaviour {

    private float jump;
    private bool canJump;
    public float moveSpeed;
    public float jumpHeight;
    void Start()
    {
        //GetComponent<Rigidbody2D>().drag = 0;
    }

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

        //GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, 0);
        //GetComponent<Rigidbody2D>().velocity += new Vector2(0, jumpHeight * jump);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(moveSpeed, jumpHeight * jump), ForceMode2D.Impulse);


    }
}
