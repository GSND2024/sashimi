using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private bool inWater = true;

    public GameObject ab1;

    public GameObject player;

    public GameObject nextLevel;

    bool cutScene = false;

    public bool isG = false;

    public Camera cam;

    public GameObject settingMenu;

    // Start is called before the first frame update
    void Start()
    {
          PlayerPrefs.SetInt("levelCounter", 0);
          int roomCounter = PlayerPrefs.GetInt("roomCounter1");

          //Debug.Log(roomCounter);

            if (roomCounter == 0)
            {
                cam.transform.position = new Vector3(0f,0f,-10f);
                player.transform.position = new Vector2(-7.38f, -1.87f);
            }
            else if (roomCounter == 1)
            {
                Debug.Log(101);
                cam.transform.position = new Vector3(17.78f,0f,-10f);
                player.transform.position = new Vector2(9.89f, -0.69f);
            }
            else if (roomCounter == 2)
            {
                cam.transform.position = new Vector3(35.55f,3.5f,-10f);
                player.transform.position = new Vector2(28.89f, -0.20f);
            }
            else if (roomCounter == 3)
            {
                cam.transform.position = new Vector3(53.33f,3.5f,-10f);
                player.transform.position = new Vector2(46.68f, -0.01f);
            }
        
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
        if (isLookingRight && xInput < 0f || !isLookingRight && xInput > 0f && cutScene == false )
        {
            Flip();
        }
        if (!inWater)
        {
            
            if (Input.GetKey(KeyCode.Space))
            {
                //StartGliding();
            }
            else
            {
                //StopGliding();
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
        if(transform.childCount == 3 && isG == false)
        {
            transform.GetChild(2).AddComponent<Rigidbody2D>();
            transform.GetChild(2).SetParent(null);      
        }

         if (Input.GetKeyDown(KeyCode.Escape))
        {
            settingMenu.SetActive(!settingMenu.activeSelf);
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
        isG = true;
        body.gravityScale = 0.5f; // Reduce gravity to simulate gliding
        //body.velocity = new Vector2(body.velocity.x, -glideSpeed); // Glide down slowly
        body.velocity = new Vector2(glideHorizontalSpeed * isGliding, -glideSpeed);
        
    }

    private void StopGliding()
    {
        isG = false;
        body.gravityScale = normalGravityScale; // Restore normal gravity
    }

    private void OnTriggerStay2D(Collider2D other) {
        inWater = true;
        body.gravityScale = waterGravityScale;
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        //cutscene
       if (other.gameObject.CompareTag("ab1"))
        {
            ab1.SetActive(false);

            nextLevel.SetActive(true);

            cutScene = true;

            player.transform.position = new Vector2(68.35f, 0.49f);

            StartCoroutine(moveTo(player.transform, 3.0f));

        }

        if (other.gameObject.CompareTag("nextLevel"))
        {
             SceneManager.LoadScene("Chapter2");
        }

        if (other.gameObject.CompareTag("water")) {
            isG = false;
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
            inWater = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)

    {
        if (other.gameObject.CompareTag("ground") && inWater == false)
        {
           ReloadScene();
        }

        if (other.gameObject.CompareTag("rock") && isG)
        {
            Destroy(other.gameObject.GetComponent<Rigidbody2D>());
            other.transform.SetParent(transform);
        }

    }

    bool isMoving = false;
    IEnumerator moveTo(Transform fromPosition, float duration)
{
    //Make sure there is only one instance of this function running
    if (isMoving)
    {
        yield break; ///exit if this is still running
    }
    isMoving = true;

    float counter = 0;

    //Get the current position of the object to be moved
    Vector3 startPos = fromPosition.position;
    Vector3 toPosition = fromPosition.position;
    toPosition.x += 10;
   


    while (counter < duration)
    {
        counter += Time.deltaTime;
        fromPosition.position = Vector3.Lerp(startPos, toPosition, counter / duration);
        yield return null;
    }
    isMoving = false;
}

    private void ReloadScene()
    {
        SceneManager.LoadScene("Chapter1");
      
    }

}