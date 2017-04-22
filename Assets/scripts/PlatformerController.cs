using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerController : StateController
{
    [SerializeField]
    float speed, jumpF, stairSpeed;
    [SerializeField]
    Rigidbody2D myRb;
    [SerializeField]
    GameObject groundP, stairCollider;

    private bool canJump = true;
    private bool canDis = true;
    private bool stairs = false;
    private bool grounded = false;
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        grounded = checkGround();
        myRb.velocity = new Vector2(Input.GetAxis("Horizontal")*speed, myRb.velocity.y);
        if(Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            canJump = false;
            myRb.AddForce(Vector2.up * jumpF, ForceMode2D.Impulse);
            StartCoroutine(enableJump());
        }
        if (grounded && Input.GetKeyDown(KeyCode.W) && canJump)
        {
            canJump = false;
            myRb.AddForce(Vector2.up * jumpF, ForceMode2D.Impulse);
            StartCoroutine(enableJump());
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) && stairs)
        {
            myRb.velocity = new Vector2(0, stairSpeed);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) && grounded && canDis)
        {
            stairCollider.SetActive(true);
            canDis = false;
            StartCoroutine(disableStairCol());
        }
    }

    IEnumerator enableJump()
    {
        yield return new WaitForSeconds(0.2f);
        canJump = true;
    }

    IEnumerator disableStairCol()
    {
        yield return new WaitForSeconds(0.1f);
        stairCollider.SetActive(false);
        canDis = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "stair")
        {
            stairs = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "stair")
        {
            stairs = false;
        }
    }

    private bool checkGround()
    {
        return Physics2D.Raycast(groundP.transform.position, Vector2.down, 0.01f);
    }
}
