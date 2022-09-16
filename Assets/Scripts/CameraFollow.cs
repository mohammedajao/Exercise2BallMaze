using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desPos = transform.position = target.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desPos, smoothSpeed);
        transform.position = smoothedPos;

        transform.LookAt(target);
    }
}
