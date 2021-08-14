using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headpos : MonoBehaviour
{
    public Transform pos;

    public void Start()
    {
        transform.rotation = pos.rotation;
    }
    public void LateUpdate()
    {
        transform.position = pos.position;
    }
}
