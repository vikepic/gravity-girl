using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingController : StateController {

    [SerializeField]
    GameObject gravityObjectPrefab;

    GameObject playerGravityObject;

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

            playerGravityObject.AddComponent<
                PlayerGravityObjectController>();
        }

        playerGravityObject.SetActive(true);
    }

    private void OnDisable()
    {
        if (playerGravityObject != null)
        {
            playerGravityObject.SetActive(false);
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
