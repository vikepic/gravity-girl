using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour {
    [SerializeField]
    float distance = 1.0f;
    [SerializeField]
    float smoothUp = 0.02f;
    [SerializeField]
    float smoothDown = 0.001f;
    public bool active = false;
    private float target, originalPos;
	// Use this for initialization
	void Start () {
        target = transform.position.y + distance;
        originalPos = transform.position.y;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if (transform.position.y != target && active){
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, target), smoothUp);
        }
        else if (!active)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, originalPos), smoothDown);
        }
    }
}
