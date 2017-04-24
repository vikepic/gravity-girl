using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerController : StateController
{
    [SerializeField]
    float speed;
    [SerializeField]
    float jumpF;
    [SerializeField]
    float stairSpeed;
    [SerializeField]
    float suitMultiplier;
    [SerializeField]
    float suitJumpMult;

    [SerializeField]
    Rigidbody2D myRb;

    [SerializeField]
    GameObject groundP;
    [SerializeField]
    GameObject stairCollider;
    [SerializeField]
    GameObject suit;
    [SerializeField]
    GameObject suitPref;

    [SerializeField]
    GameObject playerPlatformer;
    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    Sprite suitS;
    [SerializeField]
    Sprite noSuitS;

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
    private bool suitOn = true;
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
        SuitOn = true;
        faceRight = true;
    }

    void Awake()
    {
        SuitOn = true;
    }

    void Update () {
        grounded = checkGround();
        //Debug.Log(grounded);
        if (!SuitOn)
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

        //if(Input.GetKeyDown(KeyCode.Space) && canJump)
        //{
        //    canJump = false;
        //    myRb.AddForce(Vector2.up * jumpF, ForceMode2D.Impulse);
        //    StartCoroutine(enableJump());
        //}

        if (grounded && Input.GetKeyDown(KeyCode.W) && canJump)
        {
            canJump = false;
            if(!SuitOn)myRb.AddForce(Vector2.up * jumpF, ForceMode2D.Impulse);
            else myRb.AddForce(Vector2.up * jumpF * suitMultiplier, ForceMode2D.Impulse);
            StartCoroutine(enableJump());
            AudioManager.Instance.PlaySound(AudioManager.SFX.Jump);
        }
        if (!grounded && Input.GetKeyDown(KeyCode.W) && SuitOn)
        {
            spriteRenderer.sprite = noSuitS;
            myRb.AddForce(Vector2.up * jumpF * suitJumpMult, ForceMode2D.Impulse);
            StartCoroutine(enableJump());
            SuitOn = false;
            suit.SetActive(false);
            suitCurrent = Instantiate(suitPref, transform.position, Quaternion.identity);
            AudioManager.Instance.PlaySound(AudioManager.SFX.JumpDouble);
        }
        if ( (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && stairs)
        {
            if (!SuitOn) myRb.velocity = new Vector2(0, stairSpeed);
            else myRb.velocity = new Vector2(0, stairSpeed*suitMultiplier);
            AudioManager.Instance.PlaySoundIfNotPlaying(AudioManager.SFX.Climb);
        }
        else
        {
            AudioManager.Instance.StopSound(AudioManager.SFX.Climb);
        }
        if ( (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && grounded && canDis)
        {
            stairCollider.SetActive(true);
            canDis = false;
            StartCoroutine(disableStairCol());
        }
        if (Input.GetKeyDown(KeyCode.O) && SuitOn)
        {
            spriteRenderer.sprite = noSuitS;
            SuitOn = false;
            suit.SetActive(false);
            suitCurrent = Instantiate(suitPref, transform.position, Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.O) && !SuitOn && suitInRange)
        {
            spriteRenderer.sprite = suitS;
            SuitOn = true;
            suit.SetActive(true);
            //NASTY NAST WAY OF SOLVING A BUG
            
            // wow dude
            // - @wextia
            suitCurrent.transform.position = new Vector3(-1000, -1000, 0);
            Destroy(suitCurrent, 0.1f);
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
        /*RaycastHit2D hit = Physics2D.Raycast(groundP.transform.position, Vector2.down, 0.01f);
        bool hitBool = false;
        if (hit.collider != null)
        {
            Debug.Log(hit.transform.tag);
        }*/
        bool ret = Physics2D.Raycast(groundP.transform.position, Vector2.down, 0.01f);
        if(!grounded && ret)
        {
            AudioManager.Instance.PlaySound(AudioManager.SFX.Land);
        }
        return ret;
    }

    private static PlatformerController _instance;
    public static PlatformerController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (PlatformerController)FindObjectOfType(typeof(PlatformerController));
            }
            return _instance;
        }
    }

    private bool SuitOn
    {
        get
        {
            return suitOn;
        }

        set
        {
            // Getting unsuited
            if(suitOn && !value)
            {
                //AudioManager.Instance.SwitchMusic(AudioManager.Music.Track1,
                //    AudioManager.Music.Track1NES);
                AudioManager.Instance.SetState(1);
                AudioManager.Instance.PlaySound(AudioManager.SFX.SuitOff);
            }
            // Getting suited
            else if (!suitOn && value)
            {
                //AudioManager.Instance.SwitchMusic(AudioManager.Music.Track1NES,
                //    AudioManager.Music.Track1);
                AudioManager.Instance.SetState(0);
                AudioManager.Instance.PlaySound(AudioManager.SFX.SuitOn);
            }

            suitOn = value;
        }
    }
}

