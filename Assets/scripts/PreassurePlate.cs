using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreassurePlate : MonoBehaviour {
    [SerializeField]
    MoveUp[] targetWallScript;
    [SerializeField]
    Sprite on;
    [SerializeField]
    Sprite off;
    int elements = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.tag);
        if (other.tag == "Player" || other.tag == "suit")
        {
            if(elements == 0) AudioManager.Instance.PlaySound(AudioManager.SFX.PressurePlate);
            ++elements;
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log(other.tag);
        if (other.tag == "Player" || other.tag == "suit")
        {
            for (int i = 0; i < targetWallScript.Length; i++)
            {
                targetWallScript[i].active = true;
            }
            GetComponent<SpriteRenderer>().sprite = on;
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
            --elements;
            if(elements == 0)GetComponent<SpriteRenderer>().sprite = off;
        }
    }
}
