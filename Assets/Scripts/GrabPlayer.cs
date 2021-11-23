using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;

public class GrabPlayer : MonoBehaviour
{
    // pos ----------------------------------------
    private Transform Playerpos, gameobjectpos;
    // bool -------------------------------------
    private  bool hited = false;
    // floats -------------------------------------
    private float distance;
    private float cooldownofmonster = 10;
    private float cooldownofstepingonpuddle = 5;
    // vectors --------------------------------------
     private Vector3 pos;
    // Update is called once per frame
    private  IEnumerator setposforgrab()
    { 
        for(int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.5f);
         gameobjectpos.position = new Vector3(gameobjectpos.position.x, Playerpos.position.y, gameobjectpos.position.z);
        }
    }
   
    private void Awake()
    {
        gameobjectpos = this.transform.GetChild(0);
        Playerpos = GameObject.Find("Player").GetComponent<Transform>();
    }
    private void Start()
    { 
        pos  = new Vector3(transform.position.x, -1, transform.position.z);
        StartCoroutine(setposforgrab());
    }
    void Update()
    {
        cooldownofmonster += Time.deltaTime;
        distance =   Vector3.Distance(gameobjectpos.position, Playerpos.position);

        cooldownofstepingonpuddle += Time.deltaTime;
      
       if( cooldownofmonster >= 10)
       {
           hited = false;
            cooldownofmonster = 0;
       }

        if (distance < 10 && hited == false )
        {
           gameObject.GetComponent<LineRenderer>().SetPosition(0, pos);
           gameObject.GetComponent<LineRenderer>().SetPosition(1, Vector3.Lerp(gameObject.GetComponent<LineRenderer>().GetPosition(1) , Playerpos.position , 10 * Time.deltaTime));
            Playerpos.GetComponent<NewPlayer>().health -= 0.1f ;

            Playerpos.position = Vector3.MoveTowards(Playerpos.position, gameobjectpos.position, 5 * Time.deltaTime);
           Playerpos.GetComponent<NewPlayer>().movespeed = 0;

           if (Input.GetKeyDown(KeyCode.Mouse0) && GameObject.Find("WeaponsHolder").GetComponent<Meleesystem>().cooldown == 0)
           {
               hited = true; 
             Playerpos.GetComponent<NewPlayer>().movespeed = 100000;
           }

        }
        if( distance  < 4.5f && cooldownofstepingonpuddle >= 4)
        {
            Playerpos.GetComponent<NewPlayer>().health -= 30;
            cooldownofstepingonpuddle = 0;
        }
         
          if(hited == true)
          {
            gameObject.GetComponent<LineRenderer>().SetPosition(0, pos );
            gameObject.GetComponent<LineRenderer>().SetPosition(1, Vector3.Lerp(gameObject.GetComponent<LineRenderer>().GetPosition(1), pos, 3 * Time.deltaTime));
          }
    }
}
