using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {
    [SerializeField]
    MoveUp[] targetWallScript;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < targetWallScript.Length; i++)
            {
                targetWallScript[i].enabled = true;
            }
            GetComponent<SpriteRenderer>().color = new Color(141.0f/255.0f, 1.0f, 105.0f / 255.0f);
        }
        GetComponent<Button>().enabled = false;
    }
}
