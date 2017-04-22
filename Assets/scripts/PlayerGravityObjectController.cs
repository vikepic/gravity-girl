using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravityObjectController : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Uncomment to get some useful debugging info
        //Debug.Log("I collided with " + collision.gameObject.name +
        //    "!");
        // TODO
        transform.parent.SendMessage("OnPlanetCollision");
    }
}
