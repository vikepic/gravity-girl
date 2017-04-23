using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textMeshTyper : MonoBehaviour {
    [SerializeField]
    TextMeshPro tMesh;
    string[] tCharacter;
    [SerializeField]
    float timeChar;
    private bool active = false;
	// Use this for initialization
	void Start () {
        tCharacter = new string[tMesh.text.Length];
        for (int i = 0; i < tMesh.text.Length; i++)
        {
            tCharacter[i] = tMesh.text.Substring(i, 1);
        }
        tMesh.text = "";
    }
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !active)
        {
            active = false;
            StartCoroutine(typeText());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && !active)
        {
            tMesh.text = "";
        }
    }


    IEnumerator typeText()
    {
        for (int i = 0; i < tCharacter.Length; i++)
        {
            tMesh.text += tCharacter[i]; 
            yield return new WaitForSeconds(timeChar);
        }
    }
}
