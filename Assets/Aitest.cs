using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aitest : MonoBehaviour
{
    public float raycastrange = 10;
    private float random;
    private float cooldown;
    private float maxloop = 1;
    private float delay;
    private Vector3 direction;
    private bool beginwandering;

    public void Start()
    {
        Mathf.Clamp(cooldown, 0, 5);
        Mathf.Clamp(delay, 0, 10);
    }
    public IEnumerator randomize()
    {
        
        for(int i = 0; i < maxloop; i++)
        {
            RaycastHit hit;
            beginwandering = false;
         random = Random.Range(0, 360);
        direction = transform.TransformDirection(Quaternion.AngleAxis(random, new Vector3(0, 1, 0)) * new Vector3(0,0, raycastrange));
        if (Physics.Raycast(transform.position, direction, out hit, raycastrange))
        {
            Debug.DrawRay(transform.position, direction * hit.distance, Color.red);
                delay = 0;
            cooldown = 0;
            StopCoroutine(randomize());
        }
        else
        {
              
            Debug.DrawRay(transform.position,direction* raycastrange, Color.green);
                beginwandering = true;
                //transform.position = Vector3.Lerp(transform.position,  direction, 3f * Time.deltaTime);
               
        }
            delay = 0;
        cooldown = 0;
            yield return (null);
        }
       
        
       
    }

    public IEnumerator raycasts()
    {
     
        StartCoroutine(randomize());
        yield return (null);
    }
    // Update is called once per frame
    void Update()
    {
        if (delay <= 5 && beginwandering == true)
        {
            gameObject.GetComponent<Rigidbody>().MovePosition(transform.position + direction * 0.005f);

        }
        if( delay <= 10)
        {
            delay += Time.deltaTime;
        }
        else if (delay >= 4)
        {
            beginwandering = false;
        }

        if(cooldown <= 5)
        {
            cooldown += Time.deltaTime;
        }
        else if(cooldown >= 5  && delay >= 10)
        {
            StartCoroutine(raycasts());

        }
        
    }
}
