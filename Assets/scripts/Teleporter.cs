using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {
    [SerializeField]
    bool isExterior;
    [SerializeField]
    GameObject position, renderer;
    [SerializeField]
    PlayerManager pm;
    [SerializeField]
    InPlanetController pc;
    [SerializeField]
    Sprite space;

    private Transform target;
	// Use this for initialization
	void Start () {
        target = position.transform;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!isExterior)
            {
                GameObject temp = other.transform.parent.gameObject; 
                temp.transform.position = position.transform.position;
                temp.transform.localScale = new Vector3(1,1,1);
                renderer.GetComponent<SpriteRenderer>().sprite = space;
                InPlanetController.faceRight = true;
                CameraFollowSmooth.goSpace();
                pm.ExitPlanet();
            }
            else
            {
                pc.enterLevel();
            }
        }
    }
}
