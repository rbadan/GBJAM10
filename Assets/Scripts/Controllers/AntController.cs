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
    [SerializeField] private float antSpeed;
    [SerializeField] private float jumpForce;
    private Vector2 antVelocity;
    private bool isGrounded;
    

    //visuals
    [SerializeField]
    private GameObject activeMarker;

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
                    GrabBlock();
                    PlaceBlock();
                    break;
                case Ant.Ant2:
                    Walk();
                    Jump();
                    GrabAnt();
                    PlaceAnt();
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
   
    private void Walk()
    {
        var xSpeed = Input.GetAxisRaw("Horizontal") * antSpeed;
        Debug.Log(xSpeed);
        antVelocity = new Vector2 (xSpeed, antVelocity.y);

        antRB.velocity = antVelocity;        
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.W))
        {
            antRB.AddRelativeForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void GrabBlock()
    {

    }
    private void PlaceBlock()
    {

    }

    private void GrabAnt()
    {

    }

    private void PlaceAnt()
    {
     
    }
}
