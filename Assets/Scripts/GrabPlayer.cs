using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPlayer : MonoBehaviour
{
    public GameObject test;
    public float distance;
   public bool hited = false;
    private float cooldownofmonster = 10;
    // Update is called once per frame
    void Update()
    {
        cooldownofmonster += Time.deltaTime;
   distance =   Vector3.Distance(transform.position, test.transform.position);

       if( cooldownofmonster >= 10)
       {
           hited = false;
            cooldownofmonster = 0;
       }

        if (distance < 10 && hited == false )
        {
         gameObject.GetComponent<LineRenderer>().SetPosition(0, transform.position);
           gameObject.GetComponent<LineRenderer>().SetPosition(1, test.transform.position);
            Vector3 moveDirection = transform.position - test.transform.position;
             test.GetComponent<Rigidbody>().AddForce(moveDirection * 150f);
           test.GetComponent<NewPlayer>().movespeed = 0;

           if (Input.GetKeyDown(KeyCode.Mouse0) && GameObject.Find("WeaponsHolder").GetComponent<Meleesystem>().cooldown == 0)
           {
               hited = true; 
            
           
             test.GetComponent<NewPlayer>().movespeed = 100000;
           }
          if(hited == true)
          {
                gameObject.GetComponent<LineRenderer>().SetPosition(0, transform.position);
            gameObject.GetComponent<LineRenderer>().SetPosition(1, transform.position);
          }
        }
       
    }
}
