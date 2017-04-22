using UnityEngine;
using System.Collections;

public class DEMOGUI2D : MonoBehaviour {

	public Gravity2D gravityComponent;
	public Vector2 GUIOffset = new Vector2(10f,10f);
	public float objectsDrag = 0f;
	private float oldObjectsDrag = 0f;
	private bool gravity = false;
	private bool oldGravity = false;
	private GameObject[] goList;
	public string targetTag = "Grav";
	public bool hideBallOptions = false;
	void Start()
	{
		goList = GameObject.FindGameObjectsWithTag(targetTag);
	}

	void Update () 
	{
		if(!hideBallOptions)
		{
			if (objectsDrag != oldObjectsDrag)
			{
				foreach(GameObject a in goList)
				{
					if (a.GetComponent<Rigidbody2D>() != null)
					{
						a.GetComponent<Rigidbody2D>().drag = objectsDrag;
					}
				}
				oldObjectsDrag = objectsDrag;
			}
			if (gravity != oldGravity)
			{
				foreach(GameObject a in goList)
				{
					if (a.GetComponent<Rigidbody2D>() != null)
					{
						if (gravity)
						{
							a.GetComponent<Rigidbody2D>().gravityScale = 1;
						}
						else
						{
							a.GetComponent<Rigidbody2D>().gravityScale = 0;
						}
					}
				}
				oldGravity = gravity;
			}
		}
	}

	void OnGUI()
	{
		if (gravityComponent != null)
		{
			//Draw the demo Control Panel in top left corner.
			GUI.Box(new Rect(0+GUIOffset.x,0+GUIOffset.y,298,162),"");
			GUILayout.BeginArea(new Rect(2+GUIOffset.x,2+GUIOffset.y,296,160));

			GUILayout.BeginHorizontal();
			GUILayout.Label("Reverse Force",GUILayout.Width(70));
			gravityComponent.reverseForce = GUILayout.Toggle(gravityComponent.reverseForce,"");
			if(!hideBallOptions)
			{
				GUILayout.Label("Gravity",GUILayout.Width(70));
				gravity = GUILayout.Toggle(gravity,"");
			}
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			GUILayout.Label("Strength",GUILayout.Width(70));
			gravityComponent.strength = GUILayout.HorizontalSlider(gravityComponent.strength,0f,300f,GUILayout.Width(175));
			GUILayout.Label(gravityComponent.strength.ToString("F"));
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			GUILayout.Label("Exponent",GUILayout.Width(70));
			gravityComponent.strengthExponent = GUILayout.HorizontalSlider(gravityComponent.strengthExponent,0.1f,2f,GUILayout.Width(175));
			GUILayout.Label(gravityComponent.strengthExponent.ToString("F"));
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			GUILayout.Label("Range",GUILayout.Width(70));
			gravityComponent.range = GUILayout.HorizontalSlider(gravityComponent.range,5f,100f,GUILayout.Width(175));
			GUILayout.Label(gravityComponent.range.ToString("F"));
			GUILayout.EndHorizontal();
			if(!hideBallOptions)
			{
				GUILayout.BeginHorizontal();
				GUILayout.Label("Dampening",GUILayout.Width(70));
				objectsDrag = GUILayout.HorizontalSlider(objectsDrag,0.0f,2f,GUILayout.Width(175));
				GUILayout.Label(objectsDrag.ToString("F"));
				GUILayout.EndHorizontal();
			}

			GUILayout.EndArea();
		}
	}
}
