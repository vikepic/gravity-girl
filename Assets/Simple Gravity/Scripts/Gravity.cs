using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gravity : MonoBehaviour {

	public bool reverseForce = false;

	public float strength = 50f;
	public float strengthExponent = 1.1f;
	public bool scaleStrengthOnMass = false;

	public float range = 50f;

	public string targetTag = "";
	public List<Rigidbody> objectsInRange;

	private Transform _transform;
	private Rigidbody _rigidbody;

	void Start()
	{
		_transform = transform;
		if (GetComponent<Rigidbody>() != null)
		{
			_rigidbody = GetComponent<Rigidbody>();
		}
		else if(scaleStrengthOnMass)
		{
			Debug.LogError("Gravity - ScaleStrengthOnMass is on but rigidbody missing");
		}
		SphereCollider c = gameObject.AddComponent<SphereCollider>();
		c.radius = range;
		c.isTrigger = true;
	}

	void OnTriggerEnter(Collider c)
	{
		if(c.tag == targetTag)
		{
			if(c.GetComponent<Rigidbody>() != null)
			{
				objectsInRange.Add(c.GetComponent<Rigidbody>());
			}
			else
			{
				Debug.LogWarning("Gravity - Tagged object has no rigidbody");
			}
		}
	}

	void OnTriggerExit(Collider c)
	{
		if(c.tag == targetTag)
		{
			if(c.GetComponent<Rigidbody>() != null)
			{
				objectsInRange.Remove(c.GetComponent<Rigidbody>());
			}
		}
	}

	void FixedUpdate () 
	{
		float forceMultiplier;
		Vector3 forceDirection;
		foreach(Rigidbody a in objectsInRange)
		{
			forceMultiplier = (-strength / Mathf.Pow(Mathf.Max(Vector3.Distance(_transform.position,a.position),1f),strengthExponent));
			if(scaleStrengthOnMass)
			{
				if (GetComponent<Rigidbody>() != null)
				{
					forceMultiplier *= _rigidbody.mass;
				}
			}
			if(reverseForce)
			{
				forceMultiplier *= -1;
			}
			forceDirection = (a.position - _transform.position).normalized;
			a.AddForce(forceDirection * forceMultiplier);
		}
	}
	void OnDrawGizmosSelected()
	{
		Gizmos.color = (new Color(0f,0f,1f));
		Gizmos.DrawWireSphere(_transform.position,range);
	}
}
