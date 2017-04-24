using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour {
    [SerializeField]
    float distance = 1.0f;
    [SerializeField]
    float smoothUp = 0.02f;
    [SerializeField]
    float smoothDown = 0.001f;
    public bool active = false;
    private float target, originalPos;

    void Start () {
        target = transform.position.y + distance;
        originalPos = transform.position.y;
    }
	
	void FixedUpdate () {
		if (transform.position.y != target && active){
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, target), smoothUp);
            // It's a little obnoxious to play both the pressure plate sound and
            // the door's at the same time.
            //AudioManager.Instance.PlaySoundIfNotPlaying(AudioManager.SFX.Door);
        }
        else if (!active)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, originalPos), smoothDown);
        }
    }
}
