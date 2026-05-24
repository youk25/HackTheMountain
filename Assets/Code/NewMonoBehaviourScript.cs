using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private float movementSpeed = 10f;
    private Rigidbody2D rb;
    private int groundContactCount = 0;
    private bool isGrounded => groundContactCount > 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, movementSpeed);
        }

        float movementHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(movementHorizontal * movementSpeed, rb.velocity.y);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && IsTouchingBelow(collision))
        {
            groundContactCount++;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && IsTouchingBelow(collision))
        {
            groundContactCount--;
        }
    }

    private bool IsTouchingBelow(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if ( contact.normal.y > 0.5f)
            {
                return true;
            }
        }
        return false;
    }
}
