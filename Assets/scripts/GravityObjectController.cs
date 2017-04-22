using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityObjectController : MonoBehaviour {
    
    // FIXME Hardcoded
    string planetTag = "GameController";

    Rigidbody2D myRigidbody;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
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
        //Debug.Log("PlayerGravityObjectController collided with " + 
        //    collision.gameObject.name);

        if (collision.gameObject.tag != planetTag)
            return;
        PlanetController pc =
            collision.gameObject.GetComponent<PlanetController>();

        Vector2 p2 = transform.position;
        Vector2 p1 = collision.transform.position;

        float angle = 
            Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * Mathf.Rad2Deg;

        //Debug.Log("PlayerGravityObjectController's " +
        //    "collision angle is: " + angle);

        GameObject pivot =
            pc.PlaceEntity(transform.parent.gameObject, angle - 90);

        RemoveForce();
        transform.parent.SendMessage("OnPlanetCollision", pivot);
    }

    void RemoveForce()
    {
        myRigidbody.velocity = Vector3.zero;
        myRigidbody.angularVelocity = 0;
    }

    public void AddForce(Vector2 force)
    {
        myRigidbody.AddForce(force);
    }
}
