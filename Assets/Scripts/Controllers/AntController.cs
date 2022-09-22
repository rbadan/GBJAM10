using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntController : MonoBehaviour
{
    [SerializeField]
    private Ant thisAnt;

    //Movement parameters
    [SerializeField] private Rigidbody2D antRB;
    [SerializeField] private float antSpeed, antSpeedUp;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform grabSocket;
    private Vector2 antVelocity;
    private bool isGrounded;
    private bool upInput;
    private bool grabInput;
    private bool alreadyHolding;

    [SerializeField] private Collider2D thisCol, otherCol;
    
    

    //visuals
    [SerializeField]
    private GameObject activeMarker;
    private Block block;
    private Transform _ant;

    private void Awake()
    {
        isGrounded = true;
    }
    void Update()
    {
        if(GameController.GC.GetCurrentAnt() == thisAnt)
        {
            activeMarker.SetActive(true);
            switch (thisAnt)
            {
                case Ant.Ant1:
                    Walk();
                    if (Input.GetKeyDown(KeyCode.Z) && isGrounded)
                        grabInput = true;
                    break;
                case Ant.Ant2:
                    Walk();
                    if (Input.GetKeyDown(KeyCode.W) && isGrounded)
                        upInput = true;
                    if (Input.GetKeyDown(KeyCode.Z) && isGrounded)
                        grabInput = true;
                    break;
            }
        }
        else
        {
            activeMarker.SetActive(false);
        }
        if(thisAnt == Ant.Ant2)
        {
            Debug.Log(isGrounded);
        }
    }

    private void FixedUpdate()
    {
        Physics2D.IgnoreCollision(thisCol, otherCol);
        if (upInput)
        {
            if (thisAnt == Ant.Ant2)
            {
                upInput = false;
                Jump();
            }
        }

        if (grabInput)
        {
            grabInput = false;
            if (thisAnt == Ant.Ant1)
            {
                if (alreadyHolding)
                {
                    PlaceBlock();
                }
                else
                {
                    GrabBlock();
                }    
            }
            else
            {
                if (alreadyHolding)
                {
                    PlaceAnt();
                }
                else
                {
                    GrabAnt();
                }
            }
        }

        // i know, i know - bad code again
        if(antRB.velocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else if(antRB.velocity.x > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
    private void Walk()
    {
        var xSpeed = Input.GetAxisRaw("Horizontal") * antSpeed;
        Debug.Log(xSpeed);
        antVelocity = new Vector2 (xSpeed, antVelocity.y);

        antRB.velocity = antVelocity;        
    }

    private void Jump()
    {
        antRB.AddRelativeForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
    }

    private void GrabBlock()
    {
        block.SetIsGrabbed(true);
        block.transform.parent = grabSocket;
        alreadyHolding = true;
        block = null;
    }
    private void PlaceBlock()
    {
        alreadyHolding = false;
        block = grabSocket.GetChild(0).GetComponent<Block>();
        block.SetIsGrabbed(false);
        block.gameObject.transform.parent = null;


        if (!grabSocket.GetChild(0))
        {
            block = null;
        }
    }

    private void GrabAnt()
    {
        _ant.parent = grabSocket;
        alreadyHolding = true;
        _ant.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        _ant = null;
    }

    private void PlaceAnt()
    {
        alreadyHolding = false;
        _ant = grabSocket.GetChild(0);
        _ant.gameObject.transform.parent = null;
        _ant.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;

        if (!grabSocket.GetChild(0))
        {
            block = null;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (GameController.GC.GetCurrentAnt() == Ant.Ant1)
        {
            if (collision.CompareTag("Block"))
            {
                 block = collision.GetComponent<Block>();
            }
        }
        else
        {
            if (collision.CompareTag("Ant"))
            {
                _ant = collision.transform;
            }
        }
    }
}
