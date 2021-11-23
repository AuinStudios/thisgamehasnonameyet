using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAi : MonoBehaviour
{
    public Transform player;
    public float speed= 50;
    public float range;
    private bool right = true;
    private void Update()
    {
        transform.GetChild(0).LookAt(player.position);
        RaycastHit hit;
        
        if(Physics.Raycast(transform.GetChild(0).position , transform.GetChild(0).transform.TransformDirection(Vector3.forward ), out hit, range))
        {
          
            if ( hit.collider && !hit.transform.gameObject.CompareTag("Player") && right == true)
            {
                 gameObject.GetComponent<Rigidbody>().AddForce(transform.GetChild(0).transform.TransformDirection(Vector3.right) * 35);
                
            }
          
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().AddForce(( player.position - transform.position ) *  speed* Time.deltaTime);
            Debug.DrawRay(transform.GetChild(0).position, transform.GetChild(0).forward  *range, Color.green);
        }
        if (Physics.Raycast(transform.GetChild(1).position, transform.GetChild(0).transform.TransformDirection(Vector3.right), out hit, 10))
        {
            right = false;
            gameObject.GetComponent<Rigidbody>().AddForce(transform.GetChild(1).transform.TransformDirection(Vector3.right) * -35);
        }
        else
        {
           
            right = true;
        }
    }
}

