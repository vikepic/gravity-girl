using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotController : MonoBehaviour {

    PlanetController planetController;
    float planetRadius;
    bool initiated = false;


    public void Rotate(float speed)
    {
        if (!initiated)
            return;

        float rotation =
            speed * Time.deltaTime / planetRadius;

        // I have to rotate it negated
        transform.Rotate(
            new Vector3(0, 0, rotation)
            );
    }

    // Deletes himself securely after freeing entity
    public void FreeEntity(GameObject entity)
    {
        planetController.FreeEntity(entity);
    }

    public void Init(PlanetController planetController)
    {
        this.planetController = planetController;
        planetRadius = planetController.PlanetRadius;
        initiated = true;
    }

    public Vector3 GetPlanetEntryLocation()
    {
        return planetController.GetEntryLocation();
    }

    public bool CanPlanetBeEntered()
    {
        return planetController.CanBeEntered;
    }

}