using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
     //Change the numbers to change jumping or moving speed

    public float jumpSpeed = 5000f;

    Rigidbody rb;
    bool canJump;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //Checking if the player is on the floor. If so, player can jump, else he cannot. The floor or any object from where you want the player to be able to jump must be tagged as "Floor"

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
            canJump = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
            canJump = false;
        }
    }

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontal, 0, vertical) * (speed * Time.deltaTime));
        //Movement Script

        //if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        //{
        //    rb.AddForce(0f, 0f, speed * Time.deltaTime);
        //}

        //if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        //{
        //    rb.AddForce(0f, 0f, -speed * Time.deltaTime);
        //}

        //if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        //{
        //    rb.AddForce(-speed * Time.deltaTime, 0f, 0f);
        //}

        //if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        //{
        //    rb.AddForce(speed * Time.deltaTime, 0f, 0f);
        //}

        // Jumping Script. Player can jump only when canJump is true (means when the player is on the floor)
        if (Input.GetKey(KeyCode.Space) & canJump)
        {
            rb.AddForce(0f, jumpSpeed * Time.deltaTime, 0f);
        }

    }
}
