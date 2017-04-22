using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour {
    [SerializeField]
    float distance = 1.0f;
    [SerializeField]
    float smooth = 1.0f;
    private float target;
	// Use this for initialization
	void Start () {
        target = transform.position.y + distance;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (transform.position.y != target){
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, target), smooth);
        }
        else GetComponent<MoveUp>().enabled = false;
    }
}
