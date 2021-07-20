using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed, jumpSpeed;
    private float movement;
    private int lastHeight, highScore;
    private Rigidbody2D rb;

    public int currentHeight;

    public Camera mainCamera;
    private float halfHeight, halfWidth;
    private Vector3 viewPos;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        currentHeight = lastHeight = highScore = 0;

        mainCamera = Camera.main;
        halfHeight = mainCamera.orthographicSize;
        halfWidth = mainCamera.aspect * halfHeight;
        viewPos = mainCamera.WorldToViewportPoint(this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal") * movementSpeed;

        if (movement < 0)
            this.GetComponent<SpriteRenderer>().flipX = true;
        else if (movement > 0)
            this.GetComponent<SpriteRenderer>().flipX = false;

        viewPos = mainCamera.WorldToViewportPoint(this.transform.position);
        wrapAround();
    }

    void FixedUpdate()
    {
        //set the velocity of the rigidbody to the calculated movement speed in the y direction
        rb.velocity = new Vector2(movement, rb.velocity.y);

        currentHeight = (int)this.transform.position.y;
        UpdateScore();
    }

    public int getHeight()
    {
        return currentHeight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name != "Moving")
        {
            if (rb.velocity.y <= 0)
            {
                this.GetComponent<Animator>().SetBool("isJumping", false);

                if (collision.gameObject.name == "Bounce")
                    rb.velocity = new Vector2(rb.velocity.x, jumpSpeed*2);
                else
                    rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);

                this.GetComponent<Animator>().SetBool("isJumping", true);


                if (collision.gameObject.name == "Vanish")
                    Destroy(collision.gameObject);
            }
        }
        else
        {
            this.GetComponent<Animator>().SetBool("isJumping", false);
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            this.GetComponent<Animator>().SetBool("isJumping", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        this.GetComponent<Animator>().SetBool("isJumping", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Break" && this.transform.position.y > collision.gameObject.transform.position.y && rb.velocity.y < 0)
            Destroy(collision.gameObject);
    }

    private void wrapAround()
    {

        if (viewPos.x > 1.0f) //If player is off the right hand side of the screen
        {
            //Set player to the left side of the screen
            this.transform.position -= new Vector3(halfWidth*2, 0f, 0f);
        }
        else if (viewPos.x < 0f) //If player is off the left hand side of the screen
        {
            //Set player to the right side of the screen
            this.transform.position += new Vector3(halfWidth*2, 0f, 0f);
        }

    }

    private void UpdateScore()
    {
        if (currentHeight > lastHeight)
        {

            IncrementScore.Instance.UpdateScore(currentHeight);
            lastHeight = currentHeight;

            if (currentHeight > highScore)
            {
                highScore = currentHeight;                IncrementScore.Instance.UpdateHighScore(currentHeight);
            }


        }
    }

    public void Reset()
    {
        Debug.Log("Resetting");
        this.transform.position = new Vector3(0f, -3.76f, -2f);
        IncrementScore.Instance.UpdateScore(0);
        currentHeight = 0;
        lastHeight = 0;
       
    }
}
