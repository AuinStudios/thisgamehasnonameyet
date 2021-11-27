using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForOtherGoos : MonoBehaviour
{
    // float and the scriptableobject float to check all nearby puddles -------------------------------------------
    public ScriptableObectStorage distance;
    private float distancebetweenearobject = 4;
    //  spawn the   big puddle its above 20 nearby ---------------------------------------------------------------
    public Transform bigoop;
    // bool that  makes it so that big puddle doesnt  instianice more then once ---------------------------------
    public static bool resetobjects  = false;
    // the list that puts the values towards the scriptableobject and the collider that detects the stuff near it ------------------------
    public  List <GameObject> objects;
    public  Collider[] hits;

    //the main script
    public void Awake()
    {
        
        Destroy(gameObject,  150);
        resetobjects = false;
       
        //distance.hits = Physics.SphereCastAll(transform.position, 10, Vector3.up, 0, layer, QueryTriggerInteraction.Collide); 
        hits = Physics.OverlapSphere(transform.position, 10);
        objects.Clear();
        foreach(Collider hit in hits)
        {
            if ( hit.gameObject.CompareTag("goo"))
            {
                objects.Add(hit.gameObject);
            }
            
        }
    }
    // moves the goos towards the last one 
    public IEnumerator gotowards()
    {
        if(distance.count >= 20)
        {
            foreach(Collider v in hits)
            {
                if (v.transform.CompareTag("goo"))
                {
                   v.transform.position = Vector3.MoveTowards(v.transform.position, transform.position, 0.3f * Time.deltaTime);
                }
                distancebetweenearobject = Vector3.Distance(v.transform.position, transform.position);
                if(distancebetweenearobject <= 0.3f)
                {
                  transform.localScale = Vector3.Lerp(transform.localScale, bigoop.localScale, 0.05f * Time.deltaTime);
                }
            }

            yield return new WaitForSeconds(15);
           Instantiate(bigoop, transform.position, bigoop.rotation);
            resetobjects = true;
            
        }
    }
    // Update is called once per frame
     void FixedUpdate()
     {
        if (resetobjects == true)
        {
            objects.Clear();
            Destroy(gameObject);

        }
     }

    void Update()
    {
        distance.count = objects.Count;
        if(distancebetweenearobject == 4)
        {
            Vector3 normalsize = new Vector3(0.35f, 0.35f, 0.35f);
            transform.localScale = Vector3.Lerp(transform.localScale, normalsize, 1 * Time.deltaTime);
        }
        
        
        StartCoroutine(gotowards());   

       // if(resetobjects == true)
       // {
       //  objects.Clear();
       //  Destroy(gameObject);
       //    
       // }
    }
}