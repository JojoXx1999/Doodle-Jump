using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private float halfHeight, halfWidth;
    private Vector2 currentLocation;
    private Camera mainCamera;
    private Rigidbody2D rb;
    public bool moveHorizontalNotVertical = true;
    public float verticalMoveRange, horizontalMoveRange;
    private float startingPosX, currentPosX, startingPosY, currentPosY;
    public float moveSpeed;

    private enum direction { LEFT, RIGHT, UP, DOWN};
    private direction currentDirection;

    Vector3 viewPos;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        halfHeight = mainCamera.orthographicSize;
        halfWidth = mainCamera.aspect * halfHeight;
        viewPos = mainCamera.WorldToViewportPoint(this.transform.position);

        startingPosX = currentPosX = this.transform.position.x;
        startingPosY = currentPosY = this.transform.position.y;

        rb = this.GetComponent<Rigidbody2D>();

        if (moveHorizontalNotVertical == true)
            currentDirection = direction.LEFT;
        else
            currentDirection = direction.UP;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        viewPos = mainCamera.WorldToViewportPoint(this.transform.position);
        if (moveHorizontalNotVertical == true)
        {
            currentPosX = this.transform.position.x;
            if (currentDirection == direction.LEFT)
            {
                if (viewPos.x > 0 && currentPosX > (startingPosX - horizontalMoveRange))
                {
                    rb.velocity = new Vector3(-moveSpeed, 0f, 0f);
                }
                else
                {
                    currentDirection = direction.RIGHT;
                    rb.velocity = new Vector3(0f, 0f, 0f);
                }
            }
            else if (currentDirection == direction.RIGHT)
            {
                if (viewPos.x < 1 && currentPosX < (startingPosX + horizontalMoveRange))
                {
                    rb.velocity = new Vector3(moveSpeed, 0f, 0f);
                }
                else
                {
                    currentDirection = direction.LEFT;
                    rb.velocity = new Vector3(0f, 0f, 0f);
                }
            }
        }
        else
        {
            currentPosY = this.transform.position.y;
            if (currentDirection == direction.UP)
            {
                if (currentPosY < (startingPosY + verticalMoveRange))
                {
                    rb.velocity = new Vector3(0f, moveSpeed, 0f);
                }
                else
                {
                    currentDirection = direction.DOWN;
                    rb.velocity = new Vector3(0f, 0f, 0f);
                }
            }
            else if (currentDirection == direction.DOWN)
            {
                if (currentPosY > (startingPosY - verticalMoveRange))
                {
                    rb.velocity = new Vector3(0f, -moveSpeed, 0f);
                }
                else
                {
                    currentDirection = direction.UP;
                    rb.velocity = new Vector3(0f, 0f, 0f);
                }
            }
        }
 
    }
}
