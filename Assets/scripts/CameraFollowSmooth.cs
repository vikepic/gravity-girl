using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowSmooth : MonoBehaviour {
    public GameObject target;
    static float speed;
    private Vector3 pastCamPos, pastTargetPos;
    private float iniZ;

    void Start()
    {
        goSpace();
        pastCamPos = transform.position;
        pastTargetPos = target.transform.position;
        iniZ = transform.position.z;
    }
    void FixedUpdate()
    {
        // transform.position = new Vector3(target.transform.position.x, transform.position.y, -10.0f);
        transform.position = SmoothApproach(pastCamPos, pastTargetPos, target.transform.position, speed);
        pastCamPos = transform.position;
        pastTargetPos = target.transform.position;
    }

    Vector3 SmoothApproach(Vector3 pastPosition, Vector3 pastTargetPosition, Vector3 targetPosition, float speed)
    {
        float t = Time.deltaTime * speed;
        Vector3 v = (targetPosition - pastTargetPosition) / t;
        Vector3 f = pastPosition - pastTargetPosition + v;
        Vector3 res = targetPosition - v + f * Mathf.Exp(-t);
        return new Vector3(res.x, res.y, iniZ);
    }

    public static void goSpace()
    {
        speed = 10.0f;
        Camera.main.orthographicSize = 12.0f;
    }

    public static void goIn()
    {
        speed = 0.5f;
        Camera.main.orthographicSize = 7.5f;
    }
}

