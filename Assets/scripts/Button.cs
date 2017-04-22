﻿using System.Collections;
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

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "suit")
        {
            for (int i = 0; i < targetWallScript.Length; i++)
            {
                targetWallScript[i].active = true;
            }
            GetComponent<SpriteRenderer>().color = new Color(141.0f/255.0f, 1.0f, 105.0f / 255.0f);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "suit")
        {
            for (int i = 0; i < targetWallScript.Length; i++)
            {
                targetWallScript[i].active = false;
            }
            GetComponent<SpriteRenderer>().color = new Color(1.0f, 105.0f / 255.0f, 105.0f / 255.0f);
        }
    }
}
