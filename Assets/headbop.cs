using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headbop : MonoBehaviour
{
    public Transform oriRotation;
    public Transform camRot;
    public Transform pointA;
    public Transform pointB;
    [SerializeField] public float shakevalue = 0.5f;
    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.rotation = camRot.rotation;
        gameObject.transform.rotation = oriRotation.rotation;
        camRot.position = Vector3.Lerp(pointA.position, pointB.position, shakevalue);

    }
}
