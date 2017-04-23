using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textMeshTyper : MonoBehaviour {
    [SerializeField]
    TextMeshPro tMesh;
    [SerializeField]
    string tCharacter;
    // Use this for initialization
    void Start () {
        tMesh.text = "";
    }
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            tMesh.text = tCharacter;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            tMesh.text = "";
        }
    }
}
