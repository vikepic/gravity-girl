using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gravity2D : MonoBehaviour {

	public bool reverseForce = false;

	public float strength = 50f;
	public float strengthExponent = 1.1f;
	public bool scaleStrengthOnMass = false;

	public float range = 50f;

	public string targetTag = "";

	public List<Rigidbody2D> objectsInRange;

	private Transform _transform;
	private Rigidbody2D _rigidbody;

	void Start()
	{
		_transform = transform;
		if (GetComponent<Rigidbody2D>() != null)
		{
			_rigidbody = GetComponent<Rigidbody2D>();
		}
		else if(scaleStrengthOnMass)
		{
			Debug.LogError("Gravity - ScaleStrengthOnMass is on but rigidbody2D missing");
		}
		CircleCollider2D c = gameObject.AddComponent<CircleCollider2D>();
		c.radius = range;
		c.isTrigger = true;
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.tag == targetTag)
		{
			if(c.GetComponent<Rigidbody2D>() != null)
			{
                if (!objectsInRange.Contains(c.GetComponent<Rigidbody2D>()))
                {
                    objectsInRange.Add(c.GetComponent<Rigidbody2D>());
                    Debug.Log("Gravity2D: object " + c.gameObject.name + "entered the radius");
                }
			}
			else
			{
				Debug.LogWarning("Gravity - Tagged object has no rigidbody2D");
			}
		}
	}

	void OnTriggerExit2D(Collider2D c)
	{
		if(c.tag == targetTag)
		{
			if(c.GetComponent<Rigidbody2D>() != null)
			{
				objectsInRange.Remove(c.GetComponent<Rigidbody2D>());
                //Debug.Log("Gravity2D: " + c.gameObject.name + " exited the radius of " +
                //    gameObject.name);
			}
		}
	}

	void FixedUpdate () 
	{
		float forceMultiplier;
		Vector2 forceDirection;
		foreach(Rigidbody2D a in objectsInRange)
		{
            if (!a.gameObject.activeSelf)
            {
                continue;
            }
            //Debug.Log("Adding force to " + a.gameObject.name);
			forceMultiplier = (-strength / Mathf.Pow(Mathf.Max(Vector3.Distance(_transform.position,a.transform.position),1f),strengthExponent));
			if(scaleStrengthOnMass)
			{
				if (GetComponent<Rigidbody2D>() != null)
				{
					forceMultiplier *= _rigidbody.mass;
				}
			}
			if(reverseForce)
			{
				forceMultiplier *= -1;
			}
			forceDirection = (a.transform.position - _transform.position).normalized;
            //Debug.Log(Time.time + ": " +  forceMultiplier);
            a.AddForce(forceDirection * forceMultiplier);
		}
	}
}
