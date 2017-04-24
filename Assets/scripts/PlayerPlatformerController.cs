using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : MonoBehaviour {

    PlatformerController pc;

    void Update()
    {
        SyncPlayer();
    }

    void SyncPlayer()
    {
        // Very VERY ugly stuff where i sync the parent's position
        // with mine in order to create a gravitational pull effect
        // on the player
        transform.parent.position = transform.position;
        transform.localPosition = Vector3.zero;
        transform.parent.rotation = transform.rotation;
        transform.localRotation = Quaternion.identity;
    }

    private void Awake()
    {
        pc = PlatformerController.Instance;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "stair")
        {
            pc.stairs = true;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "suit")
        {
            pc.suitInRange = true;
        }
        if (other.tag == "teel") //aka teleporter...
        {
            //pm.ExitPlanet();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "stair")
        {
            pc.stairs = false;
        }
        if (other.tag == "suit")
        {
            pc.suitInRange = false;
        }
    }

}
