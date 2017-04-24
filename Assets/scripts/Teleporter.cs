using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {
    [SerializeField]
    bool isExterior;
    [SerializeField]
    GameObject position;
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
                PlayerVisController.Instance._renderer.sprite = space;
                InPlanetController.faceRight = true;
                CameraFollowSmooth.goSpace();
                PlayerManager.Instance.ExitPlanet();
                GameObject tempPm = GameObject.Find("Player");
                tempPm.GetComponent<PlayerManager>().objectiveCompleted();
            }
            else
            {
                InPlanetController.Instance.enterLevel();
            }
        }
    }
}
