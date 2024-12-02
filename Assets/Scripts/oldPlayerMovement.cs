using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D body;
    public float xInput;
    public float yInput;
    private bool isLookingRight = false;
    private bool inWater = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!inWater) return;
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        if (Mathf.Abs(xInput) > 0) {
            body.velocity = new Vector2(xInput * speed, body.velocity.y);
        }
        if (Mathf.Abs(yInput) > 0) {
            body.velocity = new Vector2(body.velocity.x, yInput * speed);
        }
        if (isLookingRight && xInput < 0f || !isLookingRight && xInput > 0f)
        {
            Flip();
        }

        Debug.Log("here");
        if (inWater) {
            body.gravityScale = 0;
        } else if (!inWater) {
            body.gravityScale = 10;
        }
    }

    private void Flip() {
        isLookingRight = !isLookingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void OnTriggerStay2d(Collider2D other) {
        if (other.gameObject.CompareTag("water")) {
            inWater = true;
        }
    }

    /**
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("water"))
        {
            //Debug.Log("Player entered the background area");
            inWater = true;
        }
    }
    **/
    private void OnTriggerExit2D(Collider2D other)
    {
       // Check if the player exits the background area
        if (other.gameObject.CompareTag("water"))
        {
            //Debug.Log("Player exited the background area");
            inWater = false;
        }
    }

    
}