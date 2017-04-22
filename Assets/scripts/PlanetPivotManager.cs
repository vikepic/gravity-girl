using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetPivotManager : MonoBehaviour {

    List<GameObject> pivots;
    PlanetController planetController;
    float planetRadius;
    bool initialized = false;

    public GameObject CreatePivot(float angle)
    {
        if(!initialized)
        {
            Debug.LogError("PlanetPivotManager not initialized yet." +
                " Try accessing it further down the execution pipeline.");
            return null;
        }

        GameObject newPivot =
            new GameObject();
        newPivot.transform.position = transform.position;
        // We give the new pivot the rotation specified by the
        // function args.
        newPivot.transform.Rotate(
            new Vector3(0, 0, angle)
            );
        newPivot.AddComponent<PivotController>().Init(planetController);
        newPivot.transform.parent = transform;
        pivots.Add(newPivot);
        return newPivot;
    }

    public void DeletePivot(GameObject pivot)
    {
        pivots.Remove(pivot);
        Destroy(pivot);
    }
    
    public void Init(PlanetController planetController)
    {
        this.planetController = planetController;
        pivots = new List<GameObject>();
        initialized = true;
    }
	
	void Update () {
		
	}
}
