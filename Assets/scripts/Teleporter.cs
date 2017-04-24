using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {
    [SerializeField]
    bool isExterior;
    [SerializeField]
    GameObject position, _renderer;
    [SerializeField]
    PlayerManager pm;
    [SerializeField]
    InPlanetController pc;
    [SerializeField]
    Sprite space;

    // Geri, please erase this commented code
    // if target is not being used
    //   private Transform target;
	//// Use this for initialization
	//void Start () {
    //       target = position.transform;
    //   }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!isExterior)
            {
                GameObject temp = other.transform.parent.gameObject; 
                temp.transform.position = position.transform.position;
                temp.transform.localScale = new Vector3(1,1,1);
                _renderer.GetComponent<SpriteRenderer>().sprite = space;
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
