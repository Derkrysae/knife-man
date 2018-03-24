using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float maxSpeed = 3;
    public float speed = 50f;
    public float jumpPower = 200f;

    public bool grounded;

    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer mySpriteRenderer;

	void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        mySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}
	
	void Update () {
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));

        if (Input.GetAxis("Horizontal") > 0.1f)
            mySpriteRenderer.flipX = false;
        if (Input.GetAxis("Horizontal") < -0.1f)
            mySpriteRenderer.flipX = true;

        if (Input.GetButtonDown("Jump") && grounded)
            rb2d.AddForce(Vector2.up * jumpPower);
    }

    void FixedUpdate()
    {
        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.y = rb2d.velocity.y;
        easeVelocity.z = 0.0f;
        easeVelocity.x *= 0.75f;

        float h = Input.GetAxis("Horizontal");

        //Fake friction / easing the x speed of our player
        if (grounded)
            rb2d.velocity = easeVelocity;

        //Player movement
        rb2d.AddForce(Vector2.right * speed * h);

        //Limiting player movement speed
        if (rb2d.velocity.x > maxSpeed)
            rb2d.velocity.Set(maxSpeed, rb2d.velocity.y);
        if (rb2d.velocity.x < -maxSpeed)
            rb2d.velocity.Set(-maxSpeed, rb2d.velocity.y);
    }
}
