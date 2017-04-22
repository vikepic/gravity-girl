using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField]
    GameObject camFollowP;
    [SerializeField]
    float smooth;
    /*[SerializeField]
    float lx1;
    [SerializeField]
    float lx2;
    [SerializeField]
    float ly1;
    [SerializeField]
    float ly2;*/
    private float iniZ;
    // Use this for initialization
    void Start () {
        iniZ = transform.position.z;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = Vector3.Lerp(transform.position, new Vector3(camFollowP.transform.position.x, camFollowP.transform.position.y, iniZ), smooth);
        /*if (transform.position.x > lx1) transform.position = new Vector3(lx1, transform.position.y, transform.position.z);
        else if (transform.position.x < lx2) transform.position = new Vector3(lx2, transform.position.y, transform.position.z);
        if (transform.position.y > ly1) transform.position = new Vector3(transform.position.x, ly1, transform.position.z);
        else if (transform.position.y < ly2) transform.position = new Vector3(transform.position.x, ly2, transform.position.z);*/
    }
}
