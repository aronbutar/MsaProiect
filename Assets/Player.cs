using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float gravity;
    public Vector2 velocity;
 
    public float groundHeight=10;
    public bool isGrounded = false;
    public float jumpVelocity = 20;
    public float maxXVelocity = 100;


    public float distance = 0;

    public bool isHoldingJump = false;
    public float maxHoldJumpTime = 0.4f;
    public float holdJumpTimer = 0.0f;

    public float acceleration = 10;
    public float maxAcceleration = 10;

    public float jumpGroundThreshold = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        float groundDistance = Mathf.Abs(pos.y - groundHeight);
        if (isGrounded|| groundDistance<=jumpGroundThreshold)
        {
            
            //KeyCode.Escape for Android
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                velocity.y = jumpVelocity;
                isHoldingJump = true;
                holdJumpTimer = 0;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false;
        }
    }
    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        if (!isGrounded)
        {

            if (isHoldingJump)
            {
                holdJumpTimer += Time.fixedDeltaTime;
                if(holdJumpTimer>=maxHoldJumpTime)
                {
                    isHoldingJump = false;
                }
            }

            pos.y += velocity.y*Time.fixedDeltaTime;
            if (!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }

            Vector2 rayOrigin = new Vector2(pos.x+0.7f, pos.y);
            Vector2 rayDirection = Vector2.up;
            float rayDistance =velocity.y*Time.fixedDeltaTime;
            RaycastHit2D hit2d = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
            if (hit2d.collider != null)
            {
                Ground ground = hit2d.collider.GetComponent<Ground>();
                if (ground != null)
                {
                    groundHeight = ground.groundHeight;
                    pos.y = groundHeight;
                    isGrounded = true;
                }
            }
            Debug.DrawRay(rayOrigin,rayDirection*rayDistance,Color.red);

            //if (pos.y <= groundHeight)
            //{
            //    pos.y = groundHeight;
            //    isGrounded = true;
            //    
            //}
        }

        distance += velocity.x * Time.fixedDeltaTime;

        if (isGrounded)
        {

            float velocityRation = velocity.x / maxXVelocity;
            acceleration = maxAcceleration * (1 - velocityRation);

            velocity.x += acceleration * Time.fixedDeltaTime;
            
            if (velocity.x >= maxAcceleration)
            {
                velocity.x = maxXVelocity;
            }



            Vector2 rayOrigin = new Vector2(pos.x + 0.9f, pos.y);
            Vector2 rayDirection = Vector2.up;
            float rayDistance = velocity.y * Time.fixedDeltaTime;
            RaycastHit2D hit2d = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
            if (hit2d.collider == null)
            {
                isGrounded = false;
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.yellow);
        }


        transform.position = pos;
    }
}
