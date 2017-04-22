using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerController : StateController
{
    [SerializeField]
    float speed, jumpF, stairSpeed, suitMultiplier;
    [SerializeField]
    Rigidbody2D myRb;
    [SerializeField]
    GameObject groundP, stairCollider, suit, suitPref;
    [SerializeField]
    GameObject playerPlatformer;

    [HideInInspector]
    public bool canJump = true;
    [HideInInspector]
    public bool faceRight = true;
    [HideInInspector]
    public bool canDis = true;
    [HideInInspector]
    public bool stairs = false;
    [HideInInspector]
    public bool grounded = false;
    [HideInInspector]
    public bool suitOn = true;
    [HideInInspector]
    public bool suitInRange = false;

    private GameObject suitCurrent;

    private void OnDisable()
    {
        playerPlatformer.SetActive(false);
    }

    private void OnEnable()
    {
        playerPlatformer.SetActive(true);
    }

	void Update () {
        grounded = checkGround();
        if (!suitOn)
        {
            myRb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, myRb.velocity.y);
        }
        else
        {
            myRb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed * suitMultiplier, myRb.velocity.y);
        }

        if (myRb.velocity.x > 0 && !faceRight)
        {
            flip();
        }
        else if (myRb.velocity.x < 0 && faceRight)
        {
            flip();
        }

        if(Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            canJump = false;
            myRb.AddForce(Vector2.up * jumpF, ForceMode2D.Impulse);
            StartCoroutine(enableJump());
        }
        if (grounded && Input.GetKeyDown(KeyCode.W) && canJump)
        {
            canJump = false;
            if(!suitOn)myRb.AddForce(Vector2.up * jumpF, ForceMode2D.Impulse);
            else myRb.AddForce(Vector2.up * jumpF * suitMultiplier, ForceMode2D.Impulse);
            StartCoroutine(enableJump());
        }
        if ( (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && stairs)
        {
            if (!suitOn) myRb.velocity = new Vector2(0, stairSpeed);
            else myRb.velocity = new Vector2(0, stairSpeed*suitMultiplier);
        }
        if ( (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && grounded && canDis)
        {
            stairCollider.SetActive(true);
            canDis = false;
            StartCoroutine(disableStairCol());
        }
        if (Input.GetKeyDown(KeyCode.O) && suitOn)
        {

            suitOn = false;
            suit.SetActive(false);
            suitCurrent = Instantiate(suitPref, transform.position, Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.O) && !suitOn && suitInRange)
        {
            suitOn = true;
            suit.SetActive(true);
            Destroy(suitCurrent);
        }
    }

    public void flip()
    {
        faceRight = !faceRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
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

    private bool checkGround()
    {
        return Physics2D.Raycast(groundP.transform.position, Vector2.down, 0.01f);
    }
}
