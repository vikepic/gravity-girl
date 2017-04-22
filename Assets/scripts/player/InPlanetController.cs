using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InPlanetController : StateController
{
    [SerializeField]
    float rotationSpeed = 3f;

    PivotController pivotController;
    // This state makes no sense if we do not have a valid
    // pivot. This boolean makes sure that we only perform actions
    // if we have an assigned pivot. It is quite similar
    // to check wether pivot == null or not, but cleaner. 
    bool hasValidPivot = false;


	void OnDisable () {
        hasValidPivot = false;
    }

    void ResetPivot(GameObject newPivot)
    {
        pivotController = newPivot.GetComponent<PivotController>();
        transform.localRotation = Quaternion.identity;
        hasValidPivot = true;
    }
	
	void Update () {
        if (!hasValidPivot)
            return;

        if (Input.GetKey(KeyCode.Space))
        {
            // This gets us outside this state
            gameObject.SendMessage("ExitPlanet");
            pivotController.FreeEntity(gameObject);
            return;
        }

        if (Input.GetKey(KeyCode.K))
        {
            // This gets us outside this state
            gameObject.SendMessage("EnterPlanet");
            pivotController.FreeEntity(gameObject);
            return;
        }


        if (Input.GetAxis("Horizontal") != 0)
        {
            pivotController.Rotate(rotationSpeed *
                -Input.GetAxis("Horizontal"));
        }        
	}
}
