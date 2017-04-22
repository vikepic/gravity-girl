using UnityEngine;
using System.Collections;

public class DEMORandomKick : MonoBehaviour {

	private GameObject[] goList;
	public string targetTag = "";
	public float kickStrength = 100f;
	public Vector2 GUIOffset = new Vector2(10f,10f);

	void Start () 
	{
		goList = GameObject.FindGameObjectsWithTag(targetTag);
	}

	void OnGUI()
	{
		if(GUI.Button(new Rect(GUIOffset.x,GUIOffset.y,180,60),"Random Kick"))
		{
			foreach(GameObject a in goList)
			{
				a.GetComponent<Rigidbody>().AddForce(Random.onUnitSphere * kickStrength);
			}
		}
	}
}
