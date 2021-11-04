using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleTest : MonoBehaviour
{

    public Transform grapple;
    public bool islaunched;
    public bool uhhh;
    public float sterngthofpull;
    public void Update()
    {
        if (islaunched == false && (Vector3.Distance(transform.position, grapple.position) > 0f))
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            Vector3 moveDirection = grapple.position - transform.position;
            gameObject.GetComponent<Rigidbody>().AddForce(moveDirection * 250f);
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.transform.parent = grapple.transform;
            gameObject.GetComponent<Rigidbody>().drag = 20f;
            gameObject.GetComponent<LineRenderer>().SetPosition(0, transform.position);
            gameObject.GetComponent<LineRenderer>().SetPosition(1, grapple.position);
        }


        if (Input.GetKeyDown(KeyCode.Mouse0) && islaunched == false)
        {
            islaunched = true;

            gameObject.transform.parent = null;
            gameObject.GetComponent<Rigidbody>().drag = 1f;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            gameObject.GetComponent<Rigidbody>().AddForce(grapple.forward * 5000);
        }
        else if (!Input.GetKey(KeyCode.Mouse0) && uhhh == true)
        {
            GameObject.Find("Player").GetComponent<Rigidbody>().drag = 10f;
            islaunched = false;
            uhhh = false;
        }
        if(islaunched == true)
        {
            gameObject.GetComponent<LineRenderer>().SetPosition(0, transform.position);
            gameObject.GetComponent<LineRenderer>().SetPosition(1, grapple.position);
        }
        if(uhhh == true)
        {
            Vector3 test = GameObject.Find("Player").GetComponent<Transform>().position - transform.position ;
            GameObject.Find("Player").GetComponent<Rigidbody>().drag = 60f;
            GameObject.Find("Player").GetComponent<Rigidbody>().AddForce(test * -250);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject)
        {
            sterngthofpull = 0;
            uhhh = true;

            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            
        }
    }
}
