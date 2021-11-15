using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPlayer : MonoBehaviour
{
    public Transform Playerpos, gameobjectpos;
    public float distance;
   public bool hited = false;
    private float cooldownofmonster = 10;
    // Update is called once per frame
    void Update()
    {
        cooldownofmonster += Time.deltaTime;
        distance =   Vector3.Distance(gameobjectpos.position, Playerpos.position);

       if( cooldownofmonster >= 10)
       {
           hited = false;
            cooldownofmonster = 0;
       }

        if (distance < 10 && hited == false )
        {
         gameObject.GetComponent<LineRenderer>().SetPosition(0, transform.position);
           gameObject.GetComponent<LineRenderer>().SetPosition(1, Vector3.Lerp(gameObject.GetComponent<LineRenderer>().GetPosition(1) , Playerpos.position , 10 * Time.deltaTime));
            
            Playerpos.position = Vector3.MoveTowards(Playerpos.position, gameobjectpos.position, 5 * Time.deltaTime);
           Playerpos.GetComponent<NewPlayer>().movespeed = 0;

           if (Input.GetKeyDown(KeyCode.Mouse0) && GameObject.Find("WeaponsHolder").GetComponent<Meleesystem>().cooldown == 0)
           {
               hited = true; 
             Playerpos.GetComponent<NewPlayer>().movespeed = 100000;
           }

        }
        else if (distance < 4)
        {
            // slowly lose hp
        }
        else if(distance < 1.5f)
        {
            // die instanly
        }
          if(hited == true)
          {
            gameObject.GetComponent<LineRenderer>().SetPosition(0, transform.position);
            gameObject.GetComponent<LineRenderer>().SetPosition(1, Vector3.Lerp(gameObject.GetComponent<LineRenderer>().GetPosition(1), transform.position, 3 * Time.deltaTime));
          }
    }
}
