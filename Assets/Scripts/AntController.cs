using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntController : MonoBehaviour
{
    public Rigidbody2D rb2d;

    private Vector2 movement;
    public float movementSpeed;

    public GameObject platformPlacementPositions;
    public Transform placementPosition;
    public Transform platformParent;

    private Transform targetPlatform;
    private Transform currentPlatform;
    private bool isLiftingPlatform;

    public SpriteRenderer spriteRenderer;

    private bool isFacingRight;

    public GameObject triggerObject;
    public float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.E))
        {
            platformPlacementPositions.SetActive(!platformPlacementPositions.activeInHierarchy);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentPlatform)
            {
                StartCoroutine(PlacePlatform());
            }
            else if (targetPlatform)
            {
                StartCoroutine(LiftPlatform());
            }
            
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(movement.x * movementSpeed, rb2d.velocity.y);

        if(rb2d.velocity.x < -0.1)
        {
            isFacingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(rb2d.velocity.x > 0.1)
        {
            isFacingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void Jump()
    {
        rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform") && !currentPlatform)
        {
            if(!targetPlatform || targetPlatform.transform.position.y < collision.transform.position.y)
            {
                targetPlatform = collision.transform;   
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (targetPlatform && collision.transform == targetPlatform)
        {
            targetPlatform = null;
        }
    }

    private IEnumerator LiftPlatform()
    {
        targetPlatform.position = platformParent.position;
        targetPlatform.parent = platformParent;
        targetPlatform.GetComponent<Rigidbody2D>().isKinematic = true;
        currentPlatform = targetPlatform;
        targetPlatform = null;

        yield return new WaitForSeconds(0.5f);
        isLiftingPlatform = true;
    }

    private IEnumerator PlacePlatform()
    {
        currentPlatform.parent = null;
        currentPlatform.GetComponent<Rigidbody2D>().isKinematic = false;
        isLiftingPlatform = false;

        yield return new WaitForSeconds(1f);

        currentPlatform.GetComponent<Rigidbody2D>().isKinematic = true;
        currentPlatform = null;
    }
}
