using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForOtherGoos : MonoBehaviour
{
    public ScriptableObectStorage distance;
    public Transform bigoop;
    public float test = 0;
    public void Awake()
    {
        
        foreach(   GameObject goo   in GameObject.FindGameObjectsWithTag("goo"))
        {
            distance.distances += 0.4f;
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        if(distance.distances >= 10)
        {
            Instantiate(bigoop, transform.position, bigoop.rotation);
            foreach (GameObject goo in GameObject.FindGameObjectsWithTag("goo"))
            {
                Destroy(goo);
            }
            distance.distances = 0;


        }
    }
}
