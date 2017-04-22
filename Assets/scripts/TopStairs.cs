using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopStairs : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "stairDisable")
        {
            foreach (Collider2D c in GetComponents<Collider2D>())
            {
                c.enabled = false;
            }
            StartCoroutine(enableStair());
        }
    }

    IEnumerator enableStair()
    {
        yield return new WaitForSeconds(0.2f);
        foreach (Collider2D c in GetComponents<Collider2D>())
        {
            c.enabled = true;
        }
    }
}
