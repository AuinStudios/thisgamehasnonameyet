using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entranceElevatorScript : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
       // anim = GameObject.Find("elevator").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position , transform.forward , out hit , 5))
            {
                if (!hit.transform.CompareTag("Ground"))
                {
                    anim.SetBool("closedoors", true);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Player"))
        //{
        //    transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
        //    anim.SetBool("closedoors", true);
        //}
    }
}
