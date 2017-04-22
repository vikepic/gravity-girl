using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour {
    [SerializeField]
    Transform s1, s2;
    [SerializeField]
    float smooth;
    [SerializeField]
    bool targetS1 = true;
    Transform target;
	// Use this for initialization
	void Start () {
        if (!targetS1) target = s2;
        else target = s1;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (transform.position != target.transform.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, smooth);
        }
        else
        {
            targetS1 = !targetS1;
            if (targetS1) target = s1;
            else target = s2;
        }
    }
}
