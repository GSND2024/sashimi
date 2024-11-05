using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Add this to manage scenes


public class PlayerMovement1 : MonoBehaviour
{
    public float speed;
    public float xInput;
    public float yInput;
    public float glideHorizontalSpeed = 6f; // Speed during gliding (horizontal)
    public float glideSpeed = 2f;
    public float normalGravityScale = 20;
    public float waterGravityScale = 0;
    public float jumpSpeed = 50;
    public float isGliding = 0;

    public Rigidbody2D body;

    private bool isLookingRight = false;
    private bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }
        xInput = Input.GetAxis("Horizontal");
        if (xInput > 0) { isGliding = 1; }
        if (xInput <0) { isGliding = -1; }
        if (Mathf.Abs(xInput) > 0)
        {
            body.velocity = new Vector2(xInput * speed, body.velocity.y);
        }
        if (isLookingRight && xInput < 0f || !isLookingRight && xInput > 0f)
        {
            Flip();
        }
        if (!canMove)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                StartGliding();
            }
            else
            {
                StopGliding();
            }
        }
        else
        {
            yInput = Input.GetAxis("Vertical");

            if (Mathf.Abs(yInput) > 0)
            {
                body.velocity = new Vector2(body.velocity.x, yInput * speed);
            }

        }


    }

    private void Flip() {
        isLookingRight = !isLookingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void StartGliding()
    {
        body.gravityScale = 0.5f; // Reduce gravity to simulate gliding
        //body.velocity = new Vector2(body.velocity.x, -glideSpeed); // Glide down slowly
        body.velocity = new Vector2(glideHorizontalSpeed * isGliding, -glideSpeed);
        
    }

    private void StopGliding()
    {
        body.gravityScale = normalGravityScale; // Restore normal gravity
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("water"))
        {
            //Debug.Log("Player entered the background area");
            body.gravityScale = waterGravityScale;
            canMove = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
       // Check if the player exits the background area
        if (other.gameObject.CompareTag("water"))
        {
            //Debug.Log("Player exited the background area");
            body.velocity = new Vector2(body.velocity.x, yInput * jumpSpeed);
            body.gravityScale = normalGravityScale;
            canMove = false;
        }
    }

    private void ReloadScene()
    {
        // Reload the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

}