using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingController : StateController {

    [SerializeField]
    GameObject gravityObjectPrefab;

    GameObject playerGravityObject;
    GravityObjectController playerGravityObjectController;

    [Header("Jetpack")]
    [SerializeField]
    float jetpackAngle = 45f;
    [SerializeField]
    float jetpackForce = 100f;
    [SerializeField]
    float initialJetpackImpulse = 400f;
    [SerializeField]
    float jumpAngle = 45f;
    [SerializeField]
    float steeringForce = 5;

    // This is true each time we disable the component
    bool firstActivation = true;

    private void OnEnable()
    {
        // We build the playerGravityObject
        if (playerGravityObject == null)
        {
            playerGravityObject =
                Instantiate(gravityObjectPrefab,
                transform.position,
                transform.rotation,
                gameObject.transform);

            playerGravityObjectController = 
                playerGravityObject.AddComponent<
                GravityObjectController>();
        }

        playerGravityObject.SetActive(true);
        firstActivation = true;
    }

    private void OnDisable()
    {
        if (playerGravityObject != null)
        {
            playerGravityObject.SetActive(false);
        }
    }

    void Start()
    {

    }

    void FixedUpdate()
    {
        if (playerGravityObject == null)
            return;

        Vector2 force = new Vector2();

        if (Input.GetKey(KeyCode.Space))
        {
            Vector2 verForce = new Vector2(1, 1);
            if (firstActivation)
            {
                verForce *= initialJetpackImpulse;
                firstActivation = false;
            }
            else
            {
                verForce *= jetpackForce;
            }

            // I rotate the force angle now
            verForce = 
                transform.rotation *
                Quaternion.Euler(new Vector3(0, 0, jetpackAngle)) *
                verForce;

            force += verForce;
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            float rotAngle = 90 * -Input.GetAxis("Horizontal");

            Vector2 horForce = new Vector2(1,1);
            horForce *= steeringForce;

            // TODO angle is hardcoded
            horForce = transform.rotation *
                Quaternion.Euler(new Vector3(0, 0, rotAngle)) * 
                    horForce;

            force += horForce;
        }

        //Debug.Log("FlyingController adding force to player: " + 
        //    force);
        if(force.sqrMagnitude > 0)
            playerGravityObjectController.AddForce(force);
    }
}
