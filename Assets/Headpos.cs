using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headpos : MonoBehaviour
{
    private GameObject pos;
    public headbop running;

    public void Awake()
    {
         if (gameObject.CompareTag("MainCamera"))
         {
               pos = GameObject.Find("Camera");
         }
        else
        {
            pos = GameObject.Find("Head");
        }
    }
    public void Start()
    {
        transform.rotation = pos.transform.rotation;
    }
    public void LateUpdate()
    {
        transform.position = pos.transform.position;
    }

    public void Update()
    {
     

        if (running.enabled && gameObject.CompareTag("MainCamera"))
        {
            pos = GameObject.Find("MainCam");
        }
        else if(!gameObject.CompareTag("MainCamera"))
        {
            pos = GameObject.Find("Head");
        }
        else if (!running.enabled && gameObject.CompareTag("MainCamera"))
        {
            pos = GameObject.Find("Camera");
        }


    }
}
