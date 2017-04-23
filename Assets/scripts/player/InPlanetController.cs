using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InPlanetController : StateController
{
    [SerializeField]
    float rotationSpeed = 3f;
    [SerializeField]
    Transform sprite;

    PivotController pivotController;
    // This state makes no sense if we do not have a valid
    // pivot. This boolean makes sure that we only perform actions
    // if we have an assigned pivot. It is quite similar
    // to check wether pivot == null or not, but cleaner. 
    bool hasValidPivot = false;
    public static bool faceRight = true;

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

        if (Input.GetKey(KeyCode.P))
        {
            if (pivotController.CanPlanetBeEntered())
            {
                Vector3 enterPos = pivotController.GetPlanetEntryLocation();
                // This gets us outside this state
                gameObject.SendMessage("EnterPlanet", enterPos);
                pivotController.FreeEntity(gameObject);
                CameraFollowSmooth.goIn();
                transform.localScale = new Vector3(1, 1, 1);
                return;
            }
        }


        if (Input.GetAxis("Horizontal") != 0)
        {
            pivotController.Rotate(rotationSpeed * -Input.GetAxis("Horizontal"));
        }
        if (Input.GetAxis("Horizontal") > 0 && !faceRight)
        {
            flip();
        }
        else if (Input.GetAxis("Horizontal") < 0 && faceRight)
        {
            flip();
        }
    }

    public void enterLevel()
    {
        Vector3 enterPos = pivotController.GetPlanetEntryLocation();
        // This gets us outside this state
        gameObject.SendMessage("EnterPlanet", enterPos);
        pivotController.FreeEntity(gameObject);
    }

    public void flip()
    {
        faceRight = !faceRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
