using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textMeshTyper : MonoBehaviour {
    TextMesh tMesh;
    string tCharacter;
    // Use this for initialization
    void Start () {
        tMesh = GetComponent<TextMesh>();
        tCharacter = tMesh.text;
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
